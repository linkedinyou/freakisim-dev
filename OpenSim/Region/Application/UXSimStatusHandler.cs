using System.IO;
using OpenSim.Framework;
using OpenSim.Framework.Servers.HttpServer;

namespace OpenSim {
    /// <summary>
    /// Handler to supply the current extended status of this sim to a user configured URI
    /// Sends the statistical data in a json serialization 
    /// If the request contains a key, "callback" the response will be wrappend in the 
    /// associated value for jsonp used with ajax/javascript
    /// </summary>
    public class UXSimStatusHandler : BaseStreamHandler {
        OpenSim m_opensim;

        public UXSimStatusHandler(OpenSim sim)
            : base("GET", "/" + sim.userStatsURI, "UXSimStatus", "Simulator UXStatus") {
            m_opensim = sim;
        }

        protected override byte[] ProcessRequest(string path, Stream request,
            IOSHttpRequest httpRequest, IOSHttpResponse httpResponse) {
            return Util.UTF8.GetBytes(m_opensim.StatReport(httpRequest));
        }

        public override string ContentType {
            get { return "text/plain"; }
        }
    }
}