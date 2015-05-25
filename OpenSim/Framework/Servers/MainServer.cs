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

using OpenSim.Framework.Servers.HttpServer;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using Akka.Actor;
using log4net;

namespace OpenSim.Framework.Servers
{
    public class MainServer {
        #region definitions
        private static readonly ILog m_log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static BaseHttpServer instance = null;
        private static ThreadedClasses.RwLockedDictionary<uint, BaseHttpServer> m_Servers = new ThreadedClasses.RwLockedDictionary<uint, BaseHttpServer>();

        /// <summary>
        /// Control the printing of certain debug messages.
        /// </summary>
        /// <remarks>
        /// If DebugLevel >= 1 then short warnings are logged when receiving bad input data.
        /// If DebugLevel >= 2 then long warnings are logged when receiving bad input data.
        /// If DebugLevel >= 3 then short notices about all incoming non-poll HTTP requests are logged.
        /// If DebugLevel >= 4 then the time taken to fulfill the request is logged.
        /// If DebugLevel >= 5 then the start of the body of incoming non-poll HTTP requests will be logged.
        /// If DebugLevel >= 6 then the entire body of incoming non-poll HTTP requests will be logged.
        /// </remarks>
        public static int DebugLevel
        {
            get { return s_debugLevel; }
            set
            {
                s_debugLevel = value;

                m_Servers.ForEach(delegate(BaseHttpServer server)
                {
                    server.DebugLevel = s_debugLevel;
                });
            }
        }

        private static int s_debugLevel;


        /// <summary>
        /// Set the main Actor System.
        /// </summary>
        /// <remarks>
        /// This will be used to register all Actors that are part of the System.
        /// </remarks>
        public static ActorSystem ActorSystem { get; set; }

        public static IActorRef SceneManager { get; set; }


        /// <summary>
        /// Set the main HTTP server instance.
        /// </summary>
        /// <remarks>
        /// This will be used to register all handlers that listen to the default port.
        /// </remarks>
        /// <exception cref='Exception'>
        /// Thrown if the HTTP server has not already been registered via AddHttpServer()
        /// </exception>
        public static BaseHttpServer Instance
        {
            get { return instance; }

            set
            {
                if (!m_Servers.ContainsValue(value))
                    throw new Exception("HTTP server must already have been registered to be set as the main instance");

                instance = value;
            }
        }

        /// <summary>
        /// Get all the registered servers.
        /// </summary>
        /// <remarks>
        /// Returns a copy of the dictionary so this can be iterated through without locking.
        /// </remarks>
        /// <value></value>
        public static Dictionary<uint, BaseHttpServer> Servers
        {
            get 
            {
                return new Dictionary<uint, BaseHttpServer>(m_Servers);
            }
        }
        #endregion


        /// <summary>
        /// Register an already started HTTP server to the collection of known servers.
        /// </summary>
        /// <param name='server'></param>
        public static void AddHttpServer(BaseHttpServer server)
        {
            m_Servers.AddIfNotExists(server.Port, delegate() { return server; });
        }

        /// <summary>
        /// Removes the http server listening on the given port.
        /// </summary>
        /// <remarks>
        /// It is the responsibility of the caller to do clean up.
        /// </remarks>
        /// <param name='port'></param>
        /// <returns></returns>
        public static bool RemoveHttpServer(uint port)
        {
            if (instance != null && instance.Port == port)
            {
                instance = null;
            }
            return m_Servers.Remove(port);
        }

        /// <summary>
        /// Does this collection of servers contain one with the given port?
        /// </summary>
        /// <remarks>
        /// Unlike GetHttpServer, this will not instantiate a server if one does not exist on that port.
        /// </remarks>
        /// <param name='port'></param>
        /// <returns>true if a server with the given port is registered, false otherwise.</returns>
        public static bool ContainsHttpServer(uint port)
        {
            return m_Servers.ContainsKey(port);
        }

        /// <summary>
        /// Get the default http server or an http server for a specific port.
        /// </summary>
        /// <remarks>
        /// If the requested HTTP server doesn't already exist then a new one is instantiated and started.
        /// </remarks>
        /// <returns></returns>
        /// <param name='port'>If 0 then the default HTTP server is returned.</param>
        public static IHttpServer GetHttpServer(uint port)
        {
            return GetHttpServer(port, null);
        }

        /// <summary>
        /// Get the default http server, an http server for a specific port
        /// and/or an http server bound to a specific address
        /// </summary>
        /// <remarks>
        /// If the requested HTTP server doesn't already exist then a new one is instantiated and started.
        /// </remarks>
        /// <returns></returns>
        /// <param name='port'>If 0 then the default HTTP server is returned.</param>
        /// <param name='ipaddr'>A specific IP address to bind to.  If null then the default IP address is used.</param>
        public static IHttpServer GetHttpServer(uint port, IPAddress ipaddr)
        {
            if (port == 0)
                return instance;
            
            if (instance != null && port == instance.Port)
                return instance;

            return m_Servers.GetOrAddIfNotExists(port, delegate()
            {
                BaseHttpServer server = new BaseHttpServer(port);

                if (ipaddr != null)
                    server.ListenIPAddress = ipaddr;

                server.Start();
                return server;

            });
        }
    }
}