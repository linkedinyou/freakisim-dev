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

using Akka;
using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using log4net;
using System.Threading.Tasks;
using Akka.Util.Internal;
using OpenSim.Framework.Servers;

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

            Commands.AddCommand("Archiving", false, "load oar",
                "load oar [--merge] [--persist-uuids] [--skip-assets]"
                + " [--force-terrain] [--force-parcels]"
                + " [--no-objects]"
                + " [--rotation degrees] [--rotation-center \"<x,y,z>\"]"
                + " [--displacement \"<x,y,z>\"]"
                + " [--default-user \"User Name\"]"
                + " [<OAR path>]",
                "Load a region's data from an OAR archive.",
                "--merge will merge the OAR with the existing scene (suppresses terrain and parcel info loading)." + Environment.NewLine
                + "--skip-assets will load the OAR but ignore the assets it contains." + Environment.NewLine
                + "--persist-uuids will restore the saved uuids from the oar (not to be combined with --merge)" + Environment.NewLine
                + "--displacement will add this value to the position of every object loaded" + Environment.NewLine
                + "--force-terrain forces the loading of terrain from the oar (undoes suppression done by --merge)" + Environment.NewLine
                + "--force-parcels forces the loading of parcels from the oar (undoes suppression done by --merge)" + Environment.NewLine
                + "--rotation specified rotation to be applied to the oar. Specified in degrees." + Environment.NewLine
                + "--rotation-center Location (relative to original OAR) to apply rotation. Default is <128,128,0>" + Environment.NewLine
                + "--no-objects suppresses the addition of any objects (good for loading only the terrain)" + Environment.NewLine
                + "The path can be either a filesystem location or a URI."
                + "  If this is not given then the command looks for an OAR named region.oar in the current directory.",
                LoadOar);

            Commands.AddCommand("Archiving", false, "save oar",
                //"save oar [-v|--version=<N>] [-p|--profile=<url>] [<OAR path>]",
                "save oar [-h|--home=<url>] [--noassets] [--publish] [--perm=<permissions>] [--all] [<OAR path>]",
                "Save a region's data to an OAR archive.",
                //                                          "-v|--version=<N> generates scene objects as per older versions of the serialization (e.g. -v=0)" + Environment.NewLine
                "-h|--home=<url> adds the url of the profile service to the saved user information.\n"
                + "--noassets stops assets being saved to the OAR.\n"
                + "--publish saves an OAR stripped of owner and last owner information.\n"
                + "   on reload, the estate owner will be the owner of all objects\n"
                + "   this is useful if you're making oars generally available that might be reloaded to the same grid from which you published\n"
                + "--perm=<permissions> stops objects with insufficient permissions from being saved to the OAR.\n"
                + "   <permissions> can contain one or more of these characters: \"C\" = Copy, \"T\" = Transfer\n"
                + "--all saves all the regions in the simulator, instead of just the current region.\n"
                + "The OAR path must be a filesystem path."
                + " If this is not given then the oar is saved to region.oar in the current directory.",
                SaveOar);
            
            
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

        /// <summary>
        /// Load a whole region from an opensimulator archive.
        /// </summary>
        /// <param name="cmdparams"></param>
        private void LoadOar(string module, string[] cmdparams) {
            var task = Task.Run(async () => {
                var job = MainServer.SceneManager.Ask(new LoadArchiveToCurrentSceneMessage(cmdparams), Timeout.InfiniteTimeSpan);
                await Task.WhenAll(job);
                return (job.Result);
            });

            if (task.Result is Failure) {
                MainConsole.Instance.Output(task.Result.AsInstanceOf<Failure>().Exception.Message);
            } else {
                MainConsole.Instance.OutputFormat("Load Oar result: {0}", task.Result.AsInstanceOf<String>());
            }
        }

        /// <summary>
        /// Save a region to a file, including all the assets needed to restore it.
        /// </summary>
        /// <param name="cmdparams"></param>
        protected void SaveOar(string module, string[] cmdparams) {
            var task = Task.Run(async () => {
                var job = MainServer.SceneManager.Ask(new SaveCurrentSceneToArchiveMessage(cmdparams), Timeout.InfiniteTimeSpan);
                await Task.WhenAll(job);
                return (job.Result);
            });

            if (task.Result is Failure) {
                MainConsole.Instance.Output(task.Result.AsInstanceOf<Failure>().Exception.Message);
            } else {
                MainConsole.Instance.OutputFormat("Load Oar result: {0}", task.Result.AsInstanceOf<String>());
            }
        }

    }
}
