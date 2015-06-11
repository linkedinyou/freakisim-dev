using System;

namespace OpenSim.Framework.Console
{
    public class LoadArchiveToCurrentSceneMessage {
        public LoadArchiveToCurrentSceneMessage(String[] commandparams) {
            this.CmdStrings = commandparams;
        }
        public String[] CmdStrings { get; private set; }
    }
}