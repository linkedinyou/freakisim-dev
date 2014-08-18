/*
 * Copyright (c) Contributors, http://opensimulator.org/
 * See CONTRIBUTORS.TXT for a full list of copyright holders.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the OpenSimulator Project nor the
 *       names of its contributors may be used to endorse or promote products
 *       derived from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE DEVELOPERS ``AS IS'' AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL THE CONTRIBUTORS BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using log4net;
using Nini.Config;
using OpenMetaverse;
using OpenSim.Framework;
using OpenSim.Region.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Timers;
using GridRegion = OpenSim.Services.Interfaces.GridRegion;

namespace OpenSim.Region.Framework.Scenes
{
    public abstract class SceneBase : IScene
    {
        private static readonly ILog m_log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

#pragma warning disable 414
        private static readonly string LogHeader = "[SCENE]";
#pragma warning restore 414

        #region Events

        public event restart OnRestart;

        #endregion

        #region Fields

        public string Name { get { return RegionInfo.RegionName; } }
        
        public IConfigSource Config
        {
            get { return GetConfig(); }
        }

        protected virtual IConfigSource GetConfig()
        {
            return null;
        }

        /// <value>
        /// All the region modules attached to this scene.
        /// </value>
        public Dictionary<string, IRegionModuleBase> RegionModules
        {
            get { return m_regionModules; }
        }
        private Dictionary<string, IRegionModuleBase> m_regionModules = new Dictionary<string, IRegionModuleBase>();

        /// <value>
        /// The module interfaces available from this scene.
        /// </value>
        protected Dictionary<Type, List<object>> ModuleInterfaces = new Dictionary<Type, List<object>>();

        protected Dictionary<string, object> ModuleAPIMethods = new Dictionary<string, object>();

        /// <value>
        /// The module commanders available from this scene
        /// </value>
        private ThreadedClasses.RwLockedDictionary<string, ICommander> m_moduleCommanders = new ThreadedClasses.RwLockedDictionary<string, ICommander>();
        
        /// <value>
        /// Registered classes that are capable of creating entities.
        /// </value>
        protected Dictionary<PCode, IEntityCreator> m_entityCreators = new Dictionary<PCode, IEntityCreator>();

        /// <summary>
        /// The last allocated local prim id.  When a new local id is requested, the next number in the sequence is
        /// dispensed.
        /// </summary>
        protected uint m_lastAllocatedLocalId = 720000;

        private readonly Mutex _primAllocateMutex = new Mutex(false);
        
        protected readonly ClientManager m_clientManager = new ClientManager();

        public bool LoginsEnabled
        {
            get
            {
                return m_loginsEnabled;
            }

            set
            {
                if (m_loginsEnabled != value)
                {
                    m_loginsEnabled = value;
                    EventManager.TriggerRegionLoginsStatusChange(this);
                }
            }
        }
        private bool m_loginsEnabled;

        public bool Ready
        {
            get
            {
                return m_ready;
            }

            set
            {
                if (m_ready != value)
                {
                    m_ready = value;
                    EventManager.TriggerRegionReadyStatusChange(this);
                }
            }
        }
        private bool m_ready;

        public float TimeDilation
        {
            get { return 1.0f; }
        }

        protected ulong m_regionHandle;
        protected string m_regionName;
        protected RegionInfo m_regInfo;

        public ITerrainChannel Heightmap;

        /// <value>
        /// Allows retrieval of land information for this scene.
        /// </value>
        public ILandChannel LandChannel;

        /// <value>
        /// Manage events that occur in this scene (avatar movement, script rez, etc.).  Commonly used by region modules
        /// to subscribe to scene events.
        /// </value>
        public EventManager EventManager
        {
            get { return m_eventManager; }
        }
        protected EventManager m_eventManager;

        protected ScenePermissions m_permissions;
        public ScenePermissions Permissions
        {
            get { return m_permissions; }
        }

         /* Used by the loadbalancer plugin on GForge */
        protected RegionStatus m_regStatus;
        public RegionStatus RegionStatus
        {
            get { return m_regStatus; }
            set { m_regStatus = value; }
        }

        #endregion

        private System.Timers.Timer m_IPUpdateTimer;

        public SceneBase(RegionInfo regInfo)
        {
			if (m_log.IsDebugEnabled) {
				m_log.DebugFormat ("{0} ", System.Reflection.MethodBase.GetCurrentMethod ().Name);
			}

            RegionInfo = regInfo;
            if(RegionInfo.MayChangeIP)
            {
                m_IPUpdateTimer = new System.Timers.Timer(60000);
                m_IPUpdateTimer.Elapsed += OnIPChangeTimer;
                m_IPUpdateTimer.Interval = 60000; /* 1 hour interval */
                m_IPUpdateTimer.Start();
            }
        }

        private void OnIPChangeTimer(object source, ElapsedEventArgs e)
        {
			if (m_log.IsDebugEnabled) {
				m_log.DebugFormat ("{0} ", System.Reflection.MethodBase.GetCurrentMethod ().Name);
			}

            /* Trigger resolver */
            RegionInfo.CheckExternalHostName();
            if (RegionInfo.IsIPChanged)
            {
                EventManager.TriggerOnSimulatorIPChanged(Util.GetHostFromDNS(RegionInfo.ExternalHostName));
            }
        }

        #region Update Methods

        /// <summary>
        /// Called to update the scene loop by a number of frames and until shutdown.
        /// </summary>
        /// <param name="frames">
        /// Number of frames to update.  Exits on shutdown even if there are frames remaining.
        /// If -1 then updates until shutdown.
        /// </param>
        public abstract void Update(int frames);

        #endregion

        #region Terrain Methods

        /// <summary>
        /// Loads the World heightmap
        /// </summary>
        public abstract void LoadWorldMap();

        /// <summary>
        /// Send the region heightmap to the client
        /// </summary>
        /// <param name="RemoteClient">Client to send to</param>
        public virtual void SendLayerData(IClientAPI RemoteClient)
        {
			if (m_log.IsDebugEnabled) {
				m_log.DebugFormat ("{0} ", System.Reflection.MethodBase.GetCurrentMethod ().Name);
			}

            ITerrainModule terrModule = RequestModuleInterface<ITerrainModule>();
            if (terrModule != null)
            {
                terrModule.PushTerrain(RemoteClient);
            }
        }

        #endregion

        #region Add/Remove Agent/Avatar

        public abstract ISceneAgent AddNewAgent(IClientAPI client, PresenceType type);

        public abstract bool CloseAgent(UUID agentID, bool force);

        public bool TryGetScenePresence(UUID agentID, out object scenePresence)
        {
			if (m_log.IsDebugEnabled) {
				m_log.DebugFormat ("{0} ", System.Reflection.MethodBase.GetCurrentMethod ().Name);
			}

            scenePresence = null;
            ScenePresence sp = null;
            if (TryGetScenePresence(agentID, out sp))
            {
                scenePresence = sp;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Try to get a scene presence from the scene
        /// </summary>
        /// <param name="agentID"></param>
        /// <param name="scenePresence">null if there is no scene presence with the given agent id</param>
        /// <returns>true if there was a scene presence with the given id, false otherwise.</returns>
        public abstract bool TryGetScenePresence(UUID agentID, out ScenePresence scenePresence);

        #endregion

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public virtual RegionInfo RegionInfo { get; private set; }

        #region admin stuff
        
        public abstract void OtherRegionUp(GridRegion otherRegion);

        public virtual string GetSimulatorVersion()
        {
            return "OpenSimulator Server";
        }

        #endregion

        #region Shutdown

        /// <summary>
        /// Tidy before shutdown
        /// </summary>
        public virtual void Close()
        {
			if (m_log.IsDebugEnabled) {
				m_log.DebugFormat ("{0} ", System.Reflection.MethodBase.GetCurrentMethod ().Name);
			}

            try
            {
                EventManager.TriggerShutdown();
            }
            catch (Exception e)
            {
                m_log.Error(string.Format("[SCENE]: SceneBase.cs: Close() - Failed with exception ", e));
            }
        }

        #endregion

        /// <summary>
        /// Returns a new unallocated local ID
        /// </summary>
        /// <returns>A brand new local ID</returns>
        public uint AllocateLocalId()
        {
			if (m_log.IsDebugEnabled) {
				m_log.DebugFormat ("{0} ", System.Reflection.MethodBase.GetCurrentMethod ().Name);
			}

            uint myID;

            _primAllocateMutex.WaitOne();
            myID = ++m_lastAllocatedLocalId;
            _primAllocateMutex.ReleaseMutex();

            return myID;
        }
        
        #region Module Methods

        /// <summary>
        /// Add a region-module to this scene. TODO: This will replace AddModule in the future.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="module"></param>
        public void AddRegionModule(string name, IRegionModuleBase module)
        {
			if (m_log.IsDebugEnabled) {
				m_log.DebugFormat ("{0} ", System.Reflection.MethodBase.GetCurrentMethod ().Name);
			}

            if (!RegionModules.ContainsKey(name))
            {
                RegionModules.Add(name, module);
            }
        }

        public void RemoveRegionModule(string name)
        {
			if (m_log.IsDebugEnabled) {
				m_log.DebugFormat ("{0} ", System.Reflection.MethodBase.GetCurrentMethod ().Name);
			}

            RegionModules.Remove(name);
        }

        /// <summary>
        /// Register a module commander.
        /// </summary>
        /// <param name="commander"></param>
        public void RegisterModuleCommander(ICommander commander)
        {
			if (m_log.IsDebugEnabled) {
				m_log.DebugFormat ("{0} ", System.Reflection.MethodBase.GetCurrentMethod ().Name);
			}

            m_moduleCommanders.Add(commander.Name, commander);
        }

        /// <summary>
        /// Unregister a module commander and all its commands
        /// </summary>
        /// <param name="name"></param>
        public void UnregisterModuleCommander(string name)
        {
			if (m_log.IsDebugEnabled) {
				m_log.DebugFormat ("{0} ", System.Reflection.MethodBase.GetCurrentMethod ().Name);
			}

            m_moduleCommanders.Remove(name);
        }

        /// <summary>
        /// Get a module commander
        /// </summary>
        /// <param name="name"></param>
        /// <returns>The module commander, null if no module commander with that name was found</returns>
        public ICommander GetCommander(string name)
        {
			if (m_log.IsDebugEnabled) {
				m_log.DebugFormat ("{0} ", System.Reflection.MethodBase.GetCurrentMethod ().Name);
			}

            ICommander val;
            if (m_moduleCommanders.TryGetValue(name, out val))
            {
                return val;
            }
            
            return null;
        }

        public Dictionary<string, ICommander> GetCommanders()
        {
			if (m_log.IsDebugEnabled) {
				m_log.DebugFormat ("{0} ", System.Reflection.MethodBase.GetCurrentMethod ().Name);
			}

            return new Dictionary<string, ICommander>(m_moduleCommanders);
        }

        /// <summary>
        /// Register an interface to a region module.  This allows module methods to be called directly as
        /// well as via events.  If there is already a module registered for this interface, it is not replaced
        /// (is this the best behaviour?)
        /// </summary>
        /// <param name="mod"></param>
        public void RegisterModuleInterface<M>(M mod)
        {
			if (m_log.IsDebugEnabled) {
				m_log.DebugFormat ("{0}<M>(M mod) M: {1}", System.Reflection.MethodBase.GetCurrentMethod ().Name,  typeof(M));
			}

            
            List<Object> l = null;
            if (!ModuleInterfaces.TryGetValue(typeof(M), out l))
            {
                l = new List<Object>();
                ModuleInterfaces.Add(typeof(M), l);
            }

            if (l.Count > 0)
                return;

            l.Add(mod);

            if (mod is IEntityCreator)
            {
                IEntityCreator entityCreator = (IEntityCreator)mod;
                foreach (PCode pcode in entityCreator.CreationCapabilities)
                {
                    m_entityCreators[pcode] = entityCreator;
                }
            }
        }

        public void UnregisterModuleInterface<M>(M mod)
        {
			if (m_log.IsDebugEnabled) {
				m_log.DebugFormat ("{0}<M>(M mod) ", System.Reflection.MethodBase.GetCurrentMethod ().Name);
			}

            List<Object> l;
            if (ModuleInterfaces.TryGetValue(typeof(M), out l))
            {
                if (l.Remove(mod))
                {
                    if (mod is IEntityCreator)
                    {
                        IEntityCreator entityCreator = (IEntityCreator)mod;
                        foreach (PCode pcode in entityCreator.CreationCapabilities)
                        {
                            m_entityCreators[pcode] = null;
                        }
                    }
                }
            }
        }

        public void StackModuleInterface<M>(M mod)
        {
			if (m_log.IsDebugEnabled) {
				m_log.DebugFormat ("{0}<M>(M mod)", System.Reflection.MethodBase.GetCurrentMethod ().Name);
			}

            List<Object> l;
            if (ModuleInterfaces.ContainsKey(typeof(M)))
                l = ModuleInterfaces[typeof(M)];
            else
                l = new List<Object>();

            if (l.Contains(mod))
                return;

            l.Add(mod);

            if (mod is IEntityCreator)
            {
                IEntityCreator entityCreator = (IEntityCreator)mod;
                foreach (PCode pcode in entityCreator.CreationCapabilities)
                {
                    m_entityCreators[pcode] = entityCreator;
                }
            }

            ModuleInterfaces[typeof(M)] = l;
        }

        /// <summary>
        /// For the given interface, retrieve the region module which implements it.
        /// </summary>
        /// <returns>null if there is no registered module implementing that interface</returns>
        public T RequestModuleInterface<T>()
        {
			if (m_log.IsDebugEnabled) {
				m_log.DebugFormat ("{0}<T> ", System.Reflection.MethodBase.GetCurrentMethod ().Name);
			}

            if (ModuleInterfaces.ContainsKey(typeof(T)) &&
                    (ModuleInterfaces[typeof(T)].Count > 0))
                return (T)ModuleInterfaces[typeof(T)][0];
            else
                return default(T);
        }

        /// <summary>
        /// For the given interface, retrieve an array of region modules that implement it.
        /// </summary>
        /// <returns>an empty array if there are no registered modules implementing that interface</returns>
        public T[] RequestModuleInterfaces<T>()
        {
			if (m_log.IsDebugEnabled) {
				m_log.DebugFormat ("{0}<T>() ", System.Reflection.MethodBase.GetCurrentMethod ().Name);
			}

            if (ModuleInterfaces.ContainsKey(typeof(T)))
            {
                List<T> ret = new List<T>();

                foreach (Object o in ModuleInterfaces[typeof(T)])
                    ret.Add((T)o);
                return ret.ToArray();
            }
            else
            {
                return new T[] {};
            }
        }
        
        #endregion
        
        /// <summary>
        /// Call this from a region module to add a command to the OpenSim console.
        /// </summary>
        /// <param name="mod"></param>
        /// <param name="command"></param>
        /// <param name="shorthelp"></param>
        /// <param name="longhelp"></param>
        /// <param name="callback"></param>
        public void AddCommand(IRegionModuleBase module, string command, string shorthelp, string longhelp, CommandDelegate callback)
        {
			if (m_log.IsDebugEnabled) {
				m_log.DebugFormat ("{0} ", System.Reflection.MethodBase.GetCurrentMethod ().Name);
			}

            AddCommand(module, command, shorthelp, longhelp, string.Empty, callback);
        }

        /// <summary>
        /// Call this from a region module to add a command to the OpenSim console.
        /// </summary>
        /// <param name="mod">
        /// The use of IRegionModuleBase is a cheap trick to get a different method signature,
        /// though all new modules should be using interfaces descended from IRegionModuleBase anyway.
        /// </param>
        /// <param name="category">
        /// Category of the command.  This is the section under which it will appear when the user asks for help
        /// </param>
        /// <param name="command"></param>
        /// <param name="shorthelp"></param>
        /// <param name="longhelp"></param>
        /// <param name="callback"></param>
        public void AddCommand(
            string category, IRegionModuleBase module, string command, string shorthelp, string longhelp, CommandDelegate callback)
        {
			if (m_log.IsDebugEnabled) {
				m_log.DebugFormat ("{0} ", System.Reflection.MethodBase.GetCurrentMethod ().Name);
			}

            AddCommand(category, module, command, shorthelp, longhelp, string.Empty, callback);
        }

        /// <summary>
        /// Call this from a region module to add a command to the OpenSim console.
        /// </summary>
        /// <param name="mod"></param>
        /// <param name="command"></param>
        /// <param name="shorthelp"></param>
        /// <param name="longhelp"></param>
        /// <param name="descriptivehelp"></param>
        /// <param name="callback"></param>
        public void AddCommand(IRegionModuleBase module, string command, string shorthelp, string longhelp, string descriptivehelp, CommandDelegate callback)
        {
			if (m_log.IsDebugEnabled) {
				m_log.DebugFormat ("{0} ", System.Reflection.MethodBase.GetCurrentMethod ().Name);
			}

            string moduleName = "";

            if (module != null)
                moduleName = module.Name;

            AddCommand(moduleName, module, command, shorthelp, longhelp, descriptivehelp, callback);
        }

        /// <summary>
        /// Call this from a region module to add a command to the OpenSim console.
        /// </summary>
        /// <param name="category">
        /// Category of the command.  This is the section under which it will appear when the user asks for help
        /// </param>
        /// <param name="mod"></param>
        /// <param name="command"></param>
        /// <param name="shorthelp"></param>
        /// <param name="longhelp"></param>
        /// <param name="descriptivehelp"></param>
        /// <param name="callback"></param>
        public void AddCommand(
            string category, IRegionModuleBase module, string command,
            string shorthelp, string longhelp, string descriptivehelp, CommandDelegate callback)
        {
			if (m_log.IsDebugEnabled) {
				m_log.DebugFormat ("{0} ", System.Reflection.MethodBase.GetCurrentMethod ().Name);
			}

            if (MainConsole.Instance == null)
                return;

            bool shared = false;

            if (module != null)
                shared = module is ISharedRegionModule;

            MainConsole.Instance.Commands.AddCommand(
                category, shared, command, shorthelp, longhelp, descriptivehelp, callback);
        }

        public virtual ISceneObject DeserializeObject(string representation)
        {
            return null;
        }

        public virtual bool AllowScriptCrossings
        {
            get { return false; }
        }

        public virtual void Start()
        {
        }

        public void Restart()
        {
			if (m_log.IsDebugEnabled) {
				m_log.DebugFormat ("{0} ", System.Reflection.MethodBase.GetCurrentMethod ().Name);
			}

            // This has to be here to fire the event
            restart handlerPhysicsCrash = OnRestart;
            if (handlerPhysicsCrash != null)
                handlerPhysicsCrash(RegionInfo);
        }

        public abstract bool CheckClient(UUID agentID, System.Net.IPEndPoint ep);
    }
}
