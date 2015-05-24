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

using System;
using System.Collections.Generic;
using System.Reflection;
using log4net;

namespace OpenSim.Framework.Console
{
    /// <summary>
    /// A console that processes commands internally
    /// </summary>
    public class CommandConsole : ConsoleBase, ICommandConsole
    {
        private static readonly ILog m_log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public event OnOutputDelegate OnOutput;

        public ICommands Commands { get; private set; }

        public CommandConsole(string defaultPrompt) : base(defaultPrompt)
        {
            Commands = new Commands();

            Commands.AddCommand(
                "Help", false, "help", "help [<item>]",
                "Display help on a particular command or on a list of commands in a category", Help);
        }

        private void Help(string module, string[] cmd)
        {
            List<string> help = Commands.GetHelp(cmd);

            foreach (string s in help)
                Output(s);
        }

        protected void FireOnOutput(string text)
        {
            OnOutputDelegate onOutput = OnOutput;
            if (onOutput != null)
                onOutput(text);
        }

        /// <summary>
        /// Display a command prompt on the console and wait for user input
        /// </summary>
        public void Prompt()
        {
            string line = ReadLine(DefaultPrompt + "# ", true, true);

            if (line != String.Empty)
                Output("Invalid command");
        }

        public void RunCommand(string cmd)
        {
            string[] parts = Parser.Parse(cmd);
            Commands.Resolve(parts);
        }

        public override string ReadLine(string p, bool isCommand, bool e)
        {
            System.Console.Write("{0}", p);
            string cmdinput = System.Console.ReadLine();

            if (isCommand)
            {
                string[] cmd = Commands.Resolve(Parser.Parse(cmdinput));

                if (cmd.Length != 0)
                {
                    int i;

                    for (i=0 ; i < cmd.Length ; i++)
                    {
                        if (cmd[i].Contains(" "))
                            cmd[i] = "\"" + cmd[i] + "\"";
                    }
                    return String.Empty;
                }
            }
            return cmdinput;
        }
    }
}
