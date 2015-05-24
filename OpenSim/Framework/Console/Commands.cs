using System;
using System.Collections.Generic;
using System.Xml;

namespace OpenSim.Framework.Console
{
    public class Commands : ICommands
    {
//        private static readonly ILog m_log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Encapsulates a command that can be invoked from the console
        /// </summary>
        private class CommandInfo
        {
            /// <value>
            /// The module from which this command comes
            /// </value>
            public string module;
            
            /// <value>
            /// Whether the module is shared
            /// </value>
            public bool shared;
            
            /// <value>
            /// Very short BNF description
            /// </value>
            public string help_text;
            
            /// <value>
            /// Longer one line help text
            /// </value>
            public string long_help;
            
            /// <value>
            /// Full descriptive help for this command
            /// </value>
            public string descriptive_help;
            
            /// <value>
            /// The method to invoke for this command
            /// </value>
            public List<CommandDelegate> fn;
        }

        public const string GeneralHelpText
            = "To enter an argument that contains spaces, surround the argument with double quotes.\nFor example, show object name \"My long object name\"\n";

        public const string ItemHelpText
            = @"For more information, type 'help all' to get a list of all commands, 
  or type help <item>' where <item> is one of the following:";

        /// <value>
        /// Commands organized by keyword in a tree
        /// </value>
        private Dictionary<string, object> tree =
            new Dictionary<string, object>();

        /// <summary>
        /// Commands organized by module
        /// </summary>
        private ThreadedClasses.RwLockedDictionaryAutoAdd<string, ThreadedClasses.RwLockedList<CommandInfo>> m_modulesCommands = 
            new ThreadedClasses.RwLockedDictionaryAutoAdd<string, ThreadedClasses.RwLockedList<CommandInfo>>(
                delegate() { return new ThreadedClasses.RwLockedList<CommandInfo>(); });

        /// <summary>
        /// Get help for the given help string
        /// </summary>
        /// <param name="helpParts">Parsed parts of the help string.  If empty then general help is returned.</param>
        /// <returns></returns>
        public List<string> GetHelp(string[] cmd)
        {
            List<string> help = new List<string>();
            List<string> helpParts = new List<string>(cmd);
            
            // Remove initial help keyword
            helpParts.RemoveAt(0);

            help.Add(""); // Will become a newline.

            // General help
            if (helpParts.Count == 0)
            {
                help.Add(GeneralHelpText);
                help.Add(ItemHelpText);
                help.AddRange(CollectModulesHelp(tree));
            }
            else if (helpParts.Count == 1 && helpParts[0] == "all")
            {
                help.AddRange(CollectAllCommandsHelp());
            }
            else
            {
                help.AddRange(CollectHelp(helpParts));
            }

            help.Add(""); // Will become a newline.

            return help;
        }

        /// <summary>
        /// Collects the help from all commands and return in alphabetical order.
        /// </summary>
        /// <returns></returns>
        private List<string> CollectAllCommandsHelp()
        {
            List<string> help = new List<string>();

            m_modulesCommands.ForEach(delegate(ThreadedClasses.RwLockedList<CommandInfo> commands)
            {
                List<string> ourHelpText = new List<string>();
                commands.ForEach(delegate(CommandInfo c)
                {
                    ourHelpText.Add(string.Format("{0} - {1}", c.help_text, c.long_help));
                });
                help.AddRange(ourHelpText);
            });

            help.Sort();

            return help;
        }
        
        /// <summary>
        /// See if we can find the requested command in order to display longer help
        /// </summary>
        /// <param name="helpParts"></param>
        /// <returns></returns>
        private List<string> CollectHelp(List<string> helpParts)
        {
            string originalHelpRequest = string.Join(" ", helpParts.ToArray());
            List<string> help = new List<string>();

            // Check modules first to see if we just need to display a list of those commands
            if (TryCollectModuleHelp(originalHelpRequest, help))
            {
                help.Insert(0, ItemHelpText);
                return help;
            }
            
            Dictionary<string, object> dict = tree;
            while (helpParts.Count > 0)
            {
                string helpPart = helpParts[0];
                
                if (!dict.ContainsKey(helpPart))
                    break;
                
                //m_log.Debug("Found {0}", helpParts[0]);
                
                if (dict[helpPart] is Dictionary<string, object>)
                    dict = (Dictionary<string, object>)dict[helpPart]; 
                
                helpParts.RemoveAt(0);
            }
        
            // There was a command for the given help string
            if (dict.ContainsKey(String.Empty))
            {
                CommandInfo commandInfo = (CommandInfo)dict[String.Empty];
                help.Add(commandInfo.help_text);
                help.Add(commandInfo.long_help);

                string descriptiveHelp = commandInfo.descriptive_help;

                // If we do have some descriptive help then insert a spacing line before for readability.
                if (descriptiveHelp != string.Empty)
                    help.Add(string.Empty);
                
                help.Add(commandInfo.descriptive_help);
            }
            else
            {
                help.Add(string.Format("No help is available for {0}", originalHelpRequest));
            }
            
            return help;
        }

        /// <summary>
        /// Try to collect help for the given module if that module exists.
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="helpText">/param>
        /// <returns>true if there was the module existed, false otherwise.</returns>
        private bool TryCollectModuleHelp(string moduleName, List<string> helpText)
        {
            try
            {
                m_modulesCommands.ForEach(delegate(KeyValuePair<string, ThreadedClasses.RwLockedList<CommandInfo>> kvp)
                {
                    // Allow topic help requests to succeed whether they are upper or lowercase.
                    if (moduleName.ToLower() == kvp.Key.ToLower())
                    {
                        List<string> ourHelpText = new List<string>();
                        kvp.Value.ForEach(delegate(CommandInfo c)
                        {
                            ourHelpText.Add(string.Format("{0} - {1}", c.help_text, c.long_help));
                        });
                        ourHelpText.Sort();
                        helpText.AddRange(ourHelpText);

                        throw new ThreadedClasses.ReturnValueException<bool>(true);
                    }
                });
            }
            catch(ThreadedClasses.ReturnValueException<bool>)
            {
                return true;
            }

            return false;
        }

        private List<string> CollectModulesHelp(Dictionary<string, object> dict)
        {
            List<string> helpText = new List<string>(m_modulesCommands.Keys);
            helpText.Sort();
            return helpText;
        }

        /// <summary>
        /// Add a command to those which can be invoked from the console.
        /// </summary>
        /// <param name="module"></param>
        /// <param name="command"></param>
        /// <param name="help"></param>
        /// <param name="longhelp"></param>
        /// <param name="fn"></param>
        public void AddCommand(string module, bool shared, string command,
            string help, string longhelp, CommandDelegate fn)
        {
            AddCommand(module, shared, command, help, longhelp, String.Empty, fn);
        }

        /// <summary>
        /// Add a command to those which can be invoked from the console.
        /// </summary>
        /// <param name="module"></param>
        /// <param name="command"></param>
        /// <param name="help"></param>
        /// <param name="longhelp"></param>
        /// <param name="descriptivehelp"></param>
        /// <param name="fn"></param>
        public void AddCommand(string module, bool shared, string command,
            string help, string longhelp, string descriptivehelp,
            CommandDelegate fn)
        {
            string[] parts = Parser.Parse(command);

            Dictionary<string, object> current = tree;
            
            foreach (string part in parts)
            {
                if (current.ContainsKey(part))
                {
                    if (current[part] is Dictionary<string, object>)
                        current = (Dictionary<string, object>)current[part];
                    else
                        return;
                }
                else
                {
                    current[part] = new Dictionary<string, object>();
                    current = (Dictionary<string, object>)current[part];
                }
            }

            CommandInfo info;

            if (current.ContainsKey(String.Empty))
            {
                info = (CommandInfo)current[String.Empty];
                if (!info.shared && !info.fn.Contains(fn))
                    info.fn.Add(fn);

                return;
            }
            
            info = new CommandInfo();
            info.module = module;
            info.shared = shared;
            info.help_text = help;
            info.long_help = longhelp;
            info.descriptive_help = descriptivehelp;
            info.fn = new List<CommandDelegate>();
            info.fn.Add(fn);
            current[String.Empty] = info;

            // Now add command to modules dictionary
            ThreadedClasses.RwLockedList<CommandInfo> commands = m_modulesCommands[module];
            commands.Add(info);
        }

        public string[] FindNextOption(string[] cmd, bool term)
        {
            Dictionary<string, object> current = tree;

            int remaining = cmd.Length;

            foreach (string s in cmd)
            {
                remaining--;

                List<string> found = new List<string>();

                foreach (string opt in current.Keys)
                {
                    if (remaining > 0 && opt == s)
                    {
                        found.Clear();
                        found.Add(opt);
                        break;
                    }
                    if (opt.StartsWith(s))
                    {
                        found.Add(opt);
                    }
                }

                if (found.Count == 1 && (remaining != 0 || term))
                {
                    current = (Dictionary<string, object>)current[found[0]];
                }
                else if (found.Count > 0)
                {
                    return found.ToArray();
                }
                else
                {
                    break;
//                    return new string[] {"<cr>"};
                }
            }

            if (current.Count > 1)
            {
                List<string> choices = new List<string>();

                bool addcr = false;
                foreach (string s in current.Keys)
                {
                    if (s == String.Empty)
                    {
                        CommandInfo ci = (CommandInfo)current[String.Empty];
                        if (ci.fn.Count != 0)
                            addcr = true;
                    }
                    else
                        choices.Add(s);
                }
                if (addcr)
                    choices.Add("<cr>");
                return choices.ToArray();
            }

            if (current.ContainsKey(String.Empty))
                return new string[] { "Command help: "+((CommandInfo)current[String.Empty]).help_text};

            return new string[] { new List<string>(current.Keys)[0] };
        }

        public string[] Resolve(string[] cmd)
        {
            string[] result = cmd;
            int index = -1;

            Dictionary<string, object> current = tree;

            foreach (string s in cmd)
            {
                // If a user puts an empty string on the console then this cannot be part of the command.
                if (s == "")
                    break;

                index++;

                List<string> found = new List<string>();

                foreach (string opt in current.Keys)
                {
                    if (opt == s)
                    {
                        found.Clear();
                        found.Add(opt);
                        break;
                    }
                    if (opt.StartsWith(s))
                    {
                        found.Add(opt);
                    }
                }

                if (found.Count == 1)
                {
                    result[index] = found[0];
                    current = (Dictionary<string, object>)current[found[0]];
                }
                else if (found.Count > 0)
                {
                    return new string[0];
                }
                else
                {
                    break;
                }
            }

            if (current.ContainsKey(String.Empty))
            {
                CommandInfo ci = (CommandInfo)current[String.Empty];
                if (ci.fn.Count == 0)
                    return new string[0];
                foreach (CommandDelegate fn in ci.fn)
                {
                    if (fn != null)
                        fn(ci.module, result);
                    else
                        return new string[0];
                }
                return result;
            }
            
            return new string[0];
        }

        public XmlElement GetXml(XmlDocument doc)
        {
            CommandInfo help = (CommandInfo)((Dictionary<string, object>)tree["help"])[String.Empty];
            ((Dictionary<string, object>)tree["help"]).Remove(string.Empty);
            if (((Dictionary<string, object>)tree["help"]).Count == 0)
                tree.Remove("help");

            CommandInfo quit = (CommandInfo)((Dictionary<string, object>)tree["quit"])[String.Empty];
            ((Dictionary<string, object>)tree["quit"]).Remove(string.Empty);
            if (((Dictionary<string, object>)tree["quit"]).Count == 0)
                tree.Remove("quit");

            XmlElement root = doc.CreateElement("", "HelpTree", "");

            ProcessTreeLevel(tree, root, doc);

            if (!tree.ContainsKey("help"))
                tree["help"] = (object) new Dictionary<string, object>();
            ((Dictionary<string, object>)tree["help"])[String.Empty] = help;

            if (!tree.ContainsKey("quit"))
                tree["quit"] = (object) new Dictionary<string, object>();
            ((Dictionary<string, object>)tree["quit"])[String.Empty] = quit;

            return root;
        }

        private void ProcessTreeLevel(Dictionary<string, object> level, XmlElement xml, XmlDocument doc)
        {
            foreach (KeyValuePair<string, object> kvp in level)
            {
                if (kvp.Value is Dictionary<string, object>)
                {
                    XmlElement next = doc.CreateElement("", "Level", "");
                    next.SetAttribute("Name", kvp.Key);

                    xml.AppendChild(next);

                    ProcessTreeLevel((Dictionary<string, object>)kvp.Value, next, doc);
                }
                else
                {
                    CommandInfo c = (CommandInfo)kvp.Value;

                    XmlElement cmd = doc.CreateElement("", "Command", "");

                    XmlElement e;

                    e = doc.CreateElement("", "Module", "");
                    cmd.AppendChild(e);
                    e.AppendChild(doc.CreateTextNode(c.module));

                    e = doc.CreateElement("", "Shared", "");
                    cmd.AppendChild(e);
                    e.AppendChild(doc.CreateTextNode(c.shared.ToString()));

                    e = doc.CreateElement("", "HelpText", "");
                    cmd.AppendChild(e);
                    e.AppendChild(doc.CreateTextNode(c.help_text));

                    e = doc.CreateElement("", "LongHelp", "");
                    cmd.AppendChild(e);
                    e.AppendChild(doc.CreateTextNode(c.long_help));

                    e = doc.CreateElement("", "Description", "");
                    cmd.AppendChild(e);
                    e.AppendChild(doc.CreateTextNode(c.descriptive_help));

                    xml.AppendChild(cmd);
                }
            }
        }

        public void FromXml(XmlElement root, CommandDelegate fn)
        {
            CommandInfo help = (CommandInfo)((Dictionary<string, object>)tree["help"])[String.Empty];
            ((Dictionary<string, object>)tree["help"]).Remove(string.Empty);
            if (((Dictionary<string, object>)tree["help"]).Count == 0)
                tree.Remove("help");

            CommandInfo quit = (CommandInfo)((Dictionary<string, object>)tree["quit"])[String.Empty];
            ((Dictionary<string, object>)tree["quit"]).Remove(string.Empty);
            if (((Dictionary<string, object>)tree["quit"]).Count == 0)
                tree.Remove("quit");

            tree.Clear();

            ReadTreeLevel(tree, root, fn);

            if (!tree.ContainsKey("help"))
                tree["help"] = (object) new Dictionary<string, object>();
            ((Dictionary<string, object>)tree["help"])[String.Empty] = help;

            if (!tree.ContainsKey("quit"))
                tree["quit"] = (object) new Dictionary<string, object>();
            ((Dictionary<string, object>)tree["quit"])[String.Empty] = quit;
        }

        private void ReadTreeLevel(Dictionary<string, object> level, XmlNode node, CommandDelegate fn)
        {
            Dictionary<string, object> next;
            string name;

            XmlNodeList nodeL = node.ChildNodes;
            XmlNodeList cmdL;
            CommandInfo c;

            foreach (XmlNode part in nodeL)
            {
                switch (part.Name)
                {
                    case "Level":
                        name = ((XmlElement)part).GetAttribute("Name");
                        next = new Dictionary<string, object>();
                        level[name] = next;
                        ReadTreeLevel(next, part, fn);
                        break;
                    case "Command":
                        cmdL = part.ChildNodes;
                        c = new CommandInfo();
                        foreach (XmlNode cmdPart in cmdL)
                        {
                            switch (cmdPart.Name)
                            {
                                case "Module":
                                    c.module = cmdPart.InnerText;
                                    break;
                                case "Shared":
                                    c.shared = Convert.ToBoolean(cmdPart.InnerText);
                                    break;
                                case "HelpText":
                                    c.help_text = cmdPart.InnerText;
                                    break;
                                case "LongHelp":
                                    c.long_help = cmdPart.InnerText;
                                    break;
                                case "Description":
                                    c.descriptive_help = cmdPart.InnerText;
                                    break;
                            }
                        }
                        c.fn = new List<CommandDelegate>();
                        c.fn.Add(fn);
                        level[String.Empty] = c;
                        break;
                }
            }
        }
    }
}