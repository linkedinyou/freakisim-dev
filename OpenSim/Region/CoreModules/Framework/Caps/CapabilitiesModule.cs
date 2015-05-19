/*
 * Copyright (c) Contributors, http://opensimulator.org/
 * See CONTRIBUTORS.TXT for a full list of copyright holders.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyrightD
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using log4net;
using Mono.Addins;
using Nini.Config;
using OpenMetaverse;
using OpenSim.Framework;
using OpenSim.Framework.Console;
using OpenSim.Framework.Servers;
using OpenSim.Framework.Servers.HttpServer;
using OpenSim.Region.Framework.Interfaces;
using OpenSim.Region.Framework.Scenes;

namespace OpenSim.Region.CoreModules.Framework.Caps
{
    [Extension(Path = "/OpenSim/RegionModules", NodeName = "RegionModule", Id = "CapabilitiesModule")]
    public class CapabilitiesModule : INonSharedRegionModule, ICapabilitiesModule
    { 
        private static readonly ILog m_log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private string m_showCapsCommandFormat = "   {0,-38} {1,-60}\n";
        
        protected Scene m_scene;
        
        /// <summary>
        /// Each agent has its own capabilities handler.
        /// </summary>
        private ThreadedClasses.RwLockedDictionary<UUID, OpenSim.Framework.Capabilities.Caps> m_capsObjects = new ThreadedClasses.RwLockedDictionary<UUID, OpenSim.Framework.Capabilities.Caps>();

        private ThreadedClasses.RwLockedDictionary<UUID, string> m_capsPaths = new ThreadedClasses.RwLockedDictionary<UUID, string>();

        private ThreadedClasses.RwLockedDictionary<UUID, ThreadedClasses.RwLockedDictionary<ulong, string>> m_childrenSeeds
            = new ThreadedClasses.RwLockedDictionary<UUID, ThreadedClasses.RwLockedDictionary<ulong, string>>();

        public void Initialise(IConfigSource source)
        {
        }

        public void AddRegion(Scene scene)
        {
            m_scene = scene;
            m_scene.RegisterModuleInterface<ICapabilitiesModule>(this);

        }

        public void RegionLoaded(Scene scene)
        {
        }

        public void RemoveRegion(Scene scene)
        {
            m_scene.UnregisterModuleInterface<ICapabilitiesModule>(this);
        }
        
        public void PostInitialise() 
        {
        }

        public void Close() {}

        public string Name 
        { 
            get { return "Capabilities Module"; } 
        }

        public Type ReplaceableInterface
        {
            get { return null; }
        }

        public Scene MScene
        {
            set { m_scene = value; }
            get { return m_scene; }
        }

        public Scene MScene1
        {
            set { m_scene = value; }
            get { return m_scene; }
        }

        public void CreateCaps(UUID agentId)
        {
            if (m_scene.RegionInfo.EstateSettings.IsBanned(agentId))
                return;

            OpenSim.Framework.Capabilities.Caps caps;
            String capsObjectPath = GetCapsPath(agentId);

            caps = new OpenSim.Framework.Capabilities.Caps(MainServer.Instance, m_scene.RegionInfo.ExternalHostName,
                    (MainServer.Instance == null) ? 0: MainServer.Instance.Port,
                    capsObjectPath, agentId, m_scene.RegionInfo.RegionName);

            m_capsObjects[agentId] = caps;

            m_scene.EventManager.TriggerOnRegisterCaps(agentId, caps);
        }

        public void RemoveCaps(UUID agentId)
        {
            m_log.DebugFormat("[CAPS]: Remove caps for agent {0} in region {1}", agentId, m_scene.RegionInfo.RegionName);
            m_childrenSeeds.Remove(agentId);

            
            OpenSim.Framework.Capabilities.Caps caps;
            if(m_capsObjects.Remove(agentId, out caps))
            {
                caps.DeregisterHandlers();
                m_scene.EventManager.TriggerOnDeregisterCaps(agentId, caps);
            }
            else
            {
                m_log.WarnFormat(
                    "[CAPS]: Received request to remove CAPS handler for root agent {0} in {1}, but no such CAPS handler found!",
                    agentId, m_scene.RegionInfo.RegionName);
            }
        }
        
        public OpenSim.Framework.Capabilities.Caps GetCapsForUser(UUID agentId)
        {
            OpenSim.Framework.Capabilities.Caps caps;
            if(m_capsObjects.TryGetValue(agentId, out caps))
            {
                return caps;
            }
            
            return null;
        }
        
        public void SetAgentCapsSeeds(AgentCircuitData agent)
        {
            m_capsPaths[agent.AgentID] = agent.CapsPath;

            m_childrenSeeds[agent.AgentID]
                = ((agent.ChildrenCapSeeds == null) ? new ThreadedClasses.RwLockedDictionary<ulong, string>() : 
                        new ThreadedClasses.RwLockedDictionary<ulong,string>(agent.ChildrenCapSeeds));
        }
        
        public string GetCapsPath(UUID agentId)
        {
            string capsPath;
            if(m_capsPaths.TryGetValue(agentId, out capsPath))
            {
                return capsPath;
            }
            return null;
        }
        
        public Dictionary<ulong, string> GetChildrenSeeds(UUID agentID)
        {
            ThreadedClasses.RwLockedDictionary<ulong, string> seeds = null;

            if (m_childrenSeeds.TryGetValue(agentID, out seeds))
                return new Dictionary<ulong, string>(seeds);

            return new Dictionary<ulong, string>();
        }

        public void DropChildSeed(UUID agentID, ulong handle)
        {
            ThreadedClasses.RwLockedDictionary<ulong, string> seeds;

            if (m_childrenSeeds.TryGetValue(agentID, out seeds))
            {
                seeds.Remove(handle);
            }
        }

        public string GetChildSeed(UUID agentID, ulong handle)
        {
            ThreadedClasses.RwLockedDictionary<ulong, string> seeds;
            string returnval;

            if (m_childrenSeeds.TryGetValue(agentID, out seeds))
            {
                if (seeds.TryGetValue(handle, out returnval))
                    return returnval;
            }

            return null;
        }

        public void SetChildrenSeed(UUID agentID, Dictionary<ulong, string> seeds)
        {
            //m_log.DebugFormat(" !!! Setting child seeds in {0} to {1}", m_scene.RegionInfo.RegionName, seeds.Count);

            m_childrenSeeds[agentID] = new ThreadedClasses.RwLockedDictionary<ulong, string>(seeds);
        }

        public void DumpChildrenSeeds(UUID agentID)
        {
            m_log.Info("================ ChildrenSeed "+m_scene.RegionInfo.RegionName+" ================");
            ThreadedClasses.RwLockedDictionary<ulong, string> seeds;

            if (m_childrenSeeds.TryGetValue(agentID, out seeds))
            {
                seeds.ForEach(delegate(KeyValuePair<ulong, string> kvp)
                {
                    uint x, y;
                    Util.RegionHandleToRegionLoc(kvp.Key, out x, out y);
                    m_log.Info(" >> " + x + ", " + y + ": " + kvp.Value);
                });
            }
        }

        public void BuildDetailedStatsByCapReport(StringBuilder sb, string capName)
        {
            sb.AppendFormat("Capability name {0}\n", capName);

            ConsoleDisplayTable cdt = new ConsoleDisplayTable();
            cdt.AddColumn("User Name", 34);
            cdt.AddColumn("Req Received", 12);
            cdt.AddColumn("Req Handled", 12);
            cdt.Indent = 2;

            Dictionary<string, int> receivedStats = new Dictionary<string, int>();
            Dictionary<string, int> handledStats = new Dictionary<string, int>();

            m_scene.ForEachScenePresence(
                sp =>
                {
                    OpenSim.Framework.Capabilities.Caps caps = m_scene.CapsModule.GetCapsForUser(sp.UUID);

                    if (caps == null)
                        return;

                    Dictionary<string, IRequestHandler> capsHandlers = caps.CapsHandlers.GetCapsHandlers();

                    IRequestHandler reqHandler;
                    if (capsHandlers.TryGetValue(capName, out reqHandler))
                    {
                        receivedStats[sp.Name] = reqHandler.RequestsReceived;
                        handledStats[sp.Name] = reqHandler.RequestsHandled;
                    }        
                    else 
                    {
                        PollServiceEventArgs pollHandler = null;
                        if (caps.TryGetPollHandler(capName, out pollHandler))
                        {
                            receivedStats[sp.Name] = pollHandler.RequestsReceived;
                            handledStats[sp.Name] = pollHandler.RequestsHandled;
                        }
                    }
                }
            );

            foreach (KeyValuePair<string, int> kvp in receivedStats.OrderByDescending(kp => kp.Value))
            {
                cdt.AddRow(kvp.Key, kvp.Value, handledStats[kvp.Key]);
            }

            sb.Append(cdt.ToString());
        }

        public void BuildSummaryStatsByCapReport(StringBuilder sb)
        {
            ConsoleDisplayTable cdt = new ConsoleDisplayTable();
            cdt.AddColumn("Name", 34);
            cdt.AddColumn("Req Received", 12);
            cdt.AddColumn("Req Handled", 12);
            cdt.Indent = 2;

            Dictionary<string, int> receivedStats = new Dictionary<string, int>();
            Dictionary<string, int> handledStats = new Dictionary<string, int>();

            m_scene.ForEachScenePresence(
                sp =>
                {
                    OpenSim.Framework.Capabilities.Caps caps = m_scene.CapsModule.GetCapsForUser(sp.UUID);

                    if (caps == null)
                        return;            

                    foreach (IRequestHandler reqHandler in caps.CapsHandlers.GetCapsHandlers().Values)
                    {
                        string reqName = reqHandler.Name ?? "";

                        if (!receivedStats.ContainsKey(reqName))
                        {
                            receivedStats[reqName] = reqHandler.RequestsReceived;
                            handledStats[reqName] = reqHandler.RequestsHandled;
                        }
                        else
                        {
                            receivedStats[reqName] += reqHandler.RequestsReceived;
                            handledStats[reqName] += reqHandler.RequestsHandled;
                        }
                    }

                    foreach (KeyValuePair<string, PollServiceEventArgs> kvp in caps.GetPollHandlers())
                    {
                        string name = kvp.Key;
                        PollServiceEventArgs pollHandler = kvp.Value;

                        if (!receivedStats.ContainsKey(name))
                        {
                            receivedStats[name] = pollHandler.RequestsReceived;
                            handledStats[name] = pollHandler.RequestsHandled;
                        }
                            else
                        {
                            receivedStats[name] += pollHandler.RequestsReceived;
                            handledStats[name] += pollHandler.RequestsHandled;
                        }
                    }
                }
            );
                    
            foreach (KeyValuePair<string, int> kvp in receivedStats.OrderByDescending(kp => kp.Value))
                cdt.AddRow(kvp.Key, kvp.Value, handledStats[kvp.Key]);

            sb.Append(cdt.ToString());
        }

        public void BuildDetailedStatsByUserReport(StringBuilder sb, ScenePresence sp)
        {
            sb.AppendFormat("Avatar name {0}, type {1}\n", sp.Name, sp.IsChildAgent ? "child" : "root");

            ConsoleDisplayTable cdt = new ConsoleDisplayTable();
            cdt.AddColumn("Cap Name", 34);
            cdt.AddColumn("Req Received", 12);
            cdt.AddColumn("Req Handled", 12);
            cdt.Indent = 2;

            OpenSim.Framework.Capabilities.Caps caps = m_scene.CapsModule.GetCapsForUser(sp.UUID);

            if (caps == null)
                return;

            List<CapabilitiesModule.CapTableRow> capRows = new List<CapabilitiesModule.CapTableRow>();

            foreach (IRequestHandler reqHandler in caps.CapsHandlers.GetCapsHandlers().Values)
                capRows.Add(new CapabilitiesModule.CapTableRow(reqHandler.Name, reqHandler.RequestsReceived, reqHandler.RequestsHandled));

            foreach (KeyValuePair<string, PollServiceEventArgs> kvp in caps.GetPollHandlers())
                capRows.Add(new CapabilitiesModule.CapTableRow(kvp.Key, kvp.Value.RequestsReceived, kvp.Value.RequestsHandled));

            foreach (CapabilitiesModule.CapTableRow ctr in capRows.OrderByDescending(ctr => ctr.RequestsReceived))
                cdt.AddRow(ctr.Name, ctr.RequestsReceived, ctr.RequestsHandled);            

            sb.Append(cdt.ToString());
        }

        public void BuildSummaryStatsByUserReport(StringBuilder sb)
        {
            ConsoleDisplayTable cdt = new ConsoleDisplayTable();
            cdt.AddColumn("Name", 32);
            cdt.AddColumn("Type", 5);
            cdt.AddColumn("Req Received", 12);
            cdt.AddColumn("Req Handled", 12);
            cdt.Indent = 2;

            m_scene.ForEachScenePresence(
                sp =>
                {
                    OpenSim.Framework.Capabilities.Caps caps = m_scene.CapsModule.GetCapsForUser(sp.UUID);

                    if (caps == null)
                        return;

                    Dictionary<string, IRequestHandler> capsHandlers = caps.CapsHandlers.GetCapsHandlers();

                    int totalRequestsReceived = 0;
                    int totalRequestsHandled = 0;

                    foreach (IRequestHandler reqHandler in capsHandlers.Values)
                    {
                        totalRequestsReceived += reqHandler.RequestsReceived;
                        totalRequestsHandled += reqHandler.RequestsHandled;
                    }

                    Dictionary<string, PollServiceEventArgs> capsPollHandlers = caps.GetPollHandlers();

                    foreach (PollServiceEventArgs handler in capsPollHandlers.Values)
                    {
                        totalRequestsReceived += handler.RequestsReceived;
                        totalRequestsHandled += handler.RequestsHandled;
                    }
                    
                    cdt.AddRow(sp.Name, sp.IsChildAgent ? "child" : "root", totalRequestsReceived, totalRequestsHandled);
                }
            );

            sb.Append(cdt.ToString());
        }

        private class CapTableRow
        {
            public string Name { get; set; }
            public int RequestsReceived { get; set; }
            public int RequestsHandled { get; set; }

            public CapTableRow(string name, int requestsReceived, int requestsHandled)
            {
                Name = name;
                RequestsReceived = requestsReceived;
                RequestsHandled = requestsHandled;
            }
        }
    }
}