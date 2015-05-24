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
using OpenMetaverse;
using OpenSim.Framework.Monitoring;
using OpenSim.Framework.Servers.HttpServer;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Timers;
using Timer = System.Timers.Timer;
using Nini.Config;

namespace OpenSim.Framework.Servers {
    /// <summary>
    /// Common base for the main OpenSimServers (user, grid, inventory, region, etc)
    /// </summary>
    public abstract class BaseOpenSimServer : ServerBase {
        private static readonly ILog m_baseLog = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// This will control a periodic log printout of the current 'show stats' (if they are active) for this
        /// server.
        /// </summary>
        private int m_periodDiagnosticTimerMS = 60 * 60 * 1000;
        private Timer m_periodicDiagnosticsTimer = new Timer(60 * 60 * 1000);

        /// <summary>
        /// Random uuid for private data 
        /// </summary>
        protected string m_osSecret = String.Empty;

        protected BaseHttpServer m_httpServer;
        public BaseHttpServer HttpServer {
            get { return m_httpServer; }
        }

        public BaseOpenSimServer()
            : base() {
            // Random uuid for private data
            m_osSecret = UUID.Random().ToString();
        }

        /// <summary>
        /// Must be overriden by child classes for their own server specific startup behaviour.
        /// </summary>
        protected virtual void StartupSpecific() {
            StatsManager.SimExtraStats = new SimExtraStatsCollector();
            RegisterCommonCommands();
            RegisterCommonComponents(Config);

            IConfig startupConfig = Config.Configs["Startup"];
            int logShowStatsSeconds = startupConfig.GetInt("LogShowStatsSeconds", m_periodDiagnosticTimerMS / 1000);
            m_periodDiagnosticTimerMS = logShowStatsSeconds * 1000;
            m_periodicDiagnosticsTimer.Elapsed += new ElapsedEventHandler(LogDiagnostics);
            if (m_periodDiagnosticTimerMS != 0) {
                m_periodicDiagnosticsTimer.Interval = m_periodDiagnosticTimerMS;
                m_periodicDiagnosticsTimer.Enabled = true;
            }
        }

        protected void BaseShutdownSpecific() {
            m_baseLog.Info("[SHUTDOWN]: Shutdown processing on main thread complete.  Exiting...");

            RemovePIDFile();

            base.ShutdownSpecific();

            Environment.Exit(0);
        }

        /// <summary>
        /// Provides a list of help topics that are available.  Overriding classes should append their topics to the
        /// information returned when the base method is called.
        /// </summary>
        /// 
        /// <returns>
        /// A list of strings that represent different help topics on which more information is available
        /// </returns>
        protected virtual List<string> GetHelpTopics() { return new List<string>(); }

        /// <summary>
        /// Print statistics to the logfile, if they are active
        /// </summary>
        protected void LogDiagnostics(object source, ElapsedEventArgs e) {
            StringBuilder sb = new StringBuilder("DIAGNOSTICS\n\n");
            sb.Append(GetUptimeReport());
            sb.Append(StatsManager.SimExtraStats.Report());
            sb.Append(Environment.NewLine);
            sb.Append(GetThreadsReport());

            m_baseLog.Debug(sb);
        }

        /// <summary>
        /// Performs initialisation of the scene, such as loading configuration from disk.
        /// </summary>
        public virtual void Startup() {
            StartupSpecific();

            TimeSpan timeTaken = DateTime.Now - m_startuptime;

            MainConsole.Instance.OutputFormat(
                "PLEASE WAIT FOR LOGINS TO BE ENABLED ON REGIONS ONCE SCRIPTS HAVE STARTED.  Non-script portion of startup took {0}m {1}s.",
                timeTaken.Minutes, timeTaken.Seconds);
        }

        public string osSecret {
            // Secret uuid for the simulator
            get { return m_osSecret; }
        }

        public string StatReport(IOSHttpRequest httpRequest) {
            // If we catch a request for "callback", wrap the response in the value for jsonp
            if (httpRequest.Query.ContainsKey("callback")) {
                return httpRequest.Query["callback"].ToString() + "(" + StatsManager.SimExtraStats.XReport((DateTime.Now - m_startuptime).ToString(), m_version) + ");";
            } else {
                return StatsManager.SimExtraStats.XReport((DateTime.Now - m_startuptime).ToString(), m_version);
            }
        }
    }
}