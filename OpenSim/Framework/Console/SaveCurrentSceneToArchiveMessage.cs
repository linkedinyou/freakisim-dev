using System;

namespace OpenSim.Framework.Console
{
    public class SaveCurrentSceneToArchiveMessage {
        public SaveCurrentSceneToArchiveMessage(String[] commandparams) {
            this.CmdStrings = commandparams;
        }
        public String[] CmdStrings { get; private set; }
    }
}