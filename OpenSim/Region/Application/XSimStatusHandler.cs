using System.IO;
using OpenSim.Framework;
using OpenSim.Framework.Servers.HttpServer;

namespace OpenSim {

    /// <summary>
    /// Handler to supply the current extended status of this sim
    /// Sends the statistical data in a json serialization 
    /// </summary>
    public class XSimStatusHandler : BaseStreamHandler {
        private OpenSim m_opensim;

        public XSimStatusHandler(OpenSim sim)
            : base("GET", "/" + Util.SHA1Hash(sim.osSecret), "XSimStatus", "Simulator XStatus") {
            m_opensim = sim;
        }

        public override string ContentType {
            get { return "text/plain"; }
        }

        protected override byte[] ProcessRequest(string path, Stream request,
            IOSHttpRequest httpRequest, IOSHttpResponse httpResponse) {
            return Util.UTF8.GetBytes(m_opensim.StatReport(httpRequest));
        }
    }
}