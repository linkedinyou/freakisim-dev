using log4net;
using Nini.Config;
using OpenSim.Framework;
using OpenSim.Framework.Monitoring;
using System;
using System.Net;
using System.Reflection;


namespace OpenSim.Region.ClientStack.LindenUDP
{
    /// <summary>
    /// A shim around LLUDPServer that implements the IClientNetworkServer interface
    /// </summary>
    public sealed class LLUDPServerShim : IClientNetworkServer
    {
        private static readonly ILog m_log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        LLUDPServer m_udpServer;

        public LLUDPServerShim()
        {
            if (m_log.IsDebugEnabled) {
                m_log.DebugFormat ("{0} called", System.Reflection.MethodBase.GetCurrentMethod ().Name);
            }
        }

        public void Initialise(IPAddress listenIP, ref uint port, int proxyPortOffsetParm, bool allow_alternate_port, IConfigSource configSource, AgentCircuitManager circuitManager)
        {
            if (m_log.IsDebugEnabled) {
                m_log.DebugFormat ("{0} called", System.Reflection.MethodBase.GetCurrentMethod ().Name);
            }


            m_udpServer = new LLUDPServer(listenIP, ref port, proxyPortOffsetParm, allow_alternate_port, configSource, circuitManager);
        }

        public void AddScene(IScene scene)
        {
            if (m_log.IsDebugEnabled) {
                m_log.DebugFormat ("{0} called", System.Reflection.MethodBase.GetCurrentMethod ().Name);
            }


            m_udpServer.AddScene(scene);

            StatsManager.RegisterStat(
                new Stat(
                    "ClientLogoutsDueToNoReceives",
                    "Number of times a client has been logged out because no packets were received before the timeout.",
                    "",
                    "",
                    "clientstack",
                    scene.Name,
                    StatType.Pull,
                    MeasuresOfInterest.None,
                    stat => stat.Value = m_udpServer.ClientLogoutsDueToNoReceives,
                    StatVerbosity.Debug));

            StatsManager.RegisterStat(
                new Stat(
                    "IncomingUDPReceivesCount",
                    "Number of UDP receives performed",
                    "",
                    "",
                    "clientstack",
                    scene.Name,
                    StatType.Pull,
                    MeasuresOfInterest.AverageChangeOverTime,
                    stat => stat.Value = m_udpServer.UdpReceives,
                    StatVerbosity.Debug));

            StatsManager.RegisterStat(
                new Stat(
                    "IncomingPacketsProcessedCount",
                    "Number of inbound LL protocol packets processed",
                    "",
                    "",
                    "clientstack",
                    scene.Name,
                    StatType.Pull,
                    MeasuresOfInterest.AverageChangeOverTime,
                    stat => stat.Value = m_udpServer.IncomingPacketsProcessed,
                    StatVerbosity.Debug));

            StatsManager.RegisterStat(
                new Stat(
                    "IncomingPacketsMalformedCount",
                    "Number of inbound UDP packets that could not be recognized as LL protocol packets.",
                    "",
                    "",
                    "clientstack",
                    scene.Name,
                    StatType.Pull,
                    MeasuresOfInterest.AverageChangeOverTime,
                    stat => stat.Value = m_udpServer.IncomingMalformedPacketCount,
                    StatVerbosity.Info));

            StatsManager.RegisterStat(
                new Stat(
                    "IncomingPacketsOrphanedCount",
                    "Number of inbound packets that were not initial connections packets and could not be associated with a viewer.",
                    "",
                    "",
                    "clientstack",
                    scene.Name,
                    StatType.Pull,
                    MeasuresOfInterest.AverageChangeOverTime,
                    stat => stat.Value = m_udpServer.IncomingOrphanedPacketCount,
                    StatVerbosity.Info));

            StatsManager.RegisterStat(
                new Stat(
                    "IncomingPacketsResentCount",
                    "Number of inbound packets that clients indicate are resends.",
                    "",
                    "",
                    "clientstack",
                    scene.Name,
                    StatType.Pull,
                    MeasuresOfInterest.AverageChangeOverTime,
                    stat => stat.Value = m_udpServer.IncomingPacketsResentCount,
                    StatVerbosity.Debug));

            StatsManager.RegisterStat(
                new Stat(
                    "OutgoingUDPSendsCount",
                    "Number of UDP sends performed",
                    "",
                    "",
                    "clientstack",
                    scene.Name,
                    StatType.Pull,
                    MeasuresOfInterest.AverageChangeOverTime,
                    stat => stat.Value = m_udpServer.UdpSends,
                    StatVerbosity.Debug));

            StatsManager.RegisterStat(
                new Stat(
                    "OutgoingPacketsResentCount",
                    "Number of packets resent because a client did not acknowledge receipt",
                    "",
                    "",
                    "clientstack",
                    scene.Name,
                    StatType.Pull,
                    MeasuresOfInterest.AverageChangeOverTime,
                    stat => stat.Value = m_udpServer.PacketsResentCount,
                    StatVerbosity.Debug));

            StatsManager.RegisterStat(
                new Stat(
                    "AverageUDPProcessTime",
                    "Average number of milliseconds taken to process each incoming UDP packet in a sample.",
                    "This is for initial receive processing which is separate from the later client LL packet processing stage.",
                    "ms",
                    "clientstack",
                    scene.Name,
                    StatType.Pull,
                    MeasuresOfInterest.None,
                    stat => stat.Value = m_udpServer.AverageReceiveTicksForLastSamplePeriod / TimeSpan.TicksPerMillisecond,
                    //                    stat => 
                    //                        stat.Value = Math.Round(m_udpServer.AverageReceiveTicksForLastSamplePeriod / TimeSpan.TicksPerMillisecond, 7),
                    StatVerbosity.Debug));
        }

        public bool HandlesRegion(Location x)
        {
            if (m_log.IsDebugEnabled) {
                m_log.DebugFormat ("{0} called", System.Reflection.MethodBase.GetCurrentMethod ().Name);
            }

            return m_udpServer.HandlesRegion(x);
        }

        public void Start()
        {
            if (m_log.IsDebugEnabled) {
                m_log.DebugFormat ("{0} called", System.Reflection.MethodBase.GetCurrentMethod ().Name);
            }


            m_udpServer.Start();
        }

        public void Stop()
        {
            if (m_log.IsDebugEnabled) {
                m_log.DebugFormat ("{0} called", System.Reflection.MethodBase.GetCurrentMethod ().Name);
            }


            m_udpServer.Stop();
        }
    }
}

