using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using OpenMetaverse;
using OpenSim.Framework;
using OpenSim.Framework.Servers.HttpServer;

namespace OpenSim.Framework.Console
{
    public class CommandHandler
    {

        public CommandHandler()
        {
            //// Capabilities Module
            //MainConsole.Instance.Commands.AddCommand(
            //    "Comms", false, "show caps list",
            //    "show caps list",
            //    "Shows list of registered capabilities for users.", CommandHandler.HandleShowCapsListCommand);

            //// Capabilities Module
            //MainConsole.Instance.Commands.AddCommand(
            //    "Comms", false, "show caps stats by user",
            //    "show caps stats by user [<first-name> <last-name>]",
            //    "Shows statistics on capabilities use by user.",
            //    "If a user name is given, then prints a detailed breakdown of caps use ordered by number of requests received.",
            //    HandleShowCapsStatsByUserCommand);

            //// Capabilities Module
            //MainConsole.Instance.Commands.AddCommand(
            //    "Comms", false, "show caps stats by cap",
            //    "show caps stats by cap [<cap-name>]",
            //    "Shows statistics on capabilities use by capability.",
            //    "If a capability name is given, then prints a detailed breakdown of use by each user.",
            //    (module, cmdParams) => CommandHandler.HandleShowCapsStatsByCapCommand(module, cmdParams, this));

        }

        private void HandleShowCapsListCommand(string module, string[] cmdParams)
        {
            //if (SceneManager.Instance.CurrentScene != null && SceneManager.Instance.CurrentScene != _capabilitiesModule.m_scene)
            //    return;

            //StringBuilder capsReport = new StringBuilder();
            //capsReport.AppendFormat("Region {0}:\n", _capabilitiesModule.m_scene.RegionInfo.RegionName);

            //_capabilitiesModule.m_capsObjects.ForEach(delegate(KeyValuePair<UUID, Caps> kvp)
            //{
            //    capsReport.AppendFormat("** User {0}:\n", kvp.Key);
            //    Caps caps = kvp.Value;

            //    for (IDictionaryEnumerator kvp2 = caps.CapsHandlers.GetCapsDetails(false, null).GetEnumerator(); kvp2.MoveNext(); )
            //    {
            //        Uri uri = new Uri(kvp2.Value.ToString());
            //        capsReport.AppendFormat(_capabilitiesModule.m_showCapsCommandFormat, kvp2.Key, uri.PathAndQuery);
            //    }

            //    foreach (KeyValuePair<string, PollServiceEventArgs> kvp2 in caps.GetPollHandlers())
            //        capsReport.AppendFormat(_capabilitiesModule.m_showCapsCommandFormat, kvp2.Key, kvp2.Value.Url);

            //    foreach (KeyValuePair<string, string> kvp3 in caps.ExternalCapsHandlers)
            //        capsReport.AppendFormat(_capabilitiesModule.m_showCapsCommandFormat, kvp3.Key, kvp3.Value);
            //});

            //MainConsole.Instance.Output(capsReport.ToString());
        }

        public void HandleShowCapsStatsByCapCommand(string module, string[] cmdParams)
        {
            //if (SceneManager.Instance.CurrentScene != null && SceneManager.Instance.CurrentScene != capabilitiesModule.MScene)
            //    return;

            //if (cmdParams.Length != 5 && cmdParams.Length != 6)
            //{
            //    MainConsole.Instance.Output("Usage: show caps stats by cap [<cap-name>]");
            //    return;
            //}

            //StringBuilder sb = new StringBuilder();
            //sb.AppendFormat("Region {0}:\n", capabilitiesModule.MScene.Name);

            //if (cmdParams.Length == 5)
            //{
            //    capabilitiesModule.BuildSummaryStatsByCapReport(sb);
            //}
            //else if (cmdParams.Length == 6)
            //{
            //    capabilitiesModule.BuildDetailedStatsByCapReport(sb, cmdParams[5]);
            //}

            //MainConsole.Instance.Output(sb.ToString());
        }

        private void HandleShowCapsStatsByUserCommand(string module, string[] cmdParams)
        {
            //if (SceneManager.Instance.CurrentScene != null && SceneManager.Instance.CurrentScene != capabilitiesModule.MScene1)
            //    return;

            //if (cmdParams.Length != 5 && cmdParams.Length != 7)
            //{
            //    MainConsole.Instance.Output("Usage: show caps stats by user [<first-name> <last-name>]");
            //    return;
            //}

            //StringBuilder sb = new StringBuilder();
            //sb.AppendFormat("Region {0}:\n", capabilitiesModule.MScene1.Name);

            //if (cmdParams.Length == 5)
            //{
            //    capabilitiesModule.BuildSummaryStatsByUserReport(sb);
            //}
            //else if (cmdParams.Length == 7)
            //{
            //    string firstName = cmdParams[5];
            //    string lastName = cmdParams[6];

            //    ScenePresence sp = capabilitiesModule.MScene1.GetScenePresence(firstName, lastName);

            //    if (sp == null)
            //        return;

            //    capabilitiesModule.BuildDetailedStatsByUserReport(sb, sp);
            //}

            //MainConsole.Instance.Output(sb.ToString());
        }
    }
}