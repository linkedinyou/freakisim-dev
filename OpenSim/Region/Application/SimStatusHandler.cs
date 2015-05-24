using System.IO;
using OpenSim.Framework;
using OpenSim.Framework.Servers.HttpServer;

namespace OpenSim {

    /// <summary>
    /// Handler to supply the current status of this sim
    /// </summary>
    /// <remarks>
    /// Currently this is always OK if the simulator is still listening for connections on its HTTP service
    /// </remarks>
    public class SimStatusHandler : BaseStreamHandler {
        public SimStatusHandler() : base("GET", "/simstatus", "SimStatus", "Simulator Status") { }

        protected override byte[] ProcessRequest(string path, Stream request,
            IOSHttpRequest httpRequest, IOSHttpResponse httpResponse) {
            return Util.UTF8.GetBytes("OK");
        }

        public override string ContentType {
            get { return "text/plain"; }
        }
    }
}