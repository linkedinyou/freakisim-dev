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
using Akka;
using Akka.Actor;
using OpenMetaverse;
using OpenSim.Framework;
using OpenSim.Region.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Threading;
using Akka.Util.Internal;

namespace OpenSim.Region.Framework.Scenes {

    #region messages

    public class SceneAddMessage {
        public SceneAddMessage(Scene scene) {
            this.Scene = scene;
        }
        public Scene Scene { get; private set; }
    }

    public class SceneCloseMessage { }

    public class LoadArchiveToCurrentSceneMessage {
        public LoadArchiveToCurrentSceneMessage(String[] commandparams) {
            this.CmdStrings = commandparams;
        }
        public String[] CmdStrings { get; private set; }
    }

    public class SaveCurrentSceneToArchiveMessage {
        public SaveCurrentSceneToArchiveMessage(String[] commandparams) {
            this.CmdStrings = commandparams;
        }
        public String[] CmdStrings { get; private set; }
    }

    //public class ForEachSceneDelegateMessage {
    //    public ForEachSceneDelegateMessage(Action<Scene> sceneAction) {
    //        this.SceneAction = sceneAction;
    //    }
    //    public Action<Scene> SceneAction { get; private set; }
    //}

    public class SetCurrentSceneMessage {
        public SetCurrentSceneMessage(String regionName) {
            this.RegionName = regionName;
        }
        public String RegionName { get; private set; }
    }


    #endregion

    public delegate void RestartSim(RegionInfo thisregion);

    /// <summary>
    /// Manager for adding, closing and restarting scenes.
    /// </summary>
    public class SceneManager : UntypedActor {
        #region definitions
        private static readonly ILog m_log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public event RestartSim OnRestartSim;

        /// <summary>
        /// Fired when either all regions are ready for use or at least one region has become unready for use where
        /// previously all regions were ready.
        /// </summary>
        public event Action<SceneManager> OnRegionsReadyStatusChange;

        /// <summary>
        /// Are all regions ready for use?
        /// </summary>
        public bool AllRegionsReady {
            get {
                return m_allRegionsReady;
            }

            private set {
                if (m_allRegionsReady != value) {
                    m_allRegionsReady = value;
                    Action<SceneManager> handler = OnRegionsReadyStatusChange;
                    if (handler != null) {
                        foreach (Action<SceneManager> d in handler.GetInvocationList()) {
                            try {
                                d(this);
                            } catch (Exception e) {
                                m_log.ErrorFormat("[SCENE MANAGER]: Delegate for OnRegionsReadyStatusChange failed - continuing {0} - {1}",
                                    e.Message, e.StackTrace);
                            }
                        }
                    }
                }
            }
        }
        private bool m_allRegionsReady;

        //private static SceneManager m_instance = null;
        //public static SceneManager Instance
        //{ 
        //    get {
        //        if (m_instance == null)
        //            m_instance = new SceneManager();
        //        return m_instance;
        //    } 
        //}

        private readonly List<Scene> m_localScenes = new List<Scene>();
        private ReaderWriterLock m_localScenesRwLock = new ReaderWriterLock();

        private List<Scene> Scenes {
            get {
                return new List<Scene>(m_localScenes);
            }
        }

        /// <summary>
        /// Scene selected from the console.
        /// </summary>
        /// <value>
        /// If null, then all scenes are considered selected (signalled as "Root" on the console).
        /// </value>
        private Scene CurrentScene { get; set; }

        private Scene CurrentOrFirstScene {
            get {
                if (CurrentScene == null) {
                    if (m_localScenes.Count > 0)
                        return m_localScenes[0];
                    else
                        return null;
                } else {
                    return CurrentScene;
                }
            }
        }

        #endregion

        #region AKKA Message Handling
        protected override void OnReceive(object message) {
            if (message is SceneAddMessage) {
                m_log.Info("Received SceneAdd message");
                Add(message.AsInstanceOf<SceneAddMessage>().Scene);
            } else if (message is SceneCloseMessage) {
                m_log.Info("Received SceneClose message");
                Close();
                Sender.Tell("Closed", Self);
            } else if (message is LoadArchiveToCurrentSceneMessage) {
                try {
                    m_log.Info("Received LoadArchiveToCurrentScene message");
                    LoadArchiveToCurrentScene(message.AsInstanceOf<LoadArchiveToCurrentSceneMessage>().CmdStrings);
                    Sender.Tell("Archive loaded from oar file", Self);
                } catch (Exception e) {
                    Sender.Tell(new Failure { Exception = e }, Self);
                }
            } else if (message is SaveCurrentSceneToArchiveMessage) {
                try {
                    m_log.Info("Received SaveArchiveToCurrentScene message");
                    SaveCurrentSceneToArchive(message.AsInstanceOf<SaveCurrentSceneToArchiveMessage>().CmdStrings);
                    Sender.Tell("Archive stored to oar file", Self);
                } catch (Exception e) {
                    Sender.Tell(new Failure { Exception = e }, Self);
                }
            // FREAKKI Not Yet Used
            //} else if (message is ForEachSceneDelegateMessage) {
            //    try {
            //        m_log.Info("Received ForEachSceneDelegate message");
            //        ForEachScene(message.AsInstanceOf<ForEachSceneDelegateMessage>().SceneAction);
            //        Sender.Tell("ForEachSceneDelegate applied", Self);
            //    } catch (Exception e) {
            //        Sender.Tell(new Failure { Exception = e }, Self);
            //    }
            } else if (message is SetCurrentSceneMessage) {
                try {
                    m_log.Info("Received SetCurrentScene message");
                    bool isSet = TrySetCurrentScene(message.AsInstanceOf<SetCurrentSceneMessage>().RegionName);
                    if( isSet ) {
                        Sender.Tell("TRUE", Self);
                    } else {
                        Sender.Tell("FALSE", Self);
                    }
                } catch (Exception e) {
                    Sender.Tell(new Failure { Exception = e }, Self);
                }
            } else {
                Unhandled(message);
            }
        }

        #endregion

        private void Close() {
            foreach (Scene t in m_localScenes) {
                t.Close();
            }
        }

        private void Add(Scene scene) {
            m_localScenes.Add(scene);

            // FREAKKI refactor this
            scene.OnRestart += HandleRestart;
            scene.EventManager.OnRegionReadyStatusChange += HandleRegionReadyStatusChange;
        }

        /// <summary>
        /// Save the current scene to an OpenSimulator archive.  This archive will eventually include the prim's assets
        /// as well as the details of the prims themselves.
        /// </summary>
        /// <param name="cmdparams"></param>
        private void SaveCurrentSceneToArchive(string[] cmdparams) {
            IRegionArchiverModule archiver = CurrentOrFirstScene.RequestModuleInterface<IRegionArchiverModule>();
            if (archiver != null)
                archiver.HandleSaveOarConsoleCommand(string.Empty, cmdparams);
        }

        /// <summary>
        /// Load an OpenSim archive into the current scene.  This will load both the shapes of the prims and upload
        /// their assets to the asset service.
        /// </summary>
        /// <param name="cmdparams"></param>
        private void LoadArchiveToCurrentScene(string[] cmdparams) {
            IRegionArchiverModule archiver = CurrentOrFirstScene.RequestModuleInterface<IRegionArchiverModule>();
            //ArchiveScenesGroup scenesGroup = new ArchiveScenesGroup();
            if (archiver != null)
                archiver.HandleLoadOarConsoleCommand(string.Empty, cmdparams, m_localScenes);
        }

        /// <summary>
        /// Sets Current Scene to the given Region Name. 
        /// </summary>
        /// <param name="regionName"></param>
        private bool TrySetCurrentScene(string regionName) {
            if ((String.Compare(regionName, "root") == 0)
                || (String.Compare(regionName, "..") == 0)
                || (String.Compare(regionName, "/") == 0)) {
                CurrentScene = null;
                return true;
            } else {
                foreach (Scene scene in m_localScenes) {
                    if (String.Compare(scene.RegionInfo.RegionName, regionName, true) == 0) {
                        CurrentScene = scene;
                        return true;
                    }
                }
                return false;
            }
        }


        private void ForEachScene(Action<Scene> action) {
            m_localScenes.ForEach(action);
        }

        private void ForEachSelectedScene(Action<Scene> func) {
            if (CurrentScene == null)
                ForEachScene(func);
            else
                func(CurrentScene);
        }

        private void HandleRestart(RegionInfo rdata) {
            Scene restartedScene = null;

            m_localScenesRwLock.AcquireWriterLock(-1);
            try {
                for (int i = 0; i < m_localScenes.Count; i++) {
                    if (rdata.RegionName == m_localScenes[i].RegionInfo.RegionName) {
                        restartedScene = m_localScenes[i];
                        m_localScenes.RemoveAt(i);
                        break;
                    }
                }
            } finally {
                m_localScenesRwLock.ReleaseWriterLock();
            }

            // If the currently selected scene has been restarted, then we can't reselect here since we the scene
            // hasn't yet been recreated.  We will have to leave this to the caller.
            if (CurrentScene == restartedScene)
                CurrentScene = null;

            // Send signal to main that we're restarting this sim.
            OnRestartSim(rdata);
        }

        private void HandleRegionReadyStatusChange(IScene scene) {
            m_localScenesRwLock.AcquireReaderLock(-1);
            try {
                AllRegionsReady = m_localScenes.TrueForAll(s => s.Ready);
            } finally {
                m_localScenesRwLock.ReleaseReaderLock();
            }
        }

        private void Close(Scene cscene) {
            m_localScenesRwLock.AcquireReaderLock(-1);
            try {
                if (m_localScenes.Contains(cscene)) {
                    for (int i = 0; i < m_localScenes.Count; i++) {
                        if (m_localScenes[i].Equals(cscene)) {
                            m_localScenes[i].Close();
                        }
                    }
                }
            } finally {
                m_localScenesRwLock.ReleaseReaderLock();
            }
        }

        private void SendSimOnlineNotification(ulong regionHandle) {
            RegionInfo Result = null;

            m_localScenesRwLock.AcquireReaderLock(-1);
            try {
                for (int i = 0; i < m_localScenes.Count; i++) {
                    if (m_localScenes[i].RegionInfo.RegionHandle == regionHandle) {
                        // Inform other regions to tell their avatar about me
                        Result = m_localScenes[i].RegionInfo;
                    }
                }

                if (Result != null) {
                    for (int i = 0; i < m_localScenes.Count; i++) {
                        if (m_localScenes[i].RegionInfo.RegionHandle != regionHandle) {
                            // Inform other regions to tell their avatar about me
                            //m_localScenes[i].OtherRegionUp(Result);
                        }
                    }
                } else {
                    m_log.Error("[REGION]: Unable to notify Other regions of this Region coming up");
                }
            } finally {
                m_localScenesRwLock.ReleaseReaderLock();
            }
        }

        /// <summary>
        /// Save the prims in the current scene to an xml file in OpenSimulator's original 'xml' format
        /// </summary>
        /// <param name="filename"></param>
        private void SaveCurrentSceneToXml(string filename) {
            IRegionSerialiserModule serialiser = CurrentOrFirstScene.RequestModuleInterface<IRegionSerialiserModule>();
            if (serialiser != null)
                serialiser.SavePrimsToXml(CurrentOrFirstScene, filename);
        }

        /// <summary>
        /// Load an xml file of prims in OpenSimulator's original 'xml' file format to the current scene
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="generateNewIDs"></param>
        /// <param name="loadOffset"></param>
        private void LoadCurrentSceneFromXml(string filename, bool generateNewIDs, Vector3 loadOffset) {
            IRegionSerialiserModule serialiser = CurrentOrFirstScene.RequestModuleInterface<IRegionSerialiserModule>();
            if (serialiser != null)
                serialiser.LoadPrimsFromXml(CurrentOrFirstScene, filename, generateNewIDs, loadOffset);
        }

        /// <summary>
        /// Save the prims in the current scene to an xml file in OpenSimulator's current 'xml2' format
        /// </summary>
        /// <param name="filename"></param>
        private void SaveCurrentSceneToXml2(string filename) {
            IRegionSerialiserModule serialiser = CurrentOrFirstScene.RequestModuleInterface<IRegionSerialiserModule>();
            if (serialiser != null)
                serialiser.SavePrimsToXml2(CurrentOrFirstScene, filename);
        }

        private void SaveNamedPrimsToXml2(string primName, string filename) {
            IRegionSerialiserModule serialiser = CurrentOrFirstScene.RequestModuleInterface<IRegionSerialiserModule>();
            if (serialiser != null)
                serialiser.SaveNamedPrimsToXml2(CurrentOrFirstScene, primName, filename);
        }

        /// <summary>
        /// Load an xml file of prims in OpenSimulator's current 'xml2' file format to the current scene
        /// </summary>
        private void LoadCurrentSceneFromXml2(string filename) {
            IRegionSerialiserModule serialiser = CurrentOrFirstScene.RequestModuleInterface<IRegionSerialiserModule>();
            if (serialiser != null)
                serialiser.LoadPrimsFromXml2(CurrentOrFirstScene, filename);
        }

        private string SaveCurrentSceneMapToXmlString() {
            return CurrentOrFirstScene.Heightmap.SaveToXmlString();
        }

        private void LoadCurrenSceneMapFromXmlString(string mapData) {
            CurrentOrFirstScene.Heightmap.LoadFromXmlString(mapData);
        }

        private void SendCommandToPluginModules(string[] cmdparams) {
            ForEachSelectedScene(delegate(Scene scene) { scene.SendCommandToPlugins(cmdparams); });
        }

        private void SetBypassPermissionsOnCurrentScene(bool bypassPermissions) {
            ForEachSelectedScene(delegate(Scene scene) { scene.Permissions.SetBypassPermissions(bypassPermissions); });
        }

        private void RestartCurrentScene() {
            ForEachSelectedScene(delegate(Scene scene) { scene.RestartNow(); });
        }

        private void BackupCurrentScene() {
            ForEachSelectedScene(delegate(Scene scene) { scene.Backup(true); });
        }
            
        private bool TrySetCurrentScene(UUID regionID) {
            m_log.Debug("Searching for Region: '" + regionID + "'");

            m_localScenesRwLock.AcquireReaderLock(-1);
            try {
                foreach (Scene scene in m_localScenes) {
                    if (scene.RegionInfo.RegionID == regionID) {
                        CurrentScene = scene;
                        return true;
                    }
                }
            } finally {
                m_localScenesRwLock.ReleaseReaderLock();
            }

            return false;
        }

        private bool TryGetScene(string regionName, out Scene scene) {
            m_localScenesRwLock.AcquireReaderLock(-1);
            try {
                foreach (Scene mscene in m_localScenes) {
                    if (String.Compare(mscene.RegionInfo.RegionName, regionName, true) == 0) {
                        scene = mscene;
                        return true;
                    }
                }
            } finally {
                m_localScenesRwLock.ReleaseReaderLock();
            }

            scene = null;
            return false;
        }

        private bool TryGetScene(UUID regionID, out Scene scene) {
            m_localScenesRwLock.AcquireReaderLock(-1);
            try {
                foreach (Scene mscene in m_localScenes) {
                    if (mscene.RegionInfo.RegionID == regionID) {
                        scene = mscene;
                        return true;
                    }
                }
            } finally {
                m_localScenesRwLock.ReleaseReaderLock();
            }

            scene = null;
            return false;
        }

        private bool TryGetScene(uint locX, uint locY, out Scene scene) {
            m_localScenesRwLock.AcquireReaderLock(-1);
            try {
                foreach (Scene mscene in m_localScenes) {
                    if (mscene.RegionInfo.RegionLocX == locX &&
                        mscene.RegionInfo.RegionLocY == locY) {
                        scene = mscene;
                        return true;
                    }
                }
            } finally {
                m_localScenesRwLock.ReleaseReaderLock();
            }

            scene = null;
            return false;
        }

        private bool TryGetScene(IPEndPoint ipEndPoint, out Scene scene) {
            m_localScenesRwLock.AcquireReaderLock(-1);
            try {
                foreach (Scene mscene in m_localScenes) {
                    if ((mscene.RegionInfo.InternalEndPoint.Equals(ipEndPoint.Address)) &&
                        (mscene.RegionInfo.InternalEndPoint.Port == ipEndPoint.Port)) {
                        scene = mscene;
                        return true;
                    }
                }
            } finally {
                m_localScenesRwLock.ReleaseReaderLock();
            }

            scene = null;
            return false;
        }

        private List<ScenePresence> GetCurrentSceneAvatars() {
            List<ScenePresence> avatars = new List<ScenePresence>();

            ForEachSelectedScene(
                delegate(Scene scene) {
                    scene.ForEachRootScenePresence(delegate(ScenePresence scenePresence) {
                        avatars.Add(scenePresence);
                    });
                }
            );

            return avatars;
        }

        private List<ScenePresence> GetCurrentScenePresences() {
            List<ScenePresence> presences = new List<ScenePresence>();

            ForEachSelectedScene(delegate(Scene scene) {
                scene.ForEachScenePresence(delegate(ScenePresence sp) {
                    presences.Add(sp);
                });
            });

            return presences;
        }

        private RegionInfo GetRegionInfo(UUID regionID) {
            m_localScenesRwLock.AcquireReaderLock(-1);
            try {
                foreach (Scene scene in m_localScenes) {
                    if (scene.RegionInfo.RegionID == regionID) {
                        return scene.RegionInfo;
                    }
                }
            } finally {
                m_localScenesRwLock.ReleaseReaderLock();
            }

            return null;
        }

        private void ForceCurrentSceneClientUpdate() {
            ForEachSelectedScene(delegate(Scene scene) { scene.ForceClientUpdate(); });
        }

        private void HandleEditCommandOnCurrentScene(string[] cmdparams) {
            ForEachSelectedScene(delegate(Scene scene) { scene.HandleEditCommand(cmdparams); });
        }

        private bool TryGetScenePresence(UUID avatarId, out ScenePresence avatar) {
            m_localScenesRwLock.AcquireReaderLock(-1);
            try {
                foreach (Scene scene in m_localScenes) {
                    if (scene.TryGetScenePresence(avatarId, out avatar)) {
                        return true;
                    }
                }
            } finally {
                m_localScenesRwLock.ReleaseReaderLock();
            }

            avatar = null;
            return false;
        }

        private bool TryGetRootScenePresence(UUID avatarId, out ScenePresence avatar) {
            m_localScenesRwLock.AcquireReaderLock(-1);
            try {
                foreach (Scene scene in m_localScenes) {
                    avatar = scene.GetScenePresence(avatarId);

                    if (avatar != null && !avatar.IsChildAgent)
                        return true;
                }
            } finally {
                m_localScenesRwLock.ReleaseReaderLock();
            }

            avatar = null;
            return false;
        }

        private void CloseScene(Scene scene) {
            m_localScenesRwLock.AcquireWriterLock(-1);
            try {
                m_localScenes.Remove(scene);
            } finally {
                m_localScenesRwLock.ReleaseWriterLock();
            }

            scene.Close();
        }

        private bool TryGetAvatarByName(string avatarName, out ScenePresence avatar) {
            m_localScenesRwLock.AcquireReaderLock(-1);
            try {
                foreach (Scene scene in m_localScenes) {
                    if (scene.TryGetAvatarByName(avatarName, out avatar)) {
                        return true;
                    }
                }
            } finally {
                m_localScenesRwLock.ReleaseReaderLock();
            }

            avatar = null;
            return false;
        }

        private bool TryGetRootScenePresenceByName(string firstName, string lastName, out ScenePresence sp) {
            m_localScenesRwLock.AcquireReaderLock(-1);
            try {
                foreach (Scene scene in m_localScenes) {
                    sp = scene.GetScenePresence(firstName, lastName);
                    if (sp != null && !sp.IsChildAgent)
                        return true;
                }
            } finally {
                m_localScenesRwLock.ReleaseReaderLock();
            }

            sp = null;
            return false;
        }

    }
}
