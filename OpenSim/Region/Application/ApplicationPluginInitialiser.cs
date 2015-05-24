using OpenSim.Framework;

namespace OpenSim
{
    public class ApplicationPluginInitialiser : PluginInitialiserBase
    {
        private OpenSim server;

        public ApplicationPluginInitialiser(OpenSim s)
        {
            server = s;
        }

        public override void Initialise(IPlugin plugin)
        {
            IApplicationPlugin p = plugin as IApplicationPlugin;
            p.Initialise(server);
        }
    }
}