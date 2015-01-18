using NUnit.Framework;
using OpenSim.Framework;
using OpenMetaverse;
using System;

namespace OpenSim.Test
{
    [TestFixture]
    public class AgentCircuitManagerTest
    {
        [TestCase]
        public void TestAddNewCircuit ()
        {
            AgentCircuitManager manager = new AgentCircuitManager ();
            AgentCircuitData agent = new AgentCircuitData ();

            UUID agentID = UUID.Parse ("150b7ff5-b79e-4524-bd7e-08eafb9bb200");

            agent.AgentID = agentID;
            manager.AddNewCircuit (123456, agent);
            // manager.RemoveCircuit(agentID);
            manager.RemoveCircuit (123456);
            manager.GetHashCode ();
        }
        [TestCase]
        public void TestTimeSpan ()
        {
            TimeSpan ts = new TimeSpan (0, 0, 1234567);
            DateTime stamp = new DateTime(1970, 1, 1) + ts;
            String result = stamp.ToLongTimeString();
        }
    }
}

