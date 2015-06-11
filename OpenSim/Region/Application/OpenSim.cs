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
using NDesk.Options;
using Akka;
using Akka.Actor;
using Nini.Config;
using OpenMetaverse;
using OpenSim.Framework;
using OpenSim.Framework.Console;
using OpenSim.Framework.Monitoring;
using OpenSim.Framework.Servers;
using OpenSim.Region.Framework.Interfaces;
using OpenSim.Region.Framework.Scenes;
using OpenSim.Services.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Akka.Util.Internal;
using OpenSim.Framework.Servers.HttpServer;
using OpenSim.Region.ClientStack;
using OpenSim.Region.Physics.Manager;
using OpenSim.Server.Base;
using OpenSim.Services.UserAccountService;
using Timer = System.Timers.Timer;

namespace OpenSim {
    /// <summary>
    /// Interactive OpenSim region server
    /// </summary>
    public class OpenSim : BaseOpenSimServer {

        #region definitions

        private const string PLUGIN_ASSET_CACHE = "/OpenSim/AssetCache";
        private const string PLUGIN_ASSET_SERVER_CLIENT = "/OpenSim/AssetClient";
        public const string ESTATE_SECTION_NAME = "Estates";

        /// <value>
        /// The file used to load and save prim backup xml if no filename has been specified
        /// </value>
        protected const string DEFAULT_PRIM_BACKUP_FILENAME = "prim-backup.xml";

        private static readonly ILog m_log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        protected string m_startupCommandsFile;
        protected string m_shutdownCommandsFile;
        protected bool m_gui = false;
        protected string m_consoleType = "local";
        protected uint m_consolePort = 0;

        /// <summary>
        /// Prompt to use for simulator command line.
        /// </summary>
        /// FREAKKI not used anymore ... private string m_consolePrompt;

        private string m_timedScript = "disabled";
        private int m_timeInterval = 1200;
        private Timer m_scriptTimer;
        protected string proxyUrl;
        protected int proxyOffset = 0;
        public string userStatsURI = String.Empty;
        public string managedStatsURI = String.Empty;
        protected bool m_autoCreateClientStack = true;
        protected ConfigSettings m_configSettings;
        protected ConfigurationLoader m_configLoader;
        public ConsoleCommand CreateAccount = null;
        protected List<IApplicationPlugin> m_plugins = new List<IApplicationPlugin>();
        protected EnvConfigSource m_EnvConfigSource = new EnvConfigSource();
        protected List<IClientNetworkServer> m_clientServers = new List<IClientNetworkServer>();
        protected IRegistryCore m_applicationRegistry = new RegistryCore();
        protected Dictionary<EndPoint, uint> m_clientCircuits = new Dictionary<EndPoint, uint>();
        protected NetworkServersInfo m_networkServersInfo;
        protected uint m_httpServerPort;
        protected ISimulationDataService m_simulationDataService;
        protected IEstateDataService m_estateDataService;
        protected ClientStackManager m_clientStackManager;

        public OpenSim(IConfigSource configSource)
            : base() {
            LoadConfigSettings(configSource);
        }

        public ConfigSettings ConfigurationSettings {
            get { return m_configSettings; }
            set { m_configSettings = value; }
        }

        /// <value>
        /// The config information passed into the OpenSimulator region server.
        /// </value>
        public OpenSimConfigSource ConfigSource { get; private set; }

        public List<IClientNetworkServer> ClientServers {
            get { return m_clientServers; }
        }

        public EnvConfigSource envConfigSource {
            get { return m_EnvConfigSource; }
        }

        public uint HttpServerPort {
            get { return m_httpServerPort; }
        }

        public IRegistryCore ApplicationRegistry {
            get { return m_applicationRegistry; }
        }

        public NetworkServersInfo NetServersInfo { get { return m_networkServersInfo; } }
        public ISimulationDataService SimulationDataService { get { return m_simulationDataService; } }
        public IEstateDataService EstateDataService { get { return m_estateDataService; } }


        #endregion

        #region configuration

        protected void LoadConfigSettings(IConfigSource configSource) {
            m_configLoader = new ConfigurationLoader();
            ConfigSource = m_configLoader.LoadConfigSettings(configSource, envConfigSource, out m_configSettings, out m_networkServersInfo);
            Config = ConfigSource.Source;
            ReadExtraConfigSettings();
        }

        protected void ReadExtraConfigSettings() {

            IConfig networkConfig = Config.Configs["Network"];
            if (networkConfig != null) {
                proxyUrl = networkConfig.GetString("proxy_url", "");
                proxyOffset = Int32.Parse(networkConfig.GetString("proxy_offset", "0"));
                m_consolePort = (uint)networkConfig.GetInt("console_port", 0);
            }


            IConfig startupConfig = Config.Configs["Startup"];

            int stpMinThreads = 2;
            int stpMaxThreads = 15;

            if (startupConfig != null) {
                Util.LogOverloads = startupConfig.GetBoolean("LogOverloads", true);

                m_startupCommandsFile = startupConfig.GetString("startup_console_commands_file", "startup_commands.txt");
                m_shutdownCommandsFile = startupConfig.GetString("shutdown_console_commands_file", "shutdown_commands.txt");

                if (startupConfig.GetString("console", String.Empty) == String.Empty)
                    m_gui = startupConfig.GetBoolean("gui", false);
                else
                    m_consoleType = startupConfig.GetString("console", String.Empty);

                m_timedScript = startupConfig.GetString("timer_Script", "disabled");
                if (m_timedScript != "disabled") {
                    m_timeInterval = startupConfig.GetInt("timer_Interval", 1200);
                }

                string asyncCallMethodStr = startupConfig.GetString("async_call_method", String.Empty);
                FireAndForgetMethod asyncCallMethod;
                if (!String.IsNullOrEmpty(asyncCallMethodStr) && Utils.EnumTryParse<FireAndForgetMethod>(asyncCallMethodStr, out asyncCallMethod))
                    Util.FireAndForgetMethod = asyncCallMethod;

                stpMinThreads = startupConfig.GetInt("MinPoolThreads", 15);
                stpMaxThreads = startupConfig.GetInt("MaxPoolThreads", 15);
                m_consolePrompt = startupConfig.GetString("ConsolePrompt", @"Region (\R) ");
            }

            if (Util.FireAndForgetMethod == FireAndForgetMethod.SmartThreadPool)
                Util.InitThreadPool(stpMinThreads, stpMaxThreads);

            m_log.Info("[OPENSIM MAIN]: Using async_call_method " + Util.FireAndForgetMethod);
        }

        #endregion

        #region startup

        /// <summary>
        /// Performs initialisation of the scene, such as loading configuration from disk.
        /// Performs startup specific to the region server, including initialization of the scene 
        /// such as loading configuration from disk.
        /// </summary>
        protected override void StartupSpecific() {
            m_log.Info("====================================================================");
            m_log.Info("========================= STARTING OPENSIM =========================");
            m_log.Info("====================================================================");

            //m_log.InfoFormat("[OPENSIM MAIN]: GC Is Server GC: {0}", GCSettings.IsServerGC.ToString());
            // http://msdn.microsoft.com/en-us/library/bb384202.aspx
            //GCSettings.LatencyMode = GCLatencyMode.Batch;
            //m_log.InfoFormat("[OPENSIM MAIN]: GC Latency Mode: {0}", GCSettings.LatencyMode.ToString());

            if (m_gui) // Driven by external GUI
            {
                m_console = new CommandConsole("Region");
            } else {
                switch (m_consoleType) {
                    case "basic":
                        m_console = new CommandConsole("Region");
                        break;
                    case "rest":
                        m_console = new RemoteConsole("Region");
                        ((RemoteConsole)m_console).ReadConfig(Config);
                        break;
                    default:
                        m_console = new LocalConsole("Region");
                        break;
                }
            }

            MainConsole.Instance = m_console;

            LogEnvironmentInformation();
            RegisterCommonAppenders(Config.Configs["Startup"]);
            RegisterConsoleCommands();

            IConfig startupConfig = Config.Configs["Startup"];
            if (startupConfig != null) {
                string pidFile = startupConfig.GetString("PIDFile", String.Empty);
                if (pidFile != String.Empty)
                    CreatePIDFile(pidFile);

                userStatsURI = startupConfig.GetString("Stats_URI", String.Empty);
                managedStatsURI = startupConfig.GetString("ManagedStatsRemoteFetchURI", String.Empty);
            }

            // Load the simulation data service
            IConfig simDataConfig = Config.Configs["SimulationDataStore"];
            if (simDataConfig == null)
                throw new Exception("Configuration file is missing the [SimulationDataStore] section.  Have you copied OpenSim.ini.example to OpenSim.ini to reference config-include/ files?");

            string module = simDataConfig.GetString("LocalServiceModule", String.Empty);
            if (String.IsNullOrEmpty(module))
                throw new Exception("Configuration file is missing the LocalServiceModule parameter in the [SimulationDataStore] section.");

            m_simulationDataService = ServerUtils.LoadPlugin<ISimulationDataService>(module, new object[] { Config });
            if (m_simulationDataService == null)
                throw new Exception(
                    string.Format(
                        "Could not load an ISimulationDataService implementation from {0}, as configured in the LocalServiceModule parameter of the [SimulationDataStore] config section.",
                        module));

            // Load the estate data service
            IConfig estateDataConfig = Config.Configs["EstateDataStore"];
            if (estateDataConfig == null)
                throw new Exception("Configuration file is missing the [EstateDataStore] section.  Have you copied OpenSim.ini.example to OpenSim.ini to reference config-include/ files?");

            module = estateDataConfig.GetString("LocalServiceModule", String.Empty);
            if (String.IsNullOrEmpty(module))
                throw new Exception("Configuration file is missing the LocalServiceModule parameter in the [EstateDataStore] section");

            m_estateDataService = ServerUtils.LoadPlugin<IEstateDataService>(module, new object[] { Config });
            if (m_estateDataService == null)
                throw new Exception(
                    string.Format(
                        "Could not load an IEstateDataService implementation from {0}, as configured in the LocalServiceModule parameter of the [EstateDataStore] config section.",
                        module));



            MainServer.SceneManager = MainServer.ActorSystem.ActorOf<SceneManager>("SceneManager");
            m_clientStackManager = CreateClientStackManager();

            Initialize();

            m_httpServer
                = new BaseHttpServer(
                    m_httpServerPort, m_networkServersInfo.HttpUsesSSL, m_networkServersInfo.httpSSLPort,
                    m_networkServersInfo.HttpSSLCN);

            if (m_networkServersInfo.HttpUsesSSL && (m_networkServersInfo.HttpListenerPort == m_networkServersInfo.httpSSLPort)) {
                m_log.Error("[REGION SERVER]: HTTP Server config failed.   HTTP Server and HTTPS server must be on different ports");
            }

            m_log.InfoFormat("[REGION SERVER]: Starting HTTP server on port {0}", m_httpServerPort);
            m_httpServer.Start();

            MainServer.AddHttpServer(m_httpServer);
            MainServer.Instance = m_httpServer;

            // "OOB" Server
            if (m_networkServersInfo.ssl_listener) {
                BaseHttpServer server = new BaseHttpServer(
                    m_networkServersInfo.https_port, m_networkServersInfo.ssl_listener, m_networkServersInfo.cert_path,
                    m_networkServersInfo.cert_pass);

                m_log.InfoFormat("[REGION SERVER]: Starting HTTPS server on port {0}", server.Port);
                MainServer.AddHttpServer(server);
                server.Start();
            }

            base.StartupSpecific();

            LoadPlugins();
            foreach (IApplicationPlugin plugin in m_plugins) {
                plugin.PostInitialise();
            }

            if (m_console != null)
                AddPluginCommands(m_console);

            MainServer.Instance.AddStreamHandler(new SimStatusHandler());
            MainServer.Instance.AddStreamHandler(new XSimStatusHandler(this));

            if (userStatsURI != String.Empty)
                MainServer.Instance.AddStreamHandler(new UXSimStatusHandler(this));

            if (managedStatsURI != String.Empty) {
                string urlBase = String.Format("/{0}/", managedStatsURI);
                MainServer.Instance.AddHTTPHandler(urlBase, StatsManager.HandleStatsRequest);
                m_log.InfoFormat("[OPENSIM] Enabling remote managed stats fetch. URL = {0}", urlBase);
            }

            if (m_console is RemoteConsole) {
                if (m_consolePort == 0) {
                    ((RemoteConsole)m_console).SetServer(m_httpServer);
                } else {
                    ((RemoteConsole)m_console).SetServer(MainServer.GetHttpServer(m_consolePort));
                }
            }

            // Hook up to the watchdog timer
            Watchdog.OnWatchdogTimeout += WatchdogTimeoutHandler;

            PrintFileToConsole("startuplogo.txt");

            // For now, start at the 'root' level by default
            // FREAKKI if (SceneManager.Scenes.Count == 1) // If there is only one region, select it
            //    ChangeSelectedRegion("region",
            //                         new string[] {"change", "region", SceneManager.Scenes[0].RegionInfo.RegionName});
            //else
            //    ChangeSelectedRegion("region", new string[] {"change", "region", "root"});

            //Run Startup Commands
            if (String.IsNullOrEmpty(m_startupCommandsFile)) {
                m_log.Info("[STARTUP]: No startup command script specified. Moving on...");
            } else {
                RunCommandScript(m_startupCommandsFile);
            }

            // Start timer script (run a script every xx seconds)
            if (m_timedScript != "disabled") {
                m_scriptTimer = new Timer();
                m_scriptTimer.Enabled = true;
                m_scriptTimer.Interval = m_timeInterval * 1000;
                m_scriptTimer.Elapsed += RunAutoTimerScript;
            }
        }

        protected virtual void LoadPlugins() {
            IConfig startupConfig = Config.Configs["Startup"];
            string registryLocation = (startupConfig != null) ? startupConfig.GetString("RegistryLocation", String.Empty) : String.Empty;

            // The location can also be specified in the environment. If there
            // is no location in the configuration, we must call the constructor
            // without a location parameter to allow that to happen.
            if (registryLocation == String.Empty) {
                using (PluginLoader<IApplicationPlugin> loader = new PluginLoader<IApplicationPlugin>(new ApplicationPluginInitialiser(this))) {
                    loader.Load("/OpenSim/Startup");
                    m_plugins = loader.Plugins;
                }
            } else {
                using (PluginLoader<IApplicationPlugin> loader = new PluginLoader<IApplicationPlugin>(new ApplicationPluginInitialiser(this), registryLocation)) {
                    loader.Load("/OpenSim/Startup");
                    m_plugins = loader.Plugins;
                }
            }
        }

        protected void Initialize() {
            // Called from base.StartUp()

            m_httpServerPort = m_networkServersInfo.HttpListenerPort;
            // FREAKKI SceneManager.OnRestartSim += HandleRestartRegion;

            // Only enable the watchdogs when all regions are ready.  Otherwise we get false positives when cpu is
            // heavily used during initial startup.
            //
            // FIXME: It's also possible that region ready status should be flipped during an OAR load since this
            // also makes heavy use of the CPU.
            // FREAKKI SceneManager.OnRegionsReadyStatusChange
            //    += sm => { MemoryWatchdog.Enabled = sm.AllRegionsReady; Watchdog.Enabled = sm.AllRegionsReady; };
        }

        #endregion

        #region region methods

        /// <summary>
        /// Execute the region creation process.  This includes setting up scene infrastructure.
        /// </summary>
        /// <param name="regionInfo"></param>
        /// <param name="portadd_flag"></param>
        /// <returns></returns>
        public List<IClientNetworkServer> CreateRegion(RegionInfo regionInfo, bool portadd_flag, out IScene scene) {
            return CreateRegion(regionInfo, portadd_flag, false, out scene);
        }

        /// <summary>
        /// Execute the region creation process.  This includes setting up scene infrastructure.
        /// </summary>
        /// <param name="regionInfo"></param>
        /// <returns></returns>
        public List<IClientNetworkServer> CreateRegion(RegionInfo regionInfo, out IScene scene) {
            return CreateRegion(regionInfo, false, true, out scene);
        }

        /// <summary>
        /// Execute the region creation process.  This includes setting up scene infrastructure.
        /// </summary>
        /// <param name="regionInfo"></param>
        /// <param name="portadd_flag"></param>
        /// <param name="do_post_init"></param>
        /// <returns></returns>
        public List<IClientNetworkServer> CreateRegion(RegionInfo regionInfo, bool portadd_flag, bool do_post_init, out IScene mscene) {
            int port = regionInfo.InternalEndPoint.Port;

            // set initial RegionID to originRegionID in RegionInfo. (it needs for loding prims)
            // Commented this out because otherwise regions can't register with
            // the grid as there is already another region with the same UUID
            // at those coordinates. This is required for the load balancer to work.
            // --Mike, 2009.02.25
            //regionInfo.originRegionID = regionInfo.RegionID;

            // set initial ServerURI
            regionInfo.HttpPort = m_httpServerPort;
            regionInfo.ServerURI = "http://" + regionInfo.ExternalHostName + ":" + regionInfo.HttpPort.ToString() + "/";

            regionInfo.osSecret = m_osSecret;

            if ((proxyUrl.Length > 0) && (portadd_flag)) {
                // set proxy url to RegionInfo
                regionInfo.proxyUrl = proxyUrl;
                regionInfo.ProxyOffset = proxyOffset;
                Util.XmlRpcCommand(proxyUrl, "AddPort", port, port + proxyOffset, regionInfo.ExternalHostName);
            }

            List<IClientNetworkServer> clientServers;
            Scene scene = SetupScene(regionInfo, proxyOffset, Config, out clientServers);

            m_log.Info("[MODULES]: Loading Region's modules (old style)");

            // Use this in the future, the line above will be deprecated soon
            m_log.Info("[REGIONMODULES]: Loading Region's modules (new style)");
            IRegionModulesController controller;
            if (ApplicationRegistry.TryGet(out controller)) {
                controller.AddRegionToModules(scene);
            } else m_log.Error("[REGIONMODULES]: The new RegionModulesController is missing...");

            scene.SetModuleInterfaces();

            while (regionInfo.EstateSettings.EstateOwner == UUID.Zero && MainConsole.Instance != null)
                SetUpEstateOwner(scene);

            // Prims have to be loaded after module configuration since some modules may be invoked during the load
            scene.LoadPrimsFromStorage(regionInfo.originRegionID);

            // TODO : Try setting resource for region xstats here on scene
            MainServer.Instance.AddStreamHandler(new RegionStatsHandler(regionInfo));

            scene.loadAllLandObjectsFromStorage(regionInfo.originRegionID);
            scene.EventManager.TriggerParcelPrimCountUpdate();

            try {
                scene.RegisterRegionWithGrid();
            } catch (Exception e) {
                m_log.ErrorFormat(
                    "[STARTUP]: Registration of region with grid failed, aborting startup due to {0} {1}",
                    e.Message, e.StackTrace);

                // Carrying on now causes a lot of confusion down the
                // line - we need to get the user's attention
                Environment.Exit(1);
            }

            // We need to do this after we've initialized the
            // scripting engines.
            scene.CreateScriptInstances();

            MainServer.SceneManager.Tell(new SceneAddMessage(scene));

            if (m_autoCreateClientStack) {
                foreach (IClientNetworkServer clientserver in clientServers) {
                    m_clientServers.Add(clientserver);
                    clientserver.Start();
                }
            }

            scene.EventManager.OnShutdown += delegate() { ShutdownRegion(scene); };

            mscene = scene;

            return clientServers;
        }

        /// <summary>
        /// Remove a region from the simulator without deleting it permanently.
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        public void CloseRegion(Scene scene) {
            //// only need to check this if we are not at the
            //// root level
            // FREAKKI if ((SceneManager.CurrentScene != null) &&
            //    (SceneManager.CurrentScene.RegionInfo.RegionID == scene.RegionInfo.RegionID))
            //{
            //    SceneManager.TrySetCurrentScene("..");
            //}

            //SceneManager.CloseScene(scene);
            //ShutdownClientServer(scene.RegionInfo);
        }

        /// <summary>
        /// Remove a region from the simulator without deleting it permanently.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public void CloseRegion(string name) {
            // FREAKKI Scene target;
            //if (SceneManager.TryGetScene(name, out target))
            //    CloseRegion(target);
        }

        private void ShutdownRegion(Scene scene) {
            m_log.DebugFormat("[SHUTDOWN]: Shutting down region {0}", scene.RegionInfo.RegionName);
            IRegionModulesController controller;
            if (ApplicationRegistry.TryGet<IRegionModulesController>(out controller)) {
                controller.RemoveRegionFromModules(scene);
            }
        }

        public void RemoveRegion(Scene scene, bool cleanup) {
            //// only need to check this if we are not at the
            //// root level
            //if ((SceneManager.CurrentScene != null) &&
            //    (SceneManager.CurrentScene.RegionInfo.RegionID == scene.RegionInfo.RegionID))
            //{
            //    SceneManager.TrySetCurrentScene("..");
            //}

            //if (cleanup)
            //{
            //    // only allow the console comand delete-region to remove objects from DB
            //    scene.DeleteAllSceneObjects();
            //}
            // FREAKKI SceneManager.CloseScene(scene);
            //ShutdownClientServer(scene.RegionInfo);

            //if (!cleanup)
            //    return;

            //if (!String.IsNullOrEmpty(scene.RegionInfo.RegionFile))
            //{
            //    if (scene.RegionInfo.RegionFile.ToLower().EndsWith(".xml"))
            //    {
            //        File.Delete(scene.RegionInfo.RegionFile);
            //        m_log.InfoFormat("[OPENSIM]: deleting region file \"{0}\"", scene.RegionInfo.RegionFile);
            //    }
            //    if (scene.RegionInfo.RegionFile.ToLower().EndsWith(".ini"))
            //    {
            //        try
            //        {
            //            IniConfigSource source = new IniConfigSource(scene.RegionInfo.RegionFile);
            //            if (source.Configs[scene.RegionInfo.RegionName] != null)
            //            {
            //                source.Configs.Remove(scene.RegionInfo.RegionName);

            //                if (source.Configs.Count == 0)
            //                {
            //                    File.Delete(scene.RegionInfo.RegionFile);
            //                }
            //                else
            //                {
            //                    source.Save(scene.RegionInfo.RegionFile);
            //                }
            //            }
            //        }
            //        catch (Exception)
            //        {
            //        }
            //    }
            //}
        }

        public void RemoveRegion(string name, bool cleanUp) {
            // FREAKKI Scene target;
            //if (SceneManager.TryGetScene(name, out target))
            //    RemoveRegion(target, cleanUp);
        }


        #endregion

        #region scene methods

        /// <summary>
        /// Create a scene and its initial base structures.
        /// </summary>
        /// <param name="regionInfo"></param>
        /// <param name="clientServer"> </param>
        /// <returns></returns>
        protected Scene SetupScene(RegionInfo regionInfo, out List<IClientNetworkServer> clientServer) {
            return SetupScene(regionInfo, 0, null, out clientServer);
        }

        /// <summary>
        /// Create a scene and its initial base structures.
        /// </summary>
        /// <param name="regionInfo"></param>
        /// <param name="proxyOffset"></param>
        /// <param name="configSource"></param>
        /// <param name="clientServer"> </param>
        /// <returns></returns>
        protected Scene SetupScene(
            RegionInfo regionInfo, int proxyOffset, IConfigSource configSource, out List<IClientNetworkServer> clientServer) {
            List<IClientNetworkServer> clientNetworkServers = null;

            AgentCircuitManager circuitManager = new AgentCircuitManager();
            IPAddress listenIP = regionInfo.InternalEndPoint.Address;
            //if (!IPAddress.TryParse(regionInfo.InternalEndPoint, out listenIP))
            //    listenIP = IPAddress.Parse("0.0.0.0");

            uint port = (uint)regionInfo.InternalEndPoint.Port;

            if (m_autoCreateClientStack) {
                clientNetworkServers = m_clientStackManager.CreateServers(
                    listenIP, ref port, proxyOffset, regionInfo.m_allow_alternate_ports, configSource,
                    circuitManager);
            } else {
                clientServer = null;
            }

            regionInfo.InternalEndPoint.Port = (int)port;

            Scene scene = CreateScene(regionInfo, m_simulationDataService, m_estateDataService, circuitManager);

            if (m_autoCreateClientStack) {
                foreach (IClientNetworkServer clientnetserver in clientNetworkServers) {
                    clientnetserver.AddScene(scene);
                }
            }
            clientServer = clientNetworkServers;
            scene.LoadWorldMap();

            Vector3 regionExtent = new Vector3(regionInfo.RegionSizeX, regionInfo.RegionSizeY, regionInfo.RegionSizeZ);
            scene.PhysicsScene = GetPhysicsScene(scene.RegionInfo.RegionName, regionExtent);
            scene.PhysicsScene.RequestAssetMethod = scene.PhysicsRequestAsset;
            scene.PhysicsScene.SetTerrain(scene.Heightmap.GetFloatsSerialised());
            scene.PhysicsScene.SetWaterLevel((float)regionInfo.RegionSettings.WaterHeight);

            return scene;
        }

        protected Scene CreateScene(RegionInfo regionInfo, ISimulationDataService simDataService,
            IEstateDataService estateDataService, AgentCircuitManager circuitManager) {

            SceneCommunicationService sceneGridService = new SceneCommunicationService();

            return new Scene(
                regionInfo, circuitManager, sceneGridService,
                simDataService, estateDataService,
                Config, m_version);
        }

        #endregion

        #region estate methods

        /// <summary>
        /// Create an estate with an initial region.
        /// </summary>
        /// <remarks>
        /// This method doesn't allow an estate to be created with the same name as existing estates.
        /// </remarks>
        /// <param name="regInfo"></param>
        /// <param name="estatesByName">A list of estate names that already exist.</param>
        /// <param name="estateName">Estate name to create if already known</param>
        /// <returns>true if the estate was created, false otherwise</returns>
        public bool CreateEstate(RegionInfo regInfo, Dictionary<string, EstateSettings> estatesByName, string estateName) {
            // Create a new estate
            regInfo.EstateSettings = EstateDataService.LoadEstateSettings(regInfo.RegionID, true);

            string newName;
            if (!string.IsNullOrEmpty(estateName))
                newName = estateName;
            else
                newName = MainConsole.Instance.CmdPrompt("New estate name", regInfo.EstateSettings.EstateName);

            if (estatesByName.ContainsKey(newName)) {
                MainConsole.Instance.OutputFormat("An estate named {0} already exists.  Please try again.", newName);
                return false;
            }

            regInfo.EstateSettings.EstateName = newName;

            // FIXME: Later on, the scene constructor will reload the estate settings no matter what.
            // Therefore, we need to do an initial save here otherwise the new estate name will be reset
            // back to the default.  The reloading of estate settings by scene could be eliminated if it
            // knows that the passed in settings in RegionInfo are already valid.  Also, it might be 
            // possible to eliminate some additional later saves made by callers of this method.
            regInfo.EstateSettings.Save();

            return true;
        }

        /// <summary>
        /// Load the estate information for the provided RegionInfo object.
        /// </summary>
        /// <param name="regInfo"></param>
        public bool PopulateRegionEstateInfo(RegionInfo regInfo) {
            if (EstateDataService != null)
                regInfo.EstateSettings = EstateDataService.LoadEstateSettings(regInfo.RegionID, false);

            if (regInfo.EstateSettings.EstateID != 0)
                return false;	// estate info in the database did not change

            m_log.WarnFormat("[ESTATE] Region {0} is not part of an estate.", regInfo.RegionName);

            List<EstateSettings> estates = EstateDataService.LoadEstateSettingsAll();
            Dictionary<string, EstateSettings> estatesByName = new Dictionary<string, EstateSettings>();

            foreach (EstateSettings estate in estates)
                estatesByName[estate.EstateName] = estate;

            string defaultEstateName = null;

            if (Config.Configs[ESTATE_SECTION_NAME] != null) {
                defaultEstateName = Config.Configs[ESTATE_SECTION_NAME].GetString("DefaultEstateName", null);

                if (defaultEstateName != null) {
                    EstateSettings defaultEstate;
                    bool defaultEstateJoined = false;

                    if (estatesByName.ContainsKey(defaultEstateName)) {
                        defaultEstate = estatesByName[defaultEstateName];

                        if (EstateDataService.LinkRegion(regInfo.RegionID, (int)defaultEstate.EstateID))
                            defaultEstateJoined = true;
                    } else {
                        if (CreateEstate(regInfo, estatesByName, defaultEstateName))
                            defaultEstateJoined = true;
                    }

                    if (defaultEstateJoined)
                        return true; // need to update the database
                    else
                        m_log.ErrorFormat(
                            "[OPENSIM BASE]: Joining default estate {0} failed", defaultEstateName);
                }
            }

            // If we have no default estate or creation of the default estate failed then ask the user.
            while (true) {
                if (estates.Count == 0) {
                    m_log.Info("[ESTATE]: No existing estates found.  You must create a new one.");

                    if (CreateEstate(regInfo, estatesByName, null))
                        break;
                    else
                        continue;
                } else {
                    string response
                        = MainConsole.Instance.CmdPrompt(
                            string.Format(
                                "Do you wish to join region {0} to an existing estate (yes/no)?", regInfo.RegionName),
                            "yes",
                            new List<string>() { "yes", "no" });

                    if (response == "no") {
                        if (CreateEstate(regInfo, estatesByName, null))
                            break;
                        else
                            continue;
                    } else {
                        string[] estateNames = estatesByName.Keys.ToArray();
                        response
                            = MainConsole.Instance.CmdPrompt(
                                string.Format(
                                    "Name of estate to join.  Existing estate names are ({0})",
                                    string.Join(", ", estateNames)),
                                estateNames[0]);

                        List<int> estateIDs = EstateDataService.GetEstates(response);
                        if (estateIDs.Count < 1) {
                            MainConsole.Instance.Output("The name you have entered matches no known estate.  Please try again.");
                            continue;
                        }

                        int estateId = estateIDs[0];

                        regInfo.EstateSettings = EstateDataService.LoadEstateSettings(estateId);

                        if (EstateDataService.LinkRegion(regInfo.RegionID, estateId))
                            break;

                        MainConsole.Instance.Output("Joining the estate failed. Please try again.");
                    }
                }
            }

            return true;	// need to update the database
        }

        /// <summary>
        /// Try to set up the estate owner for the given scene.
        /// </summary>
        /// <remarks>
        /// The involves asking the user for information about the user on the console.  If the user does not already
        /// exist then it is created.
        /// </remarks>
        /// <param name="scene"></param>
        private void SetUpEstateOwner(Scene scene) {
            RegionInfo regionInfo = scene.RegionInfo;

            string estateOwnerFirstName = null;
            string estateOwnerLastName = null;
            string estateOwnerEMail = null;
            string estateOwnerPassword = null;
            string rawEstateOwnerUuid = null;

            if (Config.Configs[ESTATE_SECTION_NAME] != null) {
                string defaultEstateOwnerName
                    = Config.Configs[ESTATE_SECTION_NAME].GetString("DefaultEstateOwnerName", "").Trim();
                string[] ownerNames = defaultEstateOwnerName.Split(' ');

                if (ownerNames.Length >= 2) {
                    estateOwnerFirstName = ownerNames[0];
                    estateOwnerLastName = ownerNames[1];
                }

                // Info to be used only on Standalone Mode
                rawEstateOwnerUuid = Config.Configs[ESTATE_SECTION_NAME].GetString("DefaultEstateOwnerUUID", null);
                estateOwnerEMail = Config.Configs[ESTATE_SECTION_NAME].GetString("DefaultEstateOwnerEMail", null);
                estateOwnerPassword = Config.Configs[ESTATE_SECTION_NAME].GetString("DefaultEstateOwnerPassword", null);
            }

            MainConsole.Instance.OutputFormat("Estate {0} has no owner set.", regionInfo.EstateSettings.EstateName);
            List<char> excluded = new List<char>(new char[1] { ' ' });


            if (estateOwnerFirstName == null || estateOwnerLastName == null) {
                estateOwnerFirstName = MainConsole.Instance.CmdPrompt("Estate owner first name", "Test", excluded);
                estateOwnerLastName = MainConsole.Instance.CmdPrompt("Estate owner last name", "User", excluded);
            }

            UserAccount account
                = scene.UserAccountService.GetUserAccount(regionInfo.ScopeID, estateOwnerFirstName, estateOwnerLastName);

            if (account == null) {

                // XXX: The LocalUserAccountServicesConnector is currently registering its inner service rather than
                // itself!
                //                    if (scene.UserAccountService is LocalUserAccountServicesConnector)
                //                    {
                //                        IUserAccountService innerUas
                //                            = ((LocalUserAccountServicesConnector)scene.UserAccountService).UserAccountService;
                //
                //                        m_log.DebugFormat("B {0}", innerUas.GetType());
                //
                //                        if (innerUas is UserAccountService)
                //                        {

                if (scene.UserAccountService is UserAccountService) {
                    if (estateOwnerPassword == null)
                        estateOwnerPassword = MainConsole.Instance.PasswdPrompt("Password");

                    if (estateOwnerEMail == null)
                        estateOwnerEMail = MainConsole.Instance.CmdPrompt("Email");

                    if (rawEstateOwnerUuid == null)
                        rawEstateOwnerUuid = MainConsole.Instance.CmdPrompt("User ID", UUID.Random().ToString());

                    UUID estateOwnerUuid = UUID.Zero;
                    if (!UUID.TryParse(rawEstateOwnerUuid, out estateOwnerUuid)) {
                        m_log.ErrorFormat("[OPENSIM]: ID {0} is not a valid UUID", rawEstateOwnerUuid);
                        return;
                    }

                    // If we've been given a zero uuid then this signals that we should use a random user id
                    if (estateOwnerUuid == UUID.Zero)
                        estateOwnerUuid = UUID.Random();

                    account
                        = ((UserAccountService)scene.UserAccountService).CreateUser(
                            regionInfo.ScopeID,
                            estateOwnerUuid,
                            estateOwnerFirstName,
                            estateOwnerLastName,
                            estateOwnerPassword,
                            estateOwnerEMail);
                }
            }

            if (account == null) {
                m_log.ErrorFormat(
                    "[OPENSIM]: Unable to store account. If this simulator is connected to a grid, you must create the estate owner account first at the grid level.");
            } else {
                regionInfo.EstateSettings.EstateOwner = account.PrincipalID;
                regionInfo.EstateSettings.Save();
            }
        }

        #endregion

        #region physics scene
        protected PhysicsScene GetPhysicsScene(string osSceneIdentifier, Vector3 regionExtent) {
            return GetPhysicsScene(
                m_configSettings.PhysicsEngine, m_configSettings.MeshEngineName, Config, osSceneIdentifier, regionExtent);
        }

        /// <summary>
        /// Get a new physics scene.
        /// </summary>
        /// <param name="engine">The name of the physics engine to use</param>
        /// <param name="meshEngine">The name of the mesh engine to use</param>
        /// <param name="config">The configuration data to pass to the physics and mesh engines</param>
        /// <param name="osSceneIdentifier">
        /// The name of the OpenSim scene this physics scene is serving.  This will be used in log messages.
        /// </param>
        /// <returns></returns>
        protected PhysicsScene GetPhysicsScene(
            string engine, string meshEngine, IConfigSource config, string osSceneIdentifier, Vector3 regionExtent) {
            PhysicsPluginManager physicsPluginManager;
            physicsPluginManager = new PhysicsPluginManager();
            physicsPluginManager.LoadPluginsFromAssemblies("Physics");

            return physicsPluginManager.GetPhysicsScene(engine, meshEngine, config, osSceneIdentifier, regionExtent);
        }
        #endregion

        #region client stack manager

        protected ClientStackManager CreateClientStackManager() {
            return new ClientStackManager(m_configSettings.ClientstackDll);
        }

        #endregion

        #region console commands

        /// <summary>
        /// Register standard set of region console commands
        /// </summary>
        private void RegisterConsoleCommands() {

            //m_console.Commands.AddCommand("Objects", false, "force update",
            //                              "force update",
            //                              "Force the update of all objects on clients",
            // FREAKKI                            HandleForceUpdate);

            //m_console.Commands.AddCommand("General", false, "change region",
            //                              "change region <region name>",
            //                              "Change current console region", 
            // FREAKKI                             ChangeSelectedRegion);

            //            m_console.Commands.AddCommand("Archiving", false, "save xml2",
            //                                          "save xml2",
            //                                          "Save a region's data in XML2 format", 
            // FREAKKI                                         SaveXml2);

            //            m_console.Commands.AddCommand("Archiving", false, "load xml2",
            //                                          "load xml2",
            //                                          "Load a region's data from XML2 format", 
            // FREAKKI                                         LoadXml2);

            //            m_console.Commands.AddCommand("Archiving", false, "save prims xml2",
            //                                          "save prims xml2 [<prim name> <file name>]",
            //                                          "Save named prim to XML2", 
            // FREAKKI                                         SavePrimsXml2);



            //m_console.Commands.AddCommand("Objects", false, "edit scale",
            //                              "edit scale <name> <x> <y> <z>",
            //                              "Change the scale of a named prim", 
            // FREAKKI                             HandleEditScale);

            //m_console.Commands.AddCommand("Objects", false, "rotate scene",
            //                              "rotate scene <degrees> [centerX, centerY]",
            //                              "Rotates all scene objects around centerX, centerY (defailt 128, 128) (please back up your region before using)",
            // FREAKKI                             HandleRotateScene);

            //m_console.Commands.AddCommand("Objects", false, "scale scene",
            //                              "scale scene <factor>",
            //                              "Scales the scene objects (please back up your region before using)",
            // FREAKKI                             HandleScaleScene);

            //m_console.Commands.AddCommand("Objects", false, "translate scene",
            //                              "translate scene xOffset yOffset zOffset",
            //                              "translates the scene objects (please back up your region before using)",
            // FREAKKI                             HandleTranslateScene);

            // m_console.Commands.AddCommand("Users", false, "kick user",
            //                              "kick user <first> <last> [--force] [message]",
            //                              "Kick a user off the simulator",
            //                              "The --force option will kick the user without any checks to see whether it's already in the process of closing\n"
            //                              + "Only use this option if you are sure the avatar is inactive and a normal kick user operation does not removed them",
            // FREAKKI                             KickUserCommand);

            //m_console.Commands.AddCommand("Users", false, "show users",
            //                              "show users [full]",
            //                              "Show user data for users currently on the region", 
            //                              "Without the 'full' option, only users actually on the region are shown."
            //                                + "  With the 'full' option child agents of users in neighbouring regions are also shown.",
            // FREAKKI                             HandleShow);

            //m_console.Commands.AddCommand("Comms", false, "show connections",
            //                              "show connections",
            //                              "Show connection data", 
            // FREAKKI                             HandleShow);

            //m_console.Commands.AddCommand("Comms", false, "show circuits",
            //                              "show circuits",
            //                              "Show agent circuit data", 
            // FREAKKI                             HandleShow);

            //m_console.Commands.AddCommand("Comms", false, "show pending-objects",
            //                              "show pending-objects",
            //                              "Show # of objects on the pending queues of all scene viewers", 
            // FREAKKI                              HandleShow);

            //m_console.Commands.AddCommand("General", false, "show modules",
            //                              "show modules",
            //                              "Show module data", 
            // FREAKKI                             HandleShow);

            //m_console.Commands.AddCommand("Regions", false, "show regions",
            //                              "show regions",
            //                              "Show region data", 
            // FREAKKI                             HandleShow);

            //m_console.Commands.AddCommand("Regions", false, "show ratings",
            //                              "show ratings",
            //                              "Show rating data", 
            // FREAKKI                             HandleShow);

            //m_console.Commands.AddCommand("Objects", false, "backup",
            //                              "backup",
            //                              "Persist currently unsaved object changes immediately instead of waiting for the normal persistence call.", 
            // FREAKKI                             RunCommand);

            //m_console.Commands.AddCommand("Regions", false, "create region",
            //                              "create region [\"region name\"] <region_file.ini>",
            //                              "Create a new region.",
            //                              "The settings for \"region name\" are read from <region_file.ini>. Paths specified with <region_file.ini> are relative to your Regions directory, unless an absolute path is given."
            //                              + " If \"region name\" does not exist in <region_file.ini>, it will be added." + Environment.NewLine
            //                              + "Without \"region name\", the first region found in <region_file.ini> will be created." + Environment.NewLine
            //                              + "If <region_file.ini> does not exist, it will be created.",
            // FREAKKI                             HandleCreateRegion);

            //m_console.Commands.AddCommand("Regions", false, "restart",
            //                              "restart",
            //                              "Restart all sims in this instance", 
            // FREAKKI                             RunCommand);

            //m_console.Commands.AddCommand("General", false, "command-script",
            //                              "command-script <script>",
            //                              "Run a command script from file", 
            // FREAKKI                             RunCommand);

            //m_console.Commands.AddCommand("Regions", false, "remove-region",
            //                              "remove-region <name>",
            //                              "Remove a region from this simulator", 
            // FREAKKI                             RunCommand);

            //m_console.Commands.AddCommand("Regions", false, "delete-region",
            //                              "delete-region <name>",
            //                              "Delete a region from disk", 
            // FREAKKI                             RunCommand);

            //m_console.Commands.AddCommand("Estates", false, "estate create",
            //                              "estate create <owner UUID> <estate name>",
            //                              "Creates a new estate with the specified name, owned by the specified user."
            //                              + " Estate name must be unique.",
            // FREAKKI                             CreateEstateCommand);

            //m_console.Commands.AddCommand("Estates", false, "estate set owner",
            //                              "estate set owner <estate-id>[ <UUID> | <Firstname> <Lastname> ]",
            //                              "Sets the owner of the specified estate to the specified UUID or user. ",
            // FREAKKI                             SetEstateOwnerCommand);

            //m_console.Commands.AddCommand("Estates", false, "estate set name",
            //                              "estate set name <estate-id> <new name>",
            //                              "Sets the name of the specified estate to the specified value. New name must be unique.",
            // FREAKKI                             SetEstateNameCommand);

            //m_console.Commands.AddCommand("Estates", false, "estate link region",
            //                              "estate link region <estate ID> <region ID>",
            //                              "Attaches the specified region to the specified estate.",
            // FREAKKI                             EstateLinkRegionCommand);
            m_console.Commands.AddCommand(
                "Comms", false, "show http-handlers",
                "show http-handlers",
                "Show all registered http handlers",
                HandleShowHttpHandlersCommand);

            m_console.Commands.AddCommand(
                "Debug", false, "debug http", "debug http <in|out|all> [<level>]",
                "Turn on http request logging.",
                "If in or all and\n"
                    + "  level <= 0 then no extra logging is done.\n"
                    + "  level >= 1 then short warnings are logged when receiving bad input data.\n"
                    + "  level >= 2 then long warnings are logged when receiving bad input data.\n"
                    + "  level >= 3 then short notices about all incoming non-poll HTTP requests are logged.\n"
                    + "  level >= 4 then the time taken to fulfill the request is logged.\n"
                    + "  level >= 5 then a sample from the beginning of the data is logged.\n"
                    + "  level >= 6 then the entire data is logged.\n"
                    + "  no level is specified then the current level is returned.\n\n"
                    + "If out or all and\n"
                    + "  level >= 3 then short notices about all outgoing requests going through WebUtil are logged.\n"
                    + "  level >= 4 then the time taken to fulfill the request is logged.\n"
                    + "  level >= 5 then a sample from the beginning of the data is logged.\n"
                    + "  level >= 6 then the entire data is logged.\n",
                HandleDebugHttpCommand);

        }

        protected virtual void AddPluginCommands(ICommandConsole console) {
            // FREAKKI List<string> topics = GetHelpTopics();

            //foreach (string topic in topics)
            //{
            //    string capitalizedTopic = char.ToUpper(topic[0]) + topic.Substring(1);

            //    // This is a hack to allow the user to enter the help command in upper or lowercase.  This will go
            //    // away at some point.
            //    console.Commands.AddCommand(capitalizedTopic, false, "help " + topic,
            //                                  "help " + capitalizedTopic,
            //                                  "Get help on plugin command '" + topic + "'",
            //                                  HandleCommanderHelp);
            //    console.Commands.AddCommand(capitalizedTopic, false, "help " + capitalizedTopic,
            //                                  "help " + capitalizedTopic,
            //                                  "Get help on plugin command '" + topic + "'",
            //                                  HandleCommanderHelp);

            //    ICommander commander = null;

            //    Scene s = SceneManager.CurrentOrFirstScene;

            //    if (s != null && s.GetCommanders() != null)
            //    {
            //        if (s.GetCommanders().ContainsKey(topic))
            //            commander = s.GetCommanders()[topic];
            //    }

            //    if (commander == null)
            //        continue;

            //    foreach (string command in commander.Commands.Keys)
            //    {
            //        console.Commands.AddCommand(capitalizedTopic, false,
            //                                      topic + " " + command,
            //                                      topic + " " + commander.Commands[command].ShortHelp(),
            //                                      String.Empty, HandleCommanderCommand);
            //    }
            //}
        }

        protected override List<string> GetHelpTopics() {
            // FREAKKI List<string> topics = base.GetHelpTopics();
            //Scene s = SceneManager.CurrentOrFirstScene;
            //if (s != null && s.GetCommanders() != null)
            //    topics.AddRange(s.GetCommanders().Keys);

            //return topics;
            throw new FreAkkiRefactoringException("GetHelpTopics");
        }

        /// <summary>
        /// Kicks users off the region
        /// </summary>
        /// <param name="module"></param>
        /// <param name="cmdparams">name of avatar to kick</param>
        private void KickUserCommand(string module, string[] cmdparams) {
            //bool force = false;

            //OptionSet options = new OptionSet().Add("f|force", delegate (string v) { force = v != null; });

            //List<string> mainParams = options.Parse(cmdparams);

            //if (mainParams.Count < 4)
            //    return;

            //string alert = null;
            //if (mainParams.Count > 4)
            //    alert = String.Format("\n{0}\n", String.Join(" ", cmdparams, 4, cmdparams.Length - 4));

            //IList agents = SceneManager.GetCurrentSceneAvatars();

            //foreach (ScenePresence presence in agents)
            //{
            //    RegionInfo regionInfo = presence.Scene.RegionInfo;

            //    if (presence.Firstname.ToLower().Equals(mainParams[2].ToLower()) &&
            //        presence.Lastname.ToLower().Equals(mainParams[3].ToLower()))
            //    {
            //        MainConsole.Instance.Output(
            //            String.Format(
            //                "Kicking user: {0,-16} {1,-16} {2,-37} in region: {3,-16}",
            //                presence.Firstname, presence.Lastname, presence.UUID, regionInfo.RegionName));

            //        // kick client...
            //        if (alert != null)
            //            presence.ControllingClient.Kick(alert);
            //        else
            //            presence.ControllingClient.Kick("\nThe OpenSim manager kicked you out.\n");

            //        presence.Scene.CloseAgent(presence.UUID, force);
            //        break;
            //    }
            //}

            //MainConsole.Instance.Output("");
            throw new FreAkkiRefactoringException("KickUserCommand");
        }

        /// <summary>
        /// Opens a file and uses it as input to the console command parser.
        /// </summary>
        /// <param name="fileName">name of file to use as input to the console</param>
        private static void PrintFileToConsole(string fileName) {
            if (File.Exists(fileName)) {
                using (StreamReader readFile = File.OpenText(fileName)) {
                    string currentLine;
                    while ((currentLine = readFile.ReadLine()) != null) {
                        m_log.Info("[!]" + currentLine);
                    }
                }
            }
        }

        /// <summary>
        /// Force resending of all updates to all clients in active region(s)
        /// </summary>
        /// <param name="module"></param>
        /// <param name="args"></param>
        private void HandleForceUpdate(string module, string[] args) {
            //MainConsole.Instance.Output("Updating all clients");
            // FREAKKI SceneManager.ForceCurrentSceneClientUpdate();
            throw new FreAkkiRefactoringException("HandleForceUpdate");
        }

        /// <summary>
        /// Edits the scale of a primative with the name specified
        /// </summary>
        /// <param name="module"></param>
        /// <param name="args">0,1, name, x, y, z</param>
        private void HandleEditScale(string module, string[] args) {
            //if (args.Length == 6)
            //{
            // FREAKKI   SceneManager.HandleEditCommandOnCurrentScene(args);
            //}
            //else
            //{
            //    MainConsole.Instance.Output("Argument error: edit scale <prim name> <x> <y> <z>");
            //}
            throw new FreAkkiRefactoringException("HandleEditScale");
        }

        private void HandleRotateScene(string module, string[] args) {
            //string usage = "Usage: rotate scene <angle in degrees> [centerX centerY] (centerX and centerY are optional and default to Constants.RegionSize / 2";

            //float centerX = Constants.RegionSize * 0.5f;
            //float centerY = Constants.RegionSize * 0.5f;

            //if (args.Length < 3 || args.Length == 4)
            //{
            //    MainConsole.Instance.Output(usage);
            //    return;
            //}

            //float angle = (float)(Convert.ToSingle(args[2]) / 180.0 * Math.PI);
            //OpenMetaverse.Quaternion rot = OpenMetaverse.Quaternion.CreateFromAxisAngle(0, 0, 1, angle);

            //if (args.Length > 4)
            //{
            //    centerX = Convert.ToSingle(args[3]);
            //    centerY = Convert.ToSingle(args[4]);
            //}

            //Vector3 center = new Vector3(centerX, centerY, 0.0f);

            //SceneManager.ForEachSelectedScene(delegate(Scene scene) 
            //{
            //    scene.ForEachSOG(delegate(SceneObjectGroup sog)
            //    {
            //        if (!sog.IsAttachment)
            //        {
            //            sog.RootPart.UpdateRotation(rot * sog.GroupRotation);
            //            Vector3 offset = sog.AbsolutePosition - center;
            //            offset *= rot;
            //            sog.UpdateGroupPosition(center + offset);
            //        }
            //    });
            //});
            throw new FreAkkiRefactoringException("HandleRotateScene");
        }

        private void HandleScaleScene(string module, string[] args) {
            //string usage = "Usage: scale scene <factor>";

            //if (args.Length < 3)
            //{
            //    MainConsole.Instance.Output(usage);
            //    return;
            //}

            //float factor = (float)(Convert.ToSingle(args[2]));

            //float minZ = float.MaxValue;

            //SceneManager.ForEachSelectedScene(delegate(Scene scene)
            //{
            //    scene.ForEachSOG(delegate(SceneObjectGroup sog)
            //    {
            //        if (!sog.IsAttachment)
            //        {
            //            if (sog.RootPart.AbsolutePosition.Z < minZ)
            //                minZ = sog.RootPart.AbsolutePosition.Z;
            //        }
            //    });
            //});

            //SceneManager.ForEachSelectedScene(delegate(Scene scene)
            //{
            //    scene.ForEachSOG(delegate(SceneObjectGroup sog)
            //    {
            //        if (!sog.IsAttachment)
            //        {
            //            Vector3 tmpRootPos = sog.RootPart.AbsolutePosition;
            //            tmpRootPos.Z -= minZ;
            //            tmpRootPos *= factor;
            //            tmpRootPos.Z += minZ;

            //            foreach (SceneObjectPart sop in sog.Parts)
            //            {
            //                if (sop.ParentID != 0)
            //                    sop.OffsetPosition *= factor;
            //                sop.Scale *= factor;
            //            }

            //            sog.UpdateGroupPosition(tmpRootPos);
            //        }
            //    });
            //});
            throw new FreAkkiRefactoringException("HandleScaleScene");
        }

        private void HandleTranslateScene(string module, string[] args) {
            //string usage = "Usage: translate scene <xOffset, yOffset, zOffset>";

            //if (args.Length < 5)
            //{
            //    MainConsole.Instance.Output(usage);
            //    return;
            //}

            //float xOFfset = (float)Convert.ToSingle(args[2]);
            //float yOffset = (float)Convert.ToSingle(args[3]);
            //float zOffset = (float)Convert.ToSingle(args[4]);

            //Vector3 offset = new Vector3(xOFfset, yOffset, zOffset);

            //SceneManager.ForEachSelectedScene(delegate(Scene scene)
            //{
            //    scene.ForEachSOG(delegate(SceneObjectGroup sog)
            //    {
            //        if (!sog.IsAttachment)
            //            sog.UpdateGroupPosition(sog.AbsolutePosition + offset);
            //    });
            //});
            throw new FreAkkiRefactoringException("HandleTranslateScene");
        }

        /// <summary>
        /// Creates a new region based on the parameters specified.   This will ask the user questions on the console
        /// </summary>
        /// <param name="module"></param>
        /// <param name="cmd">0,1,region name, region ini or XML file</param>
        private void HandleCreateRegion(string module, string[] cmd) {
            //string regionName = string.Empty;
            //string regionFile = string.Empty;

            //if (cmd.Length == 3)
            //{
            //    regionFile = cmd[2];
            //}
            //else if (cmd.Length > 3)
            //{
            //    regionName = cmd[2];
            //    regionFile = cmd[3];
            //}

            //string extension = Path.GetExtension(regionFile).ToLower();
            //bool isXml = extension.Equals(".xml");
            //bool isIni = extension.Equals(".ini");

            //if (!isXml && !isIni)
            //{
            //    MainConsole.Instance.Output("Usage: create region [\"region name\"] <region_file.ini>");
            //    return;
            //}

            //if (!Path.IsPathRooted(regionFile))
            //{
            //    string regionsDir = ConfigSource.Source.Configs["Startup"].GetString("regionload_regionsdir", "Regions").Trim();
            //    regionFile = Path.Combine(regionsDir, regionFile);
            //}

            //RegionInfo regInfo;
            //if (isXml)
            //{
            //    regInfo = new RegionInfo(regionName, regionFile, false, ConfigSource.Source);
            //}
            //else
            //{
            //    regInfo = new RegionInfo(regionName, regionFile, false, ConfigSource.Source, regionName);
            //}

            //Scene existingScene;
            //if (SceneManager.TryGetScene(regInfo.RegionID, out existingScene))
            //{
            //    MainConsole.Instance.OutputFormat(
            //        "ERROR: Cannot create region {0} with ID {1}, this ID is already assigned to region {2}",
            //        regInfo.RegionName, regInfo.RegionID, existingScene.RegionInfo.RegionName);

            //    return;
            //}

            //bool changed = PopulateRegionEstateInfo(regInfo);
            //IScene scene;
            //CreateRegion(regInfo, true, out scene);

            //if (changed)
            //    regInfo.EstateSettings.Save();
            throw new FreAkkiRefactoringException("HandleCreateRegion");
        }

        /// <summary>
        /// Turn on some debugging values for OpenSim.
        /// </summary>
        /// <param name="args"></param>
        private static void HandleDebugHttpCommand(string module, string[] cmdparams) {
            if (cmdparams.Length < 3) {
                MainConsole.Instance.Output("Usage: debug http <in|out|all> 0..6");
                return;
            }

            bool inReqs = false;
            bool outReqs = false;
            bool allReqs = false;

            string subCommand = cmdparams[2];

            if (subCommand.ToLower() == "in") {
                inReqs = true;
            } else if (subCommand.ToLower() == "out") {
                outReqs = true;
            } else if (subCommand.ToLower() == "all") {
                allReqs = true;
            } else {
                MainConsole.Instance.Output("You must specify in, out or all");
                return;
            }

            if (cmdparams.Length >= 4) {
                string rawNewDebug = cmdparams[3];
                int newDebug;

                if (!int.TryParse(rawNewDebug, out newDebug)) {
                    MainConsole.Instance.OutputFormat("{0} is not a valid debug level", rawNewDebug);
                    return;
                }

                if (newDebug < 0 || newDebug > 6) {
                    MainConsole.Instance.OutputFormat("{0} is outside the valid debug level range of 0..6", newDebug);
                    return;
                }

                if (allReqs || inReqs) {
                    MainServer.DebugLevel = newDebug;
                    MainConsole.Instance.OutputFormat("IN debug level set to {0}", newDebug);
                }

                if (allReqs || outReqs) {
                    WebUtil.DebugLevel = newDebug;
                    MainConsole.Instance.OutputFormat("OUT debug level set to {0}", newDebug);
                }
            } else {
                if (allReqs || inReqs)
                    MainConsole.Instance.OutputFormat("Current IN debug level is {0}", MainServer.DebugLevel);

                if (allReqs || outReqs)
                    MainConsole.Instance.OutputFormat("Current OUT debug level is {0}", WebUtil.DebugLevel);
            }
        }

        private static void HandleShowHttpHandlersCommand(string module, string[] args) {
            // FREAKKI
            //if (args.Length != 2) {
            //    MainConsole.Instance.Output("Usage: show http-handlers");
            //    return;
            //}

            //StringBuilder handlers = new StringBuilder();

            //foreach (BaseHttpServer httpServer in m_Servers.Values) {
            //    handlers.AppendFormat(
            //        "Registered HTTP Handlers for server at {0}:{1}\n", httpServer.ListenIPAddress, httpServer.Port);

            //    handlers.AppendFormat("* XMLRPC:\n");
            //    foreach (String s in httpServer.GetXmlRpcHandlerKeys())
            //        handlers.AppendFormat("\t{0}\n", s);

            //    handlers.AppendFormat("* HTTP:\n");
            //    foreach (String s in httpServer.GetHTTPHandlerKeys())
            //        handlers.AppendFormat("\t{0}\n", s);

            //    handlers.AppendFormat("* HTTP (poll):\n");
            //    foreach (String s in httpServer.GetPollServiceHandlerKeys())
            //        handlers.AppendFormat("\t{0}\n", s);

            //    handlers.AppendFormat("* JSONRPC:\n");
            //    foreach (String s in httpServer.GetJsonRpcHandlerKeys())
            //        handlers.AppendFormat("\t{0}\n", s);

            //    //                    handlers.AppendFormat("* Agent:\n");
            //    //                    foreach (String s in httpServer.GetAgentHandlerKeys())
            //    //                        handlers.AppendFormat("\t{0}\n", s);

            //    handlers.AppendFormat("* LLSD:\n");
            //    foreach (String s in httpServer.GetLLSDHandlerKeys())
            //        handlers.AppendFormat("\t{0}\n", s);

            //    handlers.AppendFormat("* StreamHandlers ({0}):\n", httpServer.GetStreamHandlerKeys().Count);
            //    foreach (String s in httpServer.GetStreamHandlerKeys())
            //        handlers.AppendFormat("\t{0}\n", s);

            //    handlers.Append("\n");
            //}

            //MainConsole.Instance.Output(handlers.ToString());
            throw new FreAkkiRefactoringException("HandleShowHttpHandlersCommand");
        }


        /// <summary>
        /// Runs commands issued by the server console from the operator
        /// </summary>
        /// <param name="command">The first argument of the parameter (the command)</param>
        /// <param name="cmdparams">Additional arguments passed to the command</param>
        public void RunCommand(string module, string[] cmdparams) {
            //List<string> args = new List<string>(cmdparams);
            //if (args.Count < 1)
            //    return;

            //string command = args[0];
            //args.RemoveAt(0);

            //cmdparams = args.ToArray();

            //switch (command)
            //{
            //    case "backup":
            //        MainConsole.Instance.Output("Triggering save of pending object updates to persistent store");
            //        SceneManager.BackupCurrentScene();
            //        break;

            //    case "remove-region":
            //        string regRemoveName = CombineParams(cmdparams, 0);

            //        Scene removeScene;
            //        if (SceneManager.TryGetScene(regRemoveName, out removeScene))
            //            RemoveRegion(removeScene, false);
            //        else
            //            MainConsole.Instance.Output("No region with that name");
            //        break;

            //    case "delete-region":
            //        string regDeleteName = CombineParams(cmdparams, 0);

            //        Scene killScene;
            //        if (SceneManager.TryGetScene(regDeleteName, out killScene))
            //            RemoveRegion(killScene, true);
            //        else
            //            MainConsole.Instance.Output("no region with that name");
            //        break;

            //    case "restart":
            //        SceneManager.RestartCurrentScene();
            //        break;
            //}
            throw new FreAkkiRefactoringException("RunCommand");
        }


        // see BaseOpenSimServer
        /// <summary>
        /// Many commands list objects for debugging.  Some of the types are listed  here
        /// </summary>
        /// <param name="mod"></param>
        /// <param name="cmd"></param>
        public override void HandleShow(string mod, string[] cmd) {
            //base.HandleShow(mod, cmd);

            //List<string> args = new List<string>(cmd);
            //args.RemoveAt(0);
            //string[] showParams = args.ToArray();

            //switch (showParams[0])
            //{
            //    case "users":
            //        IList agents;
            //        if (showParams.Length > 1 && showParams[1] == "full")
            //        {
            //            agents = SceneManager.GetCurrentScenePresences();
            //        } else
            //        {
            //            agents = SceneManager.GetCurrentSceneAvatars();
            //        }

            //        MainConsole.Instance.Output(String.Format("\nAgents connected: {0}\n", agents.Count));

            //        MainConsole.Instance.Output(
            //            String.Format("{0,-16} {1,-16} {2,-37} {3,-11} {4,-16} {5,-30}", "Firstname", "Lastname",
            //                          "Agent ID", "Root/Child", "Region", "Position")
            //        );

            //        foreach (ScenePresence presence in agents)
            //        {
            //            RegionInfo regionInfo = presence.Scene.RegionInfo;
            //            string regionName;

            //            if (regionInfo == null)
            //            {
            //                regionName = "Unresolvable";
            //            } else
            //            {
            //                regionName = regionInfo.RegionName;
            //            }

            //            MainConsole.Instance.Output(
            //                String.Format(
            //                    "{0,-16} {1,-16} {2,-37} {3,-11} {4,-16} {5,-30}",
            //                    presence.Firstname,
            //                    presence.Lastname,
            //                    presence.UUID,
            //                    presence.IsChildAgent ? "Child" : "Root",
            //                    regionName,
            //                    presence.AbsolutePosition.ToString())
            //            );
            //        }

            //        MainConsole.Instance.Output(String.Empty);
            //        break;

            //    case "connections":
            //        HandleShowConnections();
            //        break;

            //    case "circuits":
            //        HandleShowCircuits();
            //        break;

            //    case "modules":
            //        SceneManager.ForEachSelectedScene(
            //            scene => 
            //            {
            //                MainConsole.Instance.OutputFormat("Loaded region modules in {0} are:", scene.Name);

            //                List<IRegionModuleBase> sharedModules = new List<IRegionModuleBase>();
            //                List<IRegionModuleBase> nonSharedModules = new List<IRegionModuleBase>();

            //                foreach (IRegionModuleBase module in scene.RegionModules.Values)
            //                {
            //                    if (module.GetType().GetInterface("ISharedRegionModule") == null)
            //                        nonSharedModules.Add(module);
            //                    else
            //                        sharedModules.Add(module);
            //                }

            //                foreach (IRegionModuleBase module in sharedModules.OrderBy(m => m.Name))
            //                    MainConsole.Instance.OutputFormat("New Region Module (Shared): {0}", module.Name);

            //                foreach (IRegionModuleBase module in nonSharedModules.OrderBy(m => m.Name))
            //                    MainConsole.Instance.OutputFormat("New Region Module (Non-Shared): {0}", module.Name);
            //            }
            //        );

            //        MainConsole.Instance.Output("");
            //        break;

            //    case "regions":
            //        ConsoleDisplayTable cdt = new ConsoleDisplayTable();
            //        cdt.AddColumn("Name", ConsoleDisplayUtil.RegionNameSize);
            //        cdt.AddColumn("ID", ConsoleDisplayUtil.UuidSize);
            //        cdt.AddColumn("Position", ConsoleDisplayUtil.CoordTupleSize);
            //        cdt.AddColumn("Port", ConsoleDisplayUtil.PortSize);
            //        cdt.AddColumn("Ready?", 6);
            //        cdt.AddColumn("Estate", ConsoleDisplayUtil.EstateNameSize);
            //        SceneManager.ForEachScene(
            //            scene => 
            //            { 
            //                RegionInfo ri = scene.RegionInfo; 
            //                cdt.AddRow(
            //                    ri.RegionName, ri.RegionID, string.Format("{0},{1}", ri.RegionLocX, ri.RegionLocY), 
            //                    ri.InternalEndPoint.Port, scene.Ready ? "Yes" : "No", ri.EstateSettings.EstateName);
            //            }
            //        );

            //        MainConsole.Instance.Output(cdt.ToString());
            //        break;

            //    case "ratings":
            //        SceneManager.ForEachScene(
            //        delegate(Scene scene)
            //        {
            //            string rating = "";
            //            if (scene.RegionInfo.RegionSettings.Maturity == 1)
            //            {
            //                rating = "MATURE";
            //            }
            //            else if (scene.RegionInfo.RegionSettings.Maturity == 2)
            //            {
            //                rating = "ADULT";
            //            }
            //            else
            //            {
            //                rating = "PG";
            //            }
            //            MainConsole.Instance.Output(String.Format(
            //                       "Region Name: {0}, Region Rating {1}",
            //                       scene.RegionInfo.RegionName,
            //                       rating));
            //        });
            //        break;
            //}
            throw new FreAkkiRefactoringException("HandleShow");
        }

        private void HandleShowCircuits() {
            //ConsoleDisplayTable cdt = new ConsoleDisplayTable();
            //cdt.AddColumn("Region", 20);
            //cdt.AddColumn("Avatar name", 24);
            //cdt.AddColumn("Type", 5);
            //cdt.AddColumn("Code", 10);
            //cdt.AddColumn("IP", 16);
            //cdt.AddColumn("Viewer Name", 24);

            //SceneManager.ForEachScene(
            //    s =>
            //    {
            //        foreach (AgentCircuitData aCircuit in s.AuthenticateHandler.GetAgentCircuits().Values)
            //            cdt.AddRow(
            //                s.Name,
            //                aCircuit.Name,
            //                aCircuit.child ? "child" : "root",
            //                aCircuit.circuitcode.ToString(),
            //                aCircuit.IPAddress != null ? aCircuit.IPAddress.ToString() : "not set",
            //                Util.GetViewerName(aCircuit));
            //    });

            //MainConsole.Instance.Output(cdt.ToString());
            throw new FreAkkiRefactoringException("HandleShowCircuits");
        }

        private void HandleShowConnections() {
            //ConsoleDisplayTable cdt = new ConsoleDisplayTable();
            //cdt.AddColumn("Region", 20);
            //cdt.AddColumn("Avatar name", 24);
            //cdt.AddColumn("Circuit code", 12);
            //cdt.AddColumn("Endpoint", 23);
            //cdt.AddColumn("Active?", 7);

            //SceneManager.ForEachScene(
            //    s => s.ForEachClient(
            //        c => cdt.AddRow(
            //            s.Name,
            //            c.Name,
            //            c.CircuitCode.ToString(),
            //            c.RemoteEndPoint.ToString(),                
            //            c.IsActive.ToString())));

            //MainConsole.Instance.Output(cdt.ToString());
            throw new FreAkkiRefactoringException("HandleShowConnections");
        }

        private void HandleCommanderCommand(string module, string[] cmd) {
            // FREAKKI SceneManager.SendCommandToPluginModules(cmd);
            throw new FreAkkiRefactoringException("HandleCommanderCommand ... pending");
        }

        private void HandleCommanderHelp(string module, string[] cmd) {
            // Only safe for the interactive console, since it won't
            // let us come here unless both scene and commander exist
            //
            // FREAKKI ICommander moduleCommander = SceneManager.CurrentOrFirstScene.GetCommander(cmd[1].ToLower());
            // if (moduleCommander != null)
            //    m_console.Output(moduleCommander.Help);
            throw new FreAkkiRefactoringException("HandleCommanderHelp(string module, string[] cmd) ... pending");
        }

        protected virtual void HandleRestartRegion(RegionInfo whichRegion) {
            m_log.InfoFormat(
                "[OPENSIM]: Got restart signal from SceneManager for region {0} ({1},{2})",
                whichRegion.RegionName, whichRegion.RegionLocX, whichRegion.RegionLocY);

            ShutdownClientServer(whichRegion);
            IScene scene;
            CreateRegion(whichRegion, true, out scene);
            scene.Start();

            //// Where we are restarting multiple scenes at once, a previous call to RefreshPrompt may have set the 
            //// m_console.ConsoleScene to null (indicating all scenes).
            //if (m_console.ConsoleScene != null && whichRegion.RegionName == ((Scene)m_console.ConsoleScene).Name)
            //    SceneManager.TrySetCurrentScene(whichRegion.RegionName);

            //RefreshPrompt();
            throw new FreAkkiRefactoringException("HandleRestartRegion");
        }

        /// <summary>
        /// Use XML2 format to serialize data to a file
        /// </summary>
        /// <param name="module"></param>
        /// <param name="cmdparams"></param>
        protected void SavePrimsXml2(string module, string[] cmdparams) {
            //if (cmdparams.Length > 5)
            //{
            //    SceneManager.SaveNamedPrimsToXml2(cmdparams[3], cmdparams[4]);
            //}
            //else
            //{
            //    SceneManager.SaveNamedPrimsToXml2("Primitive", DEFAULT_PRIM_BACKUP_FILENAME);
            //}
            throw new FreAkkiRefactoringException("SavePrimsXml2");
        }
            
        /// <summary>
        /// Serialize region data to XML2Format
        /// </summary>
        /// <param name="module"></param>
        /// <param name="cmdparams"></param>
        protected void SaveXml2(string module, string[] cmdparams) {
            //if (cmdparams.Length > 2)
            //{
            //    SceneManager.SaveCurrentSceneToXml2(cmdparams[2]);
            //}
            //else
            //{
            //    SceneManager.SaveCurrentSceneToXml2(DEFAULT_PRIM_BACKUP_FILENAME);
            //}
            throw new FreAkkiRefactoringException("SaveXml2");
        }

        /// <summary>
        /// Load region data from Xml2Format
        /// </summary>
        /// <param name="module"></param>
        /// <param name="cmdparams"></param>
        protected void LoadXml2(string module, string[] cmdparams) {
            //if (cmdparams.Length > 2)
            //{
            //    try
            //    {
            //        SceneManager.LoadCurrentSceneFromXml2(cmdparams[2]);
            //    }
            //    catch (FileNotFoundException)
            //    {
            //        MainConsole.Instance.Output("Specified xml not found. Usage: load xml2 <filename>");
            //    }
            //}
            //else
            //{
            //    try
            //    {
            //        SceneManager.LoadCurrentSceneFromXml2(DEFAULT_PRIM_BACKUP_FILENAME);
            //    }
            //    catch (FileNotFoundException)
            //    {
            //        MainConsole.Instance.Output("Default xml not found. Usage: load xml2 <filename>");
            //    }
            //}
            throw new FreAkkiRefactoringException("LoadXml2");
        }



        protected void CreateEstateCommand(string module, string[] args) {
            //string response = null;
            //UUID userID;

            //if (args.Length == 2)
            //{
            //    response = "No user specified.";
            //}
            //else if (!UUID.TryParse(args[2], out userID))
            //{
            //    response = String.Format("{0} is not a valid UUID", args[2]);
            //}
            //else if (args.Length == 3)
            //{
            //    response = "No estate name specified.";
            //}
            //else
            //{
            //    Scene scene = SceneManager.CurrentOrFirstScene;

            //    // TODO: Is there a better choice here?
            //    UUID scopeID = UUID.Zero;
            //    UserAccount account = scene.UserAccountService.GetUserAccount(scopeID, userID);
            //    if (account == null)
            //    {
            //        response = String.Format("Could not find user {0}", userID);
            //    }
            //    else
            //    {
            //        // concatenate it all to "name"
            //        StringBuilder sb = new StringBuilder(args[3]);
            //        for (int i = 4; i < args.Length; i++)
            //            sb.Append (" " + args[i]);
            //        string estateName = sb.ToString().Trim();

            //        // send it off for processing.
            //        IEstateModule estateModule = scene.RequestModuleInterface<IEstateModule>();
            //        response = estateModule.CreateEstate(estateName, userID);
            //        if (response == String.Empty)
            //        {
            //            List<int> estates = scene.EstateDataService.GetEstates(estateName);
            //            response = String.Format("Estate {0} created as \"{1}\"", estates.ElementAt(0), estateName);
            //        }
            //    }
            //}

            //// give the user some feedback
            //if (response != null)
            //    MainConsole.Instance.Output(response);
            throw new FreAkkiRefactoringException("CreateEstateCommand");
        }

        protected void SetEstateOwnerCommand(string module, string[] args) {
            //string response = null;

            //Scene scene = SceneManager.CurrentOrFirstScene;
            //IEstateModule estateModule = scene.RequestModuleInterface<IEstateModule>();

            //if (args.Length == 3)
            //{
            //    response = "No estate specified.";
            //}
            //else
            //{
            //    int estateId;
            //    if (!int.TryParse(args[3], out estateId))
            //    {
            //        response = String.Format("\"{0}\" is not a valid ID for an Estate", args[3]);
            //    }
            //    else
            //    {
            //        if (args.Length == 4)
            //        {
            //            response = "No user specified.";
            //        }
            //        else
            //        {
            //            UserAccount account = null;

            //            // TODO: Is there a better choice here?
            //            UUID scopeID = UUID.Zero;

            //            string s1 = args[4];
            //            if (args.Length == 5)
            //            {
            //                // attempt to get account by UUID
            //                UUID u;
            //                if (UUID.TryParse(s1, out u))
            //                {
            //                    account = scene.UserAccountService.GetUserAccount(scopeID, u);
            //                    if (account == null)
            //                        response = String.Format("Could not find user {0}", s1);
            //                }
            //                else
            //                {
            //                    response = String.Format("Invalid UUID {0}", s1);
            //                }
            //            }
            //            else
            //            {
            //                // attempt to get account by Firstname, Lastname
            //                string s2 = args[5];
            //                account = scene.UserAccountService.GetUserAccount(scopeID, s1, s2);
            //                if (account == null)
            //                    response = String.Format("Could not find user {0} {1}", s1, s2);
            //            }

            //            // If it's valid, send it off for processing.
            //            if (account != null)
            //                response = estateModule.SetEstateOwner(estateId, account);

            //            if (response == String.Empty)
            //            {
            //                response = String.Format("Estate owner changed to {0} ({1} {2})", account.PrincipalID, account.FirstName, account.LastName);
            //            }
            //        }
            //    }
            //}

            //// give the user some feedback
            //if (response != null)
            //    MainConsole.Instance.Output(response);
            throw new FreAkkiRefactoringException("SetEstateOwnerCommand");
        }

        protected void SetEstateNameCommand(string module, string[] args) {
            //    string response = null;

            //    Scene scene = SceneManager.CurrentOrFirstScene;
            //    IEstateModule estateModule = scene.RequestModuleInterface<IEstateModule>();

            //    if (args.Length == 3)
            //    {
            //        response = "No estate specified.";
            //    }
            //    else
            //    {
            //        int estateId;
            //        if (!int.TryParse(args[3], out estateId))
            //        {
            //            response = String.Format("\"{0}\" is not a valid ID for an Estate", args[3]);
            //        }
            //        else
            //        {
            //            if (args.Length == 4)
            //            {
            //                response = "No name specified.";
            //            }
            //            else
            //            {
            //                // everything after the estate ID is "name"
            //                StringBuilder sb = new StringBuilder(args[4]);
            //                for (int i = 5; i < args.Length; i++)
            //                    sb.Append (" " + args[i]);

            //                string estateName = sb.ToString();

            //                // send it off for processing.
            //                response = estateModule.SetEstateName(estateId, estateName);

            //                if (response == String.Empty)
            //                {
            //                    response = String.Format("Estate {0} renamed to \"{1}\"", estateId, estateName);
            //                }
            //            }
            //        }
            //    }

            //    // give the user some feedback
            //    if (response != null)
            //        MainConsole.Instance.Output(response);
            throw new FreAkkiRefactoringException("SetEstateNameCommand");
        }

        private void EstateLinkRegionCommand(string module, string[] args) {
            //    int estateId =-1;
            //    UUID regionId = UUID.Zero;
            //    Scene scene = null;
            //    string response = null;

            //    if (args.Length == 3)
            //    {
            //        response = "No estate specified.";
            //    }
            //    else if (!int.TryParse(args [3], out estateId))
            //    {
            //        response = String.Format("\"{0}\" is not a valid ID for an Estate", args [3]);
            //    }
            //    else if (args.Length == 4)
            //    {
            //        response = "No region specified.";
            //    }
            //    else if (!UUID.TryParse(args[4], out regionId))
            //    {
            //        response = String.Format("\"{0}\" is not a valid UUID for a Region", args [4]);
            //    }
            //    else if (!SceneManager.TryGetScene(regionId, out scene))
            //    {
            //        // region may exist, but on a different sim.
            //        response = String.Format("No access to Region \"{0}\"", args [4]);
            //    }

            //    if (response != null)
            //    {
            //        MainConsole.Instance.Output(response);
            //        return;
            //    }

            //    // send it off for processing.
            //    IEstateModule estateModule = scene.RequestModuleInterface<IEstateModule>();
            //    response = estateModule.SetRegionEstate(scene.RegionInfo, estateId);
            //    if (response == String.Empty)
            //    {
            //        estateModule.TriggerRegionInfoChange();
            //        estateModule.sendRegionHandshakeToAll();
            //        response = String.Format ("Region {0} is now attached to estate {1}", regionId, estateId);
            //    }

            //    // give the user some feedback
            //    if (response != null)
            //        MainConsole.Instance.Output (response);
            throw new FreAkkiRefactoringException("EstateLinkRegionCommand");
        }

        private static string CombineParams(string[] commandParams, int pos) {
            string result = String.Empty;
            for (int i = pos; i < commandParams.Length; i++) {
                result += commandParams[i] + " ";
            }
            result = result.TrimEnd(' ');
            return result;
        }

        #endregion

        #region misc

        /// <summary>
        /// Get the number of the avatars in the Region server
        /// </summary>
        /// <param name="usernum">The first out parameter describing the number of all the avatars in the Region server</param>
        public void GetAvatarNumber(out int usernum) {
            // usernum = SceneManager.GetCurrentSceneAvatars().Count;
            throw new FreAkkiRefactoringException("GetAvatarNumber");
        }

        /// <summary>
        /// Get the number of regions
        /// </summary>
        /// <param name="regionnum">The first out parameter describing the number of regions</param>
        public void GetRegionNumber(out int regionnum) {
            // regionnum = SceneManager.Scenes.Count;
            throw new FreAkkiRefactoringException("GetRegionNumber");
        }

        /// <summary>
        /// Get the start time and up time of Region server
        /// </summary>
        /// <param name="starttime">The first out parameter describing when the Region server started</param>
        /// <param name="uptime">The second out parameter describing how long the Region server has run</param>
        public void GetRunTime(out string starttime, out string uptime) {
            starttime = m_startuptime.ToString();
            uptime = (DateTime.Now - m_startuptime).ToString();
        }

        /// <summary>
        /// Timer to run a specific text file as console commands.  Configured in in the main ini file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunAutoTimerScript(object sender, EventArgs e) {
            if (m_timedScript != "disabled") {
                RunCommandScript(m_timedScript);
            }
        }

        private void WatchdogTimeoutHandler(Watchdog.ThreadWatchdogInfo twi) {
            int now = Environment.TickCount;

            m_log.ErrorFormat(
                "[WATCHDOG]: Timeout detected for thread \"{0}\". ThreadState={1}. Last tick was {2}ms ago.  {3}",
                twi.Thread.Name,
                twi.Thread.ThreadState,
                now - twi.LastTick,
                twi.AlarmMethod != null ? string.Format("Data: {0}", twi.AlarmMethod()) : "");
        }
        #endregion

        #region shutdown
        /// <summary>
        /// Performs any last-minute sanity checking and shuts down the region server
        /// </summary>
        protected override void ShutdownSpecific() {
            if (m_shutdownCommandsFile != String.Empty) {
                RunCommandScript(m_shutdownCommandsFile);
            }

            if (proxyUrl.Length > 0) {
                Util.XmlRpcCommand(proxyUrl, "Stop");
            }

            m_log.Info("Closing all threads");
            m_log.Info("Killing listener thread");
            m_log.Info("Killing clients");
            m_log.Info("Closing console and terminating");

            try {
                var task = Task.Run(async () => {
                    var job = MainServer.SceneManager.Ask(new SceneCloseMessage(), TimeSpan.FromSeconds(30));
                    await Task.WhenAll(job);
                    return (job.Result);
                });
                m_log.InfoFormat("SceneManager.Close: {0}", task.Result);
            } catch (Exception e) {
                m_log.Error("[SHUTDOWN]: Ignoring failure during shutdown - ", e);
            }

            base.ShutdownSpecific();
        }


        protected void ShutdownClientServer(RegionInfo whichRegion) {
            // Close and remove the clientserver for a region
            bool foundClientServer = false;
            int clientServerElement = 0;
            Location location = new Location(whichRegion.RegionHandle);

            for (int i = 0; i < m_clientServers.Count; i++) {
                if (m_clientServers[i].HandlesRegion(location)) {
                    clientServerElement = i;
                    foundClientServer = true;
                    break;
                }
            }

            if (foundClientServer) {
                m_clientServers[clientServerElement].Stop();
                m_clientServers.RemoveAt(clientServerElement);
            }
        }

        #endregion

    }
}
