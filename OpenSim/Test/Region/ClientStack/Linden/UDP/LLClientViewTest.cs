#region Includes
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenSim.Region.ClientStack.LindenUDP;
using OpenMetaverse;
using OpenMetaverse.StructuredData;
#endregion

///////////////////////////////////////////////////////////////////////////////
// Copyright 2015 (c) by Open Sim All Rights Reserved.
//  
// Project:      Region
// Module:       LLClientViewTest.cs
// Description:  Tests for the LL Client View class in the OpenSim.Region.ClientStack.LindenUDP assembly.
//  
// Date:       Author:           Comments:
// 16.05.2015 21:44  akira     Module created.
///////////////////////////////////////////////////////////////////////////////
namespace OpenSim.Region.ClientStack.LindenUDPTest
{

    /// <summary>
    /// Tests for the LL Client View Class
    /// Documentation: Handles new client connections
    /// </summary>
    [TestFixture(Description="Tests for LL Client View")]
    public class LLClientViewTest
    {
        #region Class Variables
        private LLClientView _llClientView = null;
        #endregion

        #region Setup/Teardown

        /// <summary>
        /// Code that is run once for a suite of tests
        /// </summary>
        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {

        }

        /// <summary>
        /// Code that is run once after a suite of tests has finished executing
        /// </summary>
        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {

        }

        /// <summary>
        /// Code that is run before each test
        /// </summary>
        [SetUp]
        public void Initialize()
        {
            //New instance of LL Client View
            // _llClientView = new LLClientView(new Scene(),new LLUDPServer(),new LLUDPClient(),new AuthenticateResponse(),new UUID(),new UUID(),123);
        }

        /// <summary>
        /// Code that is run after each test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {
            //TODO:  Put dispose in here for _llClientView or delete this line
        }
        #endregion

        #region Property Tests


        /// <summary>
        /// Close Sync Lock Property Test
        /// Documentation:  Used to synchronise threads when client is being closed.
        /// Property Type:  Object
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void CloseSyncLockTest()
        {
            Object expected = new Object();
            // _llClientView.CloseSyncLock = expected;
            Assert.AreEqual(expected, _llClientView.CloseSyncLock, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.CloseSyncLock property test failed");
        }

        /// <summary>
        /// Debug Packet Level Property Test
        /// Documentation:  
        /// Property Type:  int
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void DebugPacketLevelTest()
        {
            int expected = 123;
            _llClientView.DebugPacketLevel = expected;
            Assert.AreEqual(expected, _llClientView.DebugPacketLevel, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.DebugPacketLevel property test failed");
        }

        /// <summary>
        /// Disable Facelights Property Test
        /// Documentation:  
        /// Property Type:  bool
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void DisableFacelightsTest()
        {
            bool expected = true;
            _llClientView.DisableFacelights = expected;
            Assert.AreEqual(expected, _llClientView.DisableFacelights, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.DisableFacelights property test failed");
        }

        /// <summary>
        /// Image Manager Property Test
        /// Documentation:  Handles UDP texture download.
        /// Property Type:  LLImageManager
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void ImageManagerTest()
        {
            // LLImageManager expected = new LLImageManager();
            // _llClientView.ImageManager = expected;
            // Assert.AreEqual(expected, _llClientView.ImageManager, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.ImageManager property test failed");
        }

        /// <summary>
        /// Is Active Property Test
        /// Documentation:  As well as it's function in IClientAPI, in LLClientView we are locking on this property in order to
        /// Property Type:  bool
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void IsActiveTest()
        {
            bool expected = true;
            _llClientView.IsActive = expected;
            Assert.AreEqual(expected, _llClientView.IsActive, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.IsActive property test failed");
        }

        /// <summary>
        /// Is Logging Out Property Test
        /// Documentation:  
        /// Property Type:  bool
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void IsLoggingOutTest()
        {
            bool expected = true;
            _llClientView.IsLoggingOut = expected;
            Assert.AreEqual(expected, _llClientView.IsLoggingOut, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.IsLoggingOut property test failed");
        }

        /// <summary>
        /// Request Asset Property Test
        /// Documentation:  
        /// Property Type:  event
        /// Access       :  Write Only
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void RequestAssetTest()
        {
            // event expected = new event();
            // _llClientView.RequestAsset = expected;
            //TODO: Write Only Property, Delete this line.  Assert.AreEqual(expected, _llClientView.RequestAsset, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.RequestAsset property test failed");
        }

        /// <summary>
        /// Scene Agent Property Test
        /// Documentation:  
        /// Property Type:  ISceneAgent
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SceneAgentTest()
        {
            // ISceneAgent expected = new ISceneAgent();
            // _llClientView.SceneAgent = expected;
            // Assert.AreEqual(expected, _llClientView.SceneAgent, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SceneAgent property test failed");
        }

        /// <summary>
        /// Script Reset Property Test
        /// Documentation:  
        /// Property Type:  event
        /// Access       :  Write Only
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void ScriptResetTest()
        {
            // event expected = new event();
            // _llClientView.ScriptReset = expected;
            //TODO: Write Only Property, Delete this line.  Assert.AreEqual(expected, _llClientView.ScriptReset, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.ScriptReset property test failed");
        }

        /// <summary>
        /// Start Pos Property Test
        /// Documentation:  We retain a single AgentUpdateArgs so that we can constantly reuse it rather than construct a new one for
        /// Property Type:  Vector3
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void StartPosTest()
        {
            Vector3 expected = new Vector3();
            _llClientView.StartPos = expected;
            Assert.AreEqual(expected, _llClientView.StartPos, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.StartPos property test failed");
        }

        /// <summary>
        /// Total Agent Updates Property Test
        /// Documentation:  This is a different way of processing packets then ProcessInPacket
        /// Property Type:  int
        /// Access       :  Read/Write
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void TotalAgentUpdatesTest()
        {
            int expected = 123;
            _llClientView.TotalAgentUpdates = expected;
            Assert.AreEqual(expected, _llClientView.TotalAgentUpdates, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.TotalAgentUpdates property test failed");
        }


        #endregion

        #region Method Tests


        /// <summary>
        /// Add Generic Packet Handler Method Test
        /// Documentation   :  
        /// Method Signature:  bool AddGenericPacketHandler(string MethodName, GenericMessage handler)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void AddGenericPacketHandlerTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
            bool expected = true;
            bool results;

            //Parameters
            string MethodName = "test";
            // GenericMessage handler = new GenericMessage();

            // results = _llClientView.AddGenericPacketHandler(MethodName, handler);
            // Assert.AreEqual(expected, results, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.AddGenericPacketHandler method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.AddGenericPacketHandler Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Add Local Packet Handler Method Test
        /// Documentation   :  Add a handler for the given packet type.
        /// Method Signature:  bool AddLocalPacketHandler(PacketType packetType, PacketMethod handler)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void AddLocalPacketHandler1Test()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
            bool expected = true;
            bool results;

            //Parameters
            // PacketType packetType = new PacketType();
            // PacketMethod handler = new PacketMethod();

            // results = _llClientView.AddLocalPacketHandler(packetType, handler);
            // Assert.AreEqual(expected, results, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.AddLocalPacketHandler1 method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.AddLocalPacketHandler1 Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Add Local Packet Handler Method Test
        /// Documentation   :  Add a handler for the given packet type.
        /// Method Signature:  bool AddLocalPacketHandler(PacketType packetType, PacketMethod handler, bool doAsync)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void AddLocalPacketHandler2Test()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
            bool expected = true;
            bool results;

            //Parameters
            // PacketType packetType = new PacketType();
            // PacketMethod handler = new PacketMethod();
			bool doAsync = true;

            // results = _llClientView.AddLocalPacketHandler(packetType, handler, doAsync);
            // Assert.AreEqual(expected, results, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.AddLocalPacketHandler2 method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.AddLocalPacketHandler2 Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Add Packet Handler Method Test
        /// Documentation   :  
        /// Method Signature:  bool AddPacketHandler(PacketType packetType, PacketMethod handler)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void AddPacketHandlerTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
            bool expected = true;
            bool results;

            //Parameters
            // PacketType packetType = new PacketType();
            // PacketMethod handler = new PacketMethod();

            // results = LLClientView.AddPacketHandler(packetType, handler);
            // Assert.AreEqual(expected, results, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.AddPacketHandler method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.AddPacketHandler Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Build Agent Alert Packet Method Test
        /// Documentation   :  Construct an agent alert packet
        /// Method Signature:  AgentAlertMessagePacket BuildAgentAlertPacket(string message, bool modal)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void BuildAgentAlertPacketTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
            // AgentAlertMessagePacket expected = new AgentAlertMessagePacket();
            // AgentAlertMessagePacket results;

            //Parameters
            string message = "test";
			bool modal = true;

            // results = _llClientView.BuildAgentAlertPacket(message, modal);
            // Assert.AreEqual(expected, results, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.BuildAgentAlertPacket method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.BuildAgentAlertPacket Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Check Agent Update Significance Method Test
        /// Documentation   :  This checks the update significance against the last update made.
        /// Method Signature:  bool CheckAgentUpdateSignificance(AgentUpdatePacket.AgentDataBlock x)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void CheckAgentUpdateSignificanceTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
            bool expected = true;
            bool results;

            //Parameters
            // AgentUpdatePacket.AgentDataBlock x = new AgentUpdatePacket.AgentDataBlock();

            // results = _llClientView.CheckAgentUpdateSignificance(x);
            // Assert.AreEqual(expected, results, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.CheckAgentUpdateSignificance method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.CheckAgentUpdateSignificance Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Close Method Test
        /// Documentation   :  
        /// Method Signature:  void Close()
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void Close1Test()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            
             _llClientView.Close();
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.Close1 method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.Close1 Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Close Method Test
        /// Documentation   :  
        /// Method Signature:  void Close(bool force)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void Close2Test()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            bool force = true;

             _llClientView.Close(force);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.Close2 method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.Close2 Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Close Without Checks Method Test
        /// Documentation   :  Closes down the client view without first checking whether it is active.
        /// Method Signature:  void CloseWithoutChecks()
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void CloseWithoutChecksTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            
             _llClientView.CloseWithoutChecks();
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.CloseWithoutChecks method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.CloseWithoutChecks Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Cross Region Method Test
        /// Documentation   :  
        /// Method Signature:  void CrossRegion(ulong newRegionHandle, Vector3 pos, Vector3 lookAt, IPEndPoint externalIPEndPoint, string capsURL)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void CrossRegionTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            ulong newRegionHandle = 123;
			Vector3 pos = new Vector3();
			Vector3 lookAt = new Vector3();
            // IPEndPoint externalIPEndPoint = new IPEndPoint();
			string capsURL = "test";

            // _llClientView.CrossRegion(newRegionHandle, pos, lookAt, externalIPEndPoint, capsURL);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.CrossRegion method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.CrossRegion Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Disconnect Method Test
        /// Documentation   :  
        /// Method Signature:  void Disconnect()
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void Disconnect1Test()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            
             _llClientView.Disconnect();
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.Disconnect1 method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.Disconnect1 Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Disconnect Method Test
        /// Documentation   :  
        /// Method Signature:  void Disconnect(string reason)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void Disconnect2Test()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            string reason = "test";

             _llClientView.Disconnect(reason);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.Disconnect2 method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.Disconnect2 Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Flush Prim Updates Method Test
        /// Documentation   :  
        /// Method Signature:  void FlushPrimUpdates()
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void FlushPrimUpdatesTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            
             _llClientView.FlushPrimUpdates();
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.FlushPrimUpdates method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.FlushPrimUpdates Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Get Client Info Method Test
        /// Documentation   :  
        /// Method Signature:  ClientInfo GetClientInfo()
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void GetClientInfoTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
            // ClientInfo expected = new ClientInfo();
            // ClientInfo results;

            //Parameters
            
            // results = _llClientView.GetClientInfo();
            // Assert.AreEqual(expected, results, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.GetClientInfo method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.GetClientInfo Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Get Client Option Method Test
        /// Documentation   :  
        /// Method Signature:  string GetClientOption(string option)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void GetClientOptionTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
            string expected = "test";
            string results;

            //Parameters
            string option = "test";

            results = _llClientView.GetClientOption(option);
            Assert.AreEqual(expected, results, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.GetClientOption method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.GetClientOption Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Get Group Powers Method Test
        /// Documentation   :  
        /// Method Signature:  ulong GetGroupPowers(UUID groupID)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void GetGroupPowersTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
            ulong expected = 123;
            ulong results;

            //Parameters
            UUID groupID = new UUID();

            results = _llClientView.GetGroupPowers(groupID);
            Assert.AreEqual(expected, results, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.GetGroupPowers method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.GetGroupPowers Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Get< T> Method Test
        /// Documentation   :  
        /// Method Signature:  T Get<T>()
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void GetTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
            object expected = new object();
            object results;

            //Parameters
            
            results = _llClientView.Get<object>();
            Assert.AreEqual(expected, results, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.Get<object> method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.Get<object> Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Get Throttles Packed Method Test
        /// Documentation   :  Get the current throttles for this client as a packed byte array
        /// Method Signature:  byte[] GetThrottlesPacked(float multiplier)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void GetThrottlesPackedTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
            byte[] expected = new byte[20];
            byte[] results;

            //Parameters
            float multiplier = 2.99999f;

            results = _llClientView.GetThrottlesPacked(multiplier);
            Assert.AreEqual(expected, results, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.GetThrottlesPacked method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.GetThrottlesPacked Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Handle Generic Message Method Test
        /// Documentation   :  This checks the camera update significance against the last update made.
        /// Method Signature:  bool HandleGenericMessage(IClientAPI sender, Packet pack)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void HandleGenericMessageTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
            bool expected = true;
            bool results;

            //Parameters
            // IClientAPI sender = new IClientAPI();
            // Packet pack = new Packet();

            // results = _llClientView.HandleGenericMessage(sender, pack);
            // Assert.AreEqual(expected, results, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.HandleGenericMessage method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.HandleGenericMessage Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Handle Object Group Request Method Test
        /// Documentation   :  
        /// Method Signature:  bool HandleObjectGroupRequest(IClientAPI sender, Packet Pack)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void HandleObjectGroupRequestTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
            bool expected = true;
            bool results;

            //Parameters
            // IClientAPI sender = new IClientAPI();
            // Packet Pack = new Packet();

            // results = _llClientView.HandleObjectGroupRequest(sender, Pack);
            // Assert.AreEqual(expected, results, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.HandleObjectGroupRequest method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.HandleObjectGroupRequest Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Inform Client Of Neighbour Method Test
        /// Documentation   :  Tell the client that the given neighbour region is ready to receive a child agent.
        /// Method Signature:  void InformClientOfNeighbour(ulong neighbourHandle, IPEndPoint neighbourEndPoint)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void InformClientOfNeighbourTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            ulong neighbourHandle = 123;
            // IPEndPoint neighbourEndPoint = new IPEndPoint();

            //  _llClientView.InformClientOfNeighbour(neighbourHandle, neighbourEndPoint);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.InformClientOfNeighbour method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.InformClientOfNeighbour Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// In Packet Method Test
        /// Documentation   :  Cruft?
        /// Method Signature:  void InPacket(object NewPack)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void InPacketTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            object NewPack = new object();

             _llClientView.InPacket(NewPack);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.InPacket method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.InPacket Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Is Group Member Method Test
        /// Documentation   :  
        /// Method Signature:  bool IsGroupMember(UUID groupID)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void IsGroupMemberTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
            bool expected = true;
            bool results;

            //Parameters
            UUID groupID = new UUID();

            results = _llClientView.IsGroupMember(groupID);
            Assert.AreEqual(expected, results, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.IsGroupMember method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.IsGroupMember Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Kick Method Test
        /// Documentation   :  
        /// Method Signature:  void Kick(string message)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void KickTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            string message = "test";

             _llClientView.Kick(message);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.Kick method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.Kick Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// LL Client View Constructor Test
        /// Documentation   :  Constructor
        /// Constructor Signature:  LLClientView(Scene scene, LLUDPServer udpServer, LLUDPClient udpClient, AuthenticateResponse sessionInfo, UUID agentId, UUID sessionId, uint circuitCode)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void LLClientViewConstructorTest()
        {
            System.DateTime constructorStartTime = System.DateTime.Now;
                        
            //Parameters
            // Scene scene = new Scene();
            // LLUDPServer udpServer = new LLUDPServer();
            // LLUDPClient udpClient = new LLUDPClient();
            // AuthenticateResponse sessionInfo = new AuthenticateResponse();
			UUID agentId = new UUID();
			UUID sessionId = new UUID();
			uint circuitCode = 123;

            // LLClientView llClientView = new LLClientView(scene, udpServer, udpClient, sessionInfo, agentId, sessionId, circuitCode);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.LLClientViewConstructor constructor test failed");

            System.TimeSpan constructorDuration = System.DateTime.Now.Subtract(constructorStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.LLClientViewConstructor Time Elapsed: {0}", constructorDuration.ToString()));
        }

        /// <summary>
        /// Move Agent Into Region Method Test
        /// Documentation   :  
        /// Method Signature:  void MoveAgentIntoRegion(RegionInfo regInfo, Vector3 pos, Vector3 look)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void MoveAgentIntoRegionTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            // RegionInfo regInfo = new RegionInfo();
			Vector3 pos = new Vector3();
			Vector3 look = new Vector3();

            // _llClientView.MoveAgentIntoRegion(regInfo, pos, look);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.MoveAgentIntoRegion method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.MoveAgentIntoRegion Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        ///  Method Test
        /// Documentation   :  
        /// Method Signature:  ObjectPropertyUpdate(ISceneEntity entity, uint flags, bool sendfam, bool sendobj)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void Test()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
            // ObjectPropertyUpdate(ISceneEntity expected = new ObjectPropertyUpdate(ISceneEntity;
            // ObjectPropertyUpdate(ISceneEntity results;

            //Parameters
            // ISceneEntity entity = new ISceneEntity();
			uint flags = 123;
			bool sendfam = true;
			bool sendobj = true;

            // results = _llClientView.(entity, flags, sendfam, sendobj);
            // Assert.AreEqual(expected, results, "OpenSim.Region.ClientStack.LindenUDP.LLClientView. method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView. Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// O Report Method Test
        /// Documentation   :  
        /// Method Signature:  OSDMap OReport(string uptime, string version)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void OReportTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
            OSDMap expected = new OSDMap();
            OSDMap results;

            //Parameters
            string uptime = "test";
			string version = "test";

            results = _llClientView.OReport(uptime, version);
            Assert.AreEqual(expected, results, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.OReport method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.OReport Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Populate Stats Method Test
        /// Documentation   :  
        /// Method Signature:  void PopulateStats(int inPackets, int outPackets, int unAckedBytes)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void PopulateStatsTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            int inPackets = 123;
			int outPackets = 123;
			int unAckedBytes = 123;

             _llClientView.PopulateStats(inPackets, outPackets, unAckedBytes);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.PopulateStats method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.PopulateStats Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Process In Packet Method Test
        /// Documentation   :  Entryway from the client to the simulator. All UDP packets from the client will end up here
        /// Method Signature:  void ProcessInPacket(Packet packet)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void ProcessInPacketTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            // Packet packet = new Packet();

            //  _llClientView.ProcessInPacket(packet);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.ProcessInPacket method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.ProcessInPacket Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Process Specific Packet Async Method Test
        /// Documentation   :  Try to process a packet using registered packet handlers
        /// Method Signature:  void ProcessSpecificPacketAsync(object state)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void ProcessSpecificPacketAsyncTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            object state = new object();

             _llClientView.ProcessSpecificPacketAsync(state);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.ProcessSpecificPacketAsync method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.ProcessSpecificPacketAsync Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Refresh Group Membership Method Test
        /// Documentation   :  
        /// Method Signature:  void RefreshGroupMembership()
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void RefreshGroupMembershipTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            
             _llClientView.RefreshGroupMembership();
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.RefreshGroupMembership method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.RefreshGroupMembership Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Report Method Test
        /// Documentation   :  
        /// Method Signature:  string Report()
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void ReportTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
            string expected = "test";
            string results;

            //Parameters
            
            results = _llClientView.Report();
            Assert.AreEqual(expected, results, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.Report method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.Report Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Reprioritize Updates Method Test
        /// Documentation   :  Requeue a list of EntityUpdates when they were not acknowledged by the client.
        /// Method Signature:  void ReprioritizeUpdates()
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void ReprioritizeUpdatesTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            
             _llClientView.ReprioritizeUpdates();
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.ReprioritizeUpdates method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.ReprioritizeUpdates Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Request Client Info Method Test
        /// Documentation   :  
        /// Method Signature:  AgentCircuitData RequestClientInfo()
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void RequestClientInfoTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
            // AgentCircuitData expected = new AgentCircuitData();
            // AgentCircuitData results;

            //Parameters
            
            // results = _llClientView.RequestClientInfo();
            // Assert.AreEqual(expected, results, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.RequestClientInfo method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.RequestClientInfo Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Request Map Blocks X Method Test
        /// Documentation   :  
        /// Method Signature:  void RequestMapBlocksX(int minX, int minY, int maxX, int maxY)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void RequestMapBlocksXTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            int minX = 123;
			int minY = 123;
			int maxX = 123;
			int maxY = 123;

             _llClientView.RequestMapBlocksX(minX, minY, maxX, maxY);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.RequestMapBlocksX method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.RequestMapBlocksX Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Request Map Layer Method Test
        /// Documentation   :  
        /// Method Signature:  void RequestMapLayer()
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void RequestMapLayerTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            
             _llClientView.RequestMapLayer();
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.RequestMapLayer method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.RequestMapLayer Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Abort Xfer Packet Method Test
        /// Documentation   :  
        /// Method Signature:  void SendAbortXferPacket(ulong xferID)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendAbortXferPacketTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            ulong xferID = 123;

             _llClientView.SendAbortXferPacket(xferID);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAbortXferPacket method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAbortXferPacket Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Accept Calling Card Method Test
        /// Documentation   :  
        /// Method Signature:  void SendAcceptCallingCard(UUID transactionID)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendAcceptCallingCardTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID transactionID = new UUID();

             _llClientView.SendAcceptCallingCard(transactionID);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAcceptCallingCard method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAcceptCallingCard Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Admin Response Method Test
        /// Documentation   :  
        /// Method Signature:  void SendAdminResponse(UUID Token, uint AdminLevel)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendAdminResponseTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID Token = new UUID();
			uint AdminLevel = 123;

             _llClientView.SendAdminResponse(Token, AdminLevel);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAdminResponse method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAdminResponse Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Agent Alert Message Method Test
        /// Documentation   :  Send an agent alert message to the client.
        /// Method Signature:  void SendAgentAlertMessage(string message, bool modal)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendAgentAlertMessageTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            string message = "test";
			bool modal = true;

             _llClientView.SendAgentAlertMessage(message, modal);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAgentAlertMessage method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAgentAlertMessage Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Agent Data Update Method Test
        /// Documentation   :  
        /// Method Signature:  void SendAgentDataUpdate(UUID agentid, UUID activegroupid, string firstname, string lastname, ulong grouppowers, string groupname, string grouptitle)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendAgentDataUpdateTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID agentid = new UUID();
			UUID activegroupid = new UUID();
			string firstname = "test";
			string lastname = "test";
			ulong grouppowers = 123;
			string groupname = "test";
			string grouptitle = "test";

             _llClientView.SendAgentDataUpdate(agentid, activegroupid, firstname, lastname, grouppowers, groupname, grouptitle);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAgentDataUpdate method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAgentDataUpdate Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Agent Drop Group Method Test
        /// Documentation   :  
        /// Method Signature:  void SendAgentDropGroup(UUID groupID)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendAgentDropGroupTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID groupID = new UUID();

             _llClientView.SendAgentDropGroup(groupID);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAgentDropGroup method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAgentDropGroup Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Agent Offline Method Test
        /// Documentation   :  
        /// Method Signature:  void SendAgentOffline(UUID[] agentIDs)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendAgentOfflineTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID[] agentIDs = new UUID[1];

             _llClientView.SendAgentOffline(agentIDs);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAgentOffline method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAgentOffline Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Agent Online Method Test
        /// Documentation   :  
        /// Method Signature:  void SendAgentOnline(UUID[] agentIDs)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendAgentOnlineTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID[] agentIDs = new UUID[1];

             _llClientView.SendAgentOnline(agentIDs);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAgentOnline method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAgentOnline Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Alert Message Method Test
        /// Documentation   :  Send an alert message to the client. On the Linden client (tested 1.19.1.4), this pops up a brief duration
        /// Method Signature:  void SendAlertMessage(string message)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendAlertMessageTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            string message = "test";

             _llClientView.SendAlertMessage(message);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAlertMessage method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAlertMessage Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Animations Method Test
        /// Documentation   :  
        /// Method Signature:  void SendAnimations(UUID[] animations, int[] seqs, UUID sourceAgentId, UUID[] objectIDs)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendAnimationsTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID[] animations = new UUID[1];
            int[] seqs = new int[1];
			UUID sourceAgentId = new UUID();
            UUID[] objectIDs = new UUID[1];

             _llClientView.SendAnimations(animations, seqs, sourceAgentId, objectIDs);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAnimations method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAnimations Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Appearance Method Test
        /// Documentation   :  
        /// Method Signature:  void SendAppearance(UUID agentID, byte[] visualParams, byte[] textureEntry)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendAppearanceTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID agentID = new UUID();
			byte[] visualParams = new byte[20];
			byte[] textureEntry = new byte[20];

             _llClientView.SendAppearance(agentID, visualParams, textureEntry);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAppearance method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAppearance Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Asset Not Found Method Test
        /// Documentation   :  
        /// Method Signature:  void SendAssetNotFound(AssetRequestToClient req)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendAssetNotFoundTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            // AssetRequestToClient req = new AssetRequestToClient();

            // _llClientView.SendAssetNotFound(req);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAssetNotFound method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAssetNotFound Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Asset Upload Complete Message Method Test
        /// Documentation   :  
        /// Method Signature:  void SendAssetUploadCompleteMessage(sbyte AssetType, bool Success, UUID AssetFullID)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendAssetUploadCompleteMessageTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            sbyte AssetType = new sbyte();
			bool Success = true;
			UUID AssetFullID = new UUID();

             _llClientView.SendAssetUploadCompleteMessage(AssetType, Success, AssetFullID);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAssetUploadCompleteMessage method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAssetUploadCompleteMessage Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Asset Method Test
        /// Documentation   :  
        /// Method Signature:  void SendAsset(AssetRequestToClient req)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendAssetTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            // AssetRequestToClient req = new AssetRequestToClient();

            // _llClientView.SendAsset(req);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAsset method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAsset Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Attached Sound Gain Change Method Test
        /// Documentation   :  
        /// Method Signature:  void SendAttachedSoundGainChange(UUID objectID, float gain)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendAttachedSoundGainChangeTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID objectID = new UUID();
			float gain = 2.99999f;

             _llClientView.SendAttachedSoundGainChange(objectID, gain);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAttachedSoundGainChange method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAttachedSoundGainChange Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Avatar Classified Reply Method Test
        /// Documentation   :  
        /// Method Signature:  void SendAvatarClassifiedReply(UUID targetID, Dictionary<UUID, string> classifieds)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendAvatarClassifiedReply1Test()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID targetID = new UUID();
			Dictionary<UUID, string> classifieds = new Dictionary<UUID, string>();

             _llClientView.SendAvatarClassifiedReply(targetID, classifieds);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAvatarClassifiedReply1 method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAvatarClassifiedReply1 Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Avatar Classified Reply Method Test
        /// Documentation   :  
        /// Method Signature:  void SendAvatarClassifiedReply(UUID targetID, UUID[] classifiedID, string[] name)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendAvatarClassifiedReply2Test()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID targetID = new UUID();
            UUID[] classifiedID = new UUID[1];
			string[] name = new string[20];

             _llClientView.SendAvatarClassifiedReply(targetID, classifiedID, name);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAvatarClassifiedReply2 method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAvatarClassifiedReply2 Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Avatar Data Immediate Method Test
        /// Documentation   :  Send an ObjectUpdate packet with information about an avatar
        /// Method Signature:  void SendAvatarDataImmediate(ISceneEntity avatar)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendAvatarDataImmediateTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            // ISceneEntity avatar = new ISceneEntity();

            // _llClientView.SendAvatarDataImmediate(avatar);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAvatarDataImmediate method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAvatarDataImmediate Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Avatar Groups Reply Method Test
        /// Documentation   :  
        /// Method Signature:  void SendAvatarGroupsReply(UUID avatarID, GroupMembershipData[] data)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendAvatarGroupsReplyTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID avatarID = new UUID();
            // GroupMembershipData[] data = new GroupMembershipData[1];

            // _llClientView.SendAvatarGroupsReply(avatarID, data);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAvatarGroupsReply method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAvatarGroupsReply Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Avatar Notes Reply Method Test
        /// Documentation   :  
        /// Method Signature:  void SendAvatarNotesReply(UUID targetID, string text)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendAvatarNotesReplyTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID targetID = new UUID();
			string text = "test";

             _llClientView.SendAvatarNotesReply(targetID, text);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAvatarNotesReply method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAvatarNotesReply Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Avatar Picker Reply Method Test
        /// Documentation   :  
        /// Method Signature:  void SendAvatarPickerReply(AvatarPickerReplyAgentDataArgs AgentData, List<AvatarPickerReplyDataArgs> Data)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendAvatarPickerReplyTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            // AvatarPickerReplyAgentDataArgs AgentData = new AvatarPickerReplyAgentDataArgs();
            // List<AvatarPickerReplyDataArgs> Data = new List<AvatarPickerReplyDataArgs>();

            // _llClientView.SendAvatarPickerReply(AgentData, Data);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAvatarPickerReply method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAvatarPickerReply Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Avatar Picks Reply Method Test
        /// Documentation   :  
        /// Method Signature:  void SendAvatarPicksReply(UUID targetID, Dictionary<UUID, string> picks)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendAvatarPicksReplyTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID targetID = new UUID();
			Dictionary<UUID, string> picks = new Dictionary<UUID, string>();

             _llClientView.SendAvatarPicksReply(targetID, picks);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAvatarPicksReply method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAvatarPicksReply Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Avatar Properties Method Test
        /// Documentation   :  
        /// Method Signature:  void SendAvatarProperties(UUID avatarID, string aboutText, string bornOn, Byte[] charterMember, string flAbout, uint flags, UUID flImageID, UUID imageID, string profileURL, UUID partnerID)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendAvatarPropertiesTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID avatarID = new UUID();
			string aboutText = "test";
			string bornOn = "test";
            Byte[] charterMember = new Byte[1];
			string flAbout = "test";
			uint flags = 123;
			UUID flImageID = new UUID();
			UUID imageID = new UUID();
			string profileURL = "test";
			UUID partnerID = new UUID();

             _llClientView.SendAvatarProperties(avatarID, aboutText, bornOn, charterMember, flAbout, flags, flImageID, imageID, profileURL, partnerID);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAvatarProperties method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendAvatarProperties Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Banned User List Method Test
        /// Documentation   :  
        /// Method Signature:  void SendBannedUserList(UUID invoice, EstateBan[] bl, uint estateID)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendBannedUserListTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID invoice = new UUID();
            // EstateBan[] bl = new EstateBan[1];
			uint estateID = 123;

            // _llClientView.SendBannedUserList(invoice, bl, estateID);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendBannedUserList method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendBannedUserList Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Blue Box Message Method Test
        /// Documentation   :  Send the client an Estate message blue box pop-down with a single OK button
        /// Method Signature:  void SendBlueBoxMessage(UUID FromAvatarID, String FromAvatarName, String Message)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendBlueBoxMessageTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID FromAvatarID = new UUID();
			String FromAvatarName = "test";
			String Message = "test";

             _llClientView.SendBlueBoxMessage(FromAvatarID, FromAvatarName, Message);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendBlueBoxMessage method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendBlueBoxMessage Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Bulk Update Inventory Method Test
        /// Documentation   :  Generate a bulk update inventory data block for the given item
        /// Method Signature:  void SendBulkUpdateInventory(InventoryNodeBase node)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendBulkUpdateInventoryTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            // InventoryNodeBase node = new InventoryNodeBase();

            // _llClientView.SendBulkUpdateInventory(node);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendBulkUpdateInventory method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendBulkUpdateInventory Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Cached Texture Response Method Test
        /// Documentation   :  Send a response back to a client when it asks the asset server (via the region server) if it has
        /// Method Signature:  void SendCachedTextureResponse(ISceneEntity avatar, int serial, List<CachedTextureResponseArg> cachedTextures)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendCachedTextureResponseTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            // ISceneEntity avatar = new ISceneEntity();
			int serial = 123;
            // List<CachedTextureResponseArg> cachedTextures = new List<CachedTextureResponseArg>();

            // _llClientView.SendCachedTextureResponse(avatar, serial, cachedTextures);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendCachedTextureResponse method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendCachedTextureResponse Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Camera Constraint Method Test
        /// Documentation   :  
        /// Method Signature:  void SendCameraConstraint(Vector4 ConstraintPlane)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendCameraConstraintTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            Vector4 ConstraintPlane = new Vector4();

             _llClientView.SendCameraConstraint(ConstraintPlane);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendCameraConstraint method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendCameraConstraint Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Chat Message Method Test
        /// Documentation   :  
        /// Method Signature:  void SendChatMessage( string message, byte type, Vector3 fromPos, string fromName, UUID fromAgentID, UUID ownerID, byte source, byte audible)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendChatMessageTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            string message = "test";
			byte type = 0x2A;
			Vector3 fromPos = new Vector3();
			string fromName = "test";
			UUID fromAgentID = new UUID();
			UUID ownerID = new UUID();
			byte source = 0x2A;
			byte audible = 0x2A;

             _llClientView.SendChatMessage(message, type, fromPos, fromName, fromAgentID, ownerID, source, audible);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendChatMessage method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendChatMessage Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Classified Info Reply Method Test
        /// Documentation   :  
        /// Method Signature:  void SendClassifiedInfoReply(UUID classifiedID, UUID creatorID, uint creationDate, uint expirationDate, uint category, string name, string description, UUID parcelID, uint parentEstate, UUID snapshotID, string simName, Vector3 globalPos, string parcelName, byte classifiedFlags, int price)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendClassifiedInfoReplyTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID classifiedID = new UUID();
			UUID creatorID = new UUID();
			uint creationDate = 123;
			uint expirationDate = 123;
			uint category = 123;
			string name = "test";
			string description = "test";
			UUID parcelID = new UUID();
			uint parentEstate = 123;
			UUID snapshotID = new UUID();
			string simName = "test";
			Vector3 globalPos = new Vector3();
			string parcelName = "test";
			byte classifiedFlags = 0x2A;
			int price = 123;

             _llClientView.SendClassifiedInfoReply(classifiedID, creatorID, creationDate, expirationDate, category, name, description, parcelID, parentEstate, snapshotID, simName, globalPos, parcelName, classifiedFlags, price);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendClassifiedInfoReply method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendClassifiedInfoReply Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Clear Follow Cam Properties Method Test
        /// Documentation   :  
        /// Method Signature:  void SendClearFollowCamProperties(UUID objectID)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendClearFollowCamPropertiesTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID objectID = new UUID();

             _llClientView.SendClearFollowCamProperties(objectID);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendClearFollowCamProperties method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendClearFollowCamProperties Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Cloud Data Method Test
        /// Documentation   :  Send the cloud matrix to the client
        /// Method Signature:  void SendCloudData(float[] cloudDensity)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendCloudDataTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            float[] cloudDensity = new float[1];

             _llClientView.SendCloudData(cloudDensity);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendCloudData method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendCloudData Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Coarse Location Update Method Test
        /// Documentation   :  
        /// Method Signature:  void SendCoarseLocationUpdate(List<UUID> users, List<Vector3> CoarseLocations)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendCoarseLocationUpdateTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            List<UUID> users = new List<UUID>();
			List<Vector3> CoarseLocations = new List<Vector3>();

             _llClientView.SendCoarseLocationUpdate(users, CoarseLocations);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendCoarseLocationUpdate method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendCoarseLocationUpdate Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Confirm Xfer Method Test
        /// Documentation   :  
        /// Method Signature:  void SendConfirmXfer(ulong xferID, uint PacketID)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendConfirmXferTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            ulong xferID = 123;
			uint PacketID = 123;

             _llClientView.SendConfirmXfer(xferID, PacketID);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendConfirmXfer method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendConfirmXfer Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Create Group Reply Method Test
        /// Documentation   :  
        /// Method Signature:  void SendCreateGroupReply(UUID groupID, bool success, string message)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendCreateGroupReplyTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID groupID = new UUID();
			bool success = true;
			string message = "test";

             _llClientView.SendCreateGroupReply(groupID, success, message);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendCreateGroupReply method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendCreateGroupReply Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Decline Calling Card Method Test
        /// Documentation   :  
        /// Method Signature:  void SendDeclineCallingCard(UUID transactionID)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendDeclineCallingCardTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID transactionID = new UUID();

             _llClientView.SendDeclineCallingCard(transactionID);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendDeclineCallingCard method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendDeclineCallingCard Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Detailed Estate Data Method Test
        /// Documentation   :  
        /// Method Signature:  void SendDetailedEstateData( UUID invoice, string estateName, uint estateID, uint parentEstate, uint estateFlags, uint sunPosition, UUID covenant, uint covenantChanged, string abuseEmail, UUID estateOwner)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendDetailedEstateDataTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID invoice = new UUID();
			string estateName = "test";
			uint estateID = 123;
			uint parentEstate = 123;
			uint estateFlags = 123;
			uint sunPosition = 123;
			UUID covenant = new UUID();
			uint covenantChanged = 123;
			string abuseEmail = "test";
			UUID estateOwner = new UUID();

             _llClientView.SendDetailedEstateData(invoice, estateName, estateID, parentEstate, estateFlags, sunPosition, covenant, covenantChanged, abuseEmail, estateOwner);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendDetailedEstateData method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendDetailedEstateData Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Dialog Method Test
        /// Documentation   :  
        /// Method Signature:  void SendDialog( string objectname, UUID objectID, UUID ownerID, string ownerFirstName, string ownerLastName, string msg, UUID textureID, int ch, string[] buttonlabels)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendDialogTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            string objectname = "test";
			UUID objectID = new UUID();
			UUID ownerID = new UUID();
			string ownerFirstName = "test";
			string ownerLastName = "test";
			string msg = "test";
			UUID textureID = new UUID();
			int ch = 123;
			string[] buttonlabels = new string[20];

             _llClientView.SendDialog(objectname, objectID, ownerID, ownerFirstName, ownerLastName, msg, textureID, ch, buttonlabels);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendDialog method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendDialog Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Dir Classified Reply Method Test
        /// Documentation   :  
        /// Method Signature:  void SendDirClassifiedReply(UUID queryID, DirClassifiedReplyData[] data)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendDirClassifiedReplyTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID queryID = new UUID();
            // DirClassifiedReplyData[] data = new DirClassifiedReplyData[1];

            // _llClientView.SendDirClassifiedReply(queryID, data);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendDirClassifiedReply method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendDirClassifiedReply Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Dir Events Reply Method Test
        /// Documentation   :  
        /// Method Signature:  void SendDirEventsReply(UUID queryID, DirEventsReplyData[] data)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendDirEventsReplyTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID queryID = new UUID();
            // DirEventsReplyData[] data = new DirEventsReplyData[1];

            // _llClientView.SendDirEventsReply(queryID, data);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendDirEventsReply method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendDirEventsReply Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Dir Groups Reply Method Test
        /// Documentation   :  
        /// Method Signature:  void SendDirGroupsReply(UUID queryID, DirGroupsReplyData[] data)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendDirGroupsReplyTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID queryID = new UUID();
            // DirGroupsReplyData[] data = new DirGroupsReplyData[1];

            // _llClientView.SendDirGroupsReply(queryID, data);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendDirGroupsReply method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendDirGroupsReply Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Dir Land Reply Method Test
        /// Documentation   :  
        /// Method Signature:  void SendDirLandReply(UUID queryID, DirLandReplyData[] data)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendDirLandReplyTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID queryID = new UUID();
            // DirLandReplyData[] data = new DirLandReplyData[1];

            // _llClientView.SendDirLandReply(queryID, data);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendDirLandReply method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendDirLandReply Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Dir People Reply Method Test
        /// Documentation   :  
        /// Method Signature:  void SendDirPeopleReply(UUID queryID, DirPeopleReplyData[] data)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendDirPeopleReplyTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID queryID = new UUID();
            // DirPeopleReplyData[] data = new DirPeopleReplyData[1];

            // _llClientView.SendDirPeopleReply(queryID, data);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendDirPeopleReply method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendDirPeopleReply Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Dir Places Reply Method Test
        /// Documentation   :  
        /// Method Signature:  void SendDirPlacesReply(UUID queryID, DirPlacesReplyData[] data)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendDirPlacesReplyTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID queryID = new UUID();
            // DirPlacesReplyData[] data = new DirPlacesReplyData[1];

            // _llClientView.SendDirPlacesReply(queryID, data);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendDirPlacesReply method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendDirPlacesReply Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Dir Popular Reply Method Test
        /// Documentation   :  
        /// Method Signature:  void SendDirPopularReply(UUID queryID, DirPopularReplyData[] data)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendDirPopularReplyTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID queryID = new UUID();
            // DirPopularReplyData[] data = new DirPopularReplyData[1];

            // _llClientView.SendDirPopularReply(queryID, data);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendDirPopularReply method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendDirPopularReply Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Economy Data Method Test
        /// Documentation   :  
        /// Method Signature:  void SendEconomyData(float EnergyEfficiency, int ObjectCapacity, int ObjectCount, int PriceEnergyUnit, int PriceGroupCreate, int PriceObjectClaim, float PriceObjectRent, float PriceObjectScaleFactor, int PriceParcelClaim, float PriceParcelClaimFactor, int PriceParcelRent, int PricePublicObjectDecay, int PricePublicObjectDelete, int PriceRentLight, int PriceUpload, int TeleportMinPrice, float TeleportPriceExponent)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendEconomyDataTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            float EnergyEfficiency = 2.99999f;
			int ObjectCapacity = 123;
			int ObjectCount = 123;
			int PriceEnergyUnit = 123;
			int PriceGroupCreate = 123;
			int PriceObjectClaim = 123;
			float PriceObjectRent = 2.99999f;
			float PriceObjectScaleFactor = 2.99999f;
			int PriceParcelClaim = 123;
			float PriceParcelClaimFactor = 2.99999f;
			int PriceParcelRent = 123;
			int PricePublicObjectDecay = 123;
			int PricePublicObjectDelete = 123;
			int PriceRentLight = 123;
			int PriceUpload = 123;
			int TeleportMinPrice = 123;
			float TeleportPriceExponent = 2.99999f;

             _llClientView.SendEconomyData(EnergyEfficiency, ObjectCapacity, ObjectCount, PriceEnergyUnit, PriceGroupCreate, PriceObjectClaim, PriceObjectRent, PriceObjectScaleFactor, PriceParcelClaim, PriceParcelClaimFactor, PriceParcelRent, PricePublicObjectDecay, PricePublicObjectDelete, PriceRentLight, PriceUpload, TeleportMinPrice, TeleportPriceExponent);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendEconomyData method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendEconomyData Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Eject Group Member Reply Method Test
        /// Documentation   :  
        /// Method Signature:  void SendEjectGroupMemberReply(UUID agentID, UUID groupID, bool success)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendEjectGroupMemberReplyTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID agentID = new UUID();
			UUID groupID = new UUID();
			bool success = true;

             _llClientView.SendEjectGroupMemberReply(agentID, groupID, success);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendEjectGroupMemberReply method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendEjectGroupMemberReply Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Entity Update Method Test
        /// Documentation   :  Generate one of the object update packets based on PrimUpdateFlags
        /// Method Signature:  void SendEntityUpdate(ISceneEntity entity, PrimUpdateFlags updateFlags)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendEntityUpdateTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            // ISceneEntity entity = new ISceneEntity();
            // PrimUpdateFlags updateFlags = new PrimUpdateFlags();

            // _llClientView.SendEntityUpdate(entity, updateFlags);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendEntityUpdate method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendEntityUpdate Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Estate Covenant Information Method Test
        /// Documentation   :  
        /// Method Signature:  void SendEstateCovenantInformation(UUID covenant)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendEstateCovenantInformationTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID covenant = new UUID();

             _llClientView.SendEstateCovenantInformation(covenant);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendEstateCovenantInformation method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendEstateCovenantInformation Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Estate List Method Test
        /// Documentation   :  
        /// Method Signature:  void SendEstateList(UUID invoice, int code, UUID[] Data, uint estateID)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendEstateListTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID invoice = new UUID();
			int code = 123;
            UUID[] Data = new UUID[1];
			uint estateID = 123;

             _llClientView.SendEstateList(invoice, code, Data, estateID);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendEstateList method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendEstateList Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Event Info Reply Method Test
        /// Documentation   :  
        /// Method Signature:  void SendEventInfoReply(EventData data)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendEventInfoReplyTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            // EventData data = new EventData();

            // _llClientView.SendEventInfoReply(data);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendEventInfoReply method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendEventInfoReply Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Force Client Select Objects Method Test
        /// Documentation   :  
        /// Method Signature:  void SendForceClientSelectObjects(List<uint> ObjectIDs)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendForceClientSelectObjectsTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            List<uint> ObjectIDs = new List<uint>();

             _llClientView.SendForceClientSelectObjects(ObjectIDs);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendForceClientSelectObjects method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendForceClientSelectObjects Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Generic Message Method Test
        /// Documentation   :  
        /// Method Signature:  void SendGenericMessage(string method, UUID invoice, List<byte[]> message)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendGenericMessage1Test()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            string method = "test";
			UUID invoice = new UUID();
            // List<byte[]> message = new List<byte[1]>;

            //  _llClientView.SendGenericMessage(method, invoice, message);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendGenericMessage1 method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendGenericMessage1 Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Generic Message Method Test
        /// Documentation   :  
        /// Method Signature:  void SendGenericMessage(string method, UUID invoice, List<string> message)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendGenericMessage2Test()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            string method = "test";
			UUID invoice = new UUID();
			List<string> message = new List<string>();

             _llClientView.SendGenericMessage(method, invoice, message);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendGenericMessage2 method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendGenericMessage2 Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Group Accounting Details Method Test
        /// Documentation   :  
        /// Method Signature:  void SendGroupAccountingDetails(IClientAPI sender,UUID groupID, UUID transactionID, UUID sessionID, int amt)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendGroupAccountingDetailsTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            // IClientAPI sender = new IClientAPI();
			UUID groupID = new UUID();
			UUID transactionID = new UUID();
			UUID sessionID = new UUID();
			int amt = 123;

            // _llClientView.SendGroupAccountingDetails(sender, groupID, transactionID, sessionID, amt);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendGroupAccountingDetails method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendGroupAccountingDetails Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Group Accounting Summary Method Test
        /// Documentation   :  
        /// Method Signature:  void SendGroupAccountingSummary(IClientAPI sender,UUID groupID, uint moneyAmt, int totalTier, int usedTier)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendGroupAccountingSummaryTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            // IClientAPI sender = new IClientAPI();
			UUID groupID = new UUID();
			uint moneyAmt = 123;
			int totalTier = 123;
			int usedTier = 123;

            // _llClientView.SendGroupAccountingSummary(sender, groupID, moneyAmt, totalTier, usedTier);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendGroupAccountingSummary method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendGroupAccountingSummary Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Group Active Proposals Method Test
        /// Documentation   :  
        /// Method Signature:  void SendGroupActiveProposals(UUID groupID, UUID transactionID, GroupActiveProposals[] Proposals)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendGroupActiveProposalsTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID groupID = new UUID();
			UUID transactionID = new UUID();
            // GroupActiveProposals[] Proposals = new GroupActiveProposals[1];

            // _llClientView.SendGroupActiveProposals(groupID, transactionID, Proposals);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendGroupActiveProposals method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendGroupActiveProposals Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Group Membership Method Test
        /// Documentation   :  
        /// Method Signature:  void SendGroupMembership(GroupMembershipData[] GroupMembership)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendGroupMembershipTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            // GroupMembershipData[] GroupMembership = new GroupMembershipData[1];

            // _llClientView.SendGroupMembership(GroupMembership);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendGroupMembership method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendGroupMembership Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Group Name Reply Method Test
        /// Documentation   :  
        /// Method Signature:  void SendGroupNameReply(UUID groupLLUID, string GroupName)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendGroupNameReplyTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID groupLLUID = new UUID();
			string GroupName = "test";

             _llClientView.SendGroupNameReply(groupLLUID, GroupName);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendGroupNameReply method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendGroupNameReply Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Group Transactions Summary Details Method Test
        /// Documentation   :  
        /// Method Signature:  void SendGroupTransactionsSummaryDetails(IClientAPI sender,UUID groupID, UUID transactionID, UUID sessionID, int amt)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendGroupTransactionsSummaryDetailsTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            // IClientAPI sender = new IClientAPI();
			UUID groupID = new UUID();
			UUID transactionID = new UUID();
			UUID sessionID = new UUID();
			int amt = 123;

            // _llClientView.SendGroupTransactionsSummaryDetails(sender, groupID, transactionID, sessionID, amt);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendGroupTransactionsSummaryDetails method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendGroupTransactionsSummaryDetails Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Group Vote History Method Test
        /// Documentation   :  
        /// Method Signature:  void SendGroupVoteHistory(UUID groupID, UUID transactionID, GroupVoteHistory[] Votes)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendGroupVoteHistoryTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID groupID = new UUID();
			UUID transactionID = new UUID();
            // GroupVoteHistory[] Votes = new GroupVoteHistory[1];

            // _llClientView.SendGroupVoteHistory(groupID, transactionID, Votes);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendGroupVoteHistory method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendGroupVoteHistory Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Health Method Test
        /// Documentation   :  
        /// Method Signature:  void SendHealth(float health)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendHealthTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            float health = 2.99999f;

             _llClientView.SendHealth(health);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendHealth method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendHealth Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Image First Part Method Test
        /// Documentation   :  
        /// Method Signature:  void SendImageFirstPart( ushort numParts, UUID ImageUUID, uint ImageSize, byte[] ImageData, byte imageCodec)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendImageFirstPartTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            ushort numParts = 123;
			UUID ImageUUID = new UUID();
			uint ImageSize = 123;
			byte[] ImageData = new byte[20];
			byte imageCodec = 0x2A;

             _llClientView.SendImageFirstPart(numParts, ImageUUID, ImageSize, ImageData, imageCodec);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendImageFirstPart method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendImageFirstPart Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Image Next Part Method Test
        /// Documentation   :  
        /// Method Signature:  void SendImageNextPart(ushort partNumber, UUID imageUuid, byte[] imageData)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendImageNextPartTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            ushort partNumber = 123;
			UUID imageUuid = new UUID();
			byte[] imageData = new byte[20];

             _llClientView.SendImageNextPart(partNumber, imageUuid, imageData);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendImageNextPart method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendImageNextPart Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Image Not Found Method Test
        /// Documentation   :  
        /// Method Signature:  void SendImageNotFound(UUID imageid)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendImageNotFoundTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID imageid = new UUID();

             _llClientView.SendImageNotFound(imageid);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendImageNotFound method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendImageNotFound Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Initiate Download Method Test
        /// Documentation   :  
        /// Method Signature:  void SendInitiateDownload(string simFileName, string clientFileName)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendInitiateDownloadTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            string simFileName = "test";
			string clientFileName = "test";

             _llClientView.SendInitiateDownload(simFileName, clientFileName);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendInitiateDownload method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendInitiateDownload Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Instant Message Method Test
        /// Documentation   :  Send an instant message to this client
        /// Method Signature:  void SendInstantMessage(GridInstantMessage im)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendInstantMessageTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            // GridInstantMessage im = new GridInstantMessage();

            // _llClientView.SendInstantMessage(im);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendInstantMessage method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendInstantMessage Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Inventory Folder Details Method Test
        /// Documentation   :  Send information about the items contained in a folder to the client.
        /// Method Signature:  void SendInventoryFolderDetails(UUID ownerID, UUID folderID, List<InventoryItemBase> items, List<InventoryFolderBase> folders, int version, bool fetchFolders, bool fetchItems)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendInventoryFolderDetailsTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID ownerID = new UUID();
			UUID folderID = new UUID();
            // List<InventoryItemBase> items = new List<InventoryItemBase>();
            // List<InventoryFolderBase> folders = new List<InventoryFolderBase>();
			int version = 123;
			bool fetchFolders = true;
			bool fetchItems = true;

            // _llClientView.SendInventoryFolderDetails(ownerID, folderID, items, folders, version, fetchFolders, fetchItems);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendInventoryFolderDetails method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendInventoryFolderDetails Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Inventory Item Create Update Method Test
        /// Documentation   :  
        /// Method Signature:  void SendInventoryItemCreateUpdate(InventoryItemBase Item, uint callbackId)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendInventoryItemCreateUpdateTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            // InventoryItemBase Item = new InventoryItemBase();
			uint callbackId = 123;

            // _llClientView.SendInventoryItemCreateUpdate(Item, callbackId);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendInventoryItemCreateUpdate method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendInventoryItemCreateUpdate Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Inventory Item Details Method Test
        /// Documentation   :  
        /// Method Signature:  void SendInventoryItemDetails(UUID ownerID, InventoryItemBase item)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendInventoryItemDetailsTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID ownerID = new UUID();
            // InventoryItemBase item = new InventoryItemBase();

            // _llClientView.SendInventoryItemDetails(ownerID, item);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendInventoryItemDetails method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendInventoryItemDetails Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Join Group Reply Method Test
        /// Documentation   :  
        /// Method Signature:  void SendJoinGroupReply(UUID groupID, bool success)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendJoinGroupReplyTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID groupID = new UUID();
			bool success = true;

             _llClientView.SendJoinGroupReply(groupID, success);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendJoinGroupReply method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendJoinGroupReply Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Kill Object Method Test
        /// Documentation   :  
        /// Method Signature:  void SendKillObject(List<uint> localIDs)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendKillObjectTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            List<uint> localIDs = new List<uint>();

             _llClientView.SendKillObject(localIDs);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendKillObject method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendKillObject Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Land Access List Data Method Test
        /// Documentation   :  
        /// Method Signature:  void SendLandAccessListData(List<LandAccessEntry> accessList, uint accessFlag, int localLandID)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendLandAccessListDataTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            // List<LandAccessEntry> accessList = new List<LandAccessEntry>();
			uint accessFlag = 123;
			int localLandID = 123;

            // _llClientView.SendLandAccessListData(accessList, accessFlag, localLandID);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendLandAccessListData method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendLandAccessListData Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Land Object Owners Method Test
        /// Documentation   :  
        /// Method Signature:  void SendLandObjectOwners(LandData land, List<UUID> groups, Dictionary<UUID, int> ownersAndCount)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendLandObjectOwnersTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            // LandData land = new LandData();
			List<UUID> groups = new List<UUID>();
			Dictionary<UUID, int> ownersAndCount = new Dictionary<UUID, int>();

            // _llClientView.SendLandObjectOwners(land, groups, ownersAndCount);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendLandObjectOwners method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendLandObjectOwners Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Land Parcel Overlay Method Test
        /// Documentation   :  
        /// Method Signature:  void SendLandParcelOverlay(byte[] data, int sequence_id)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendLandParcelOverlayTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            byte[] data = new byte[20];
			int sequence_id = 123;

             _llClientView.SendLandParcelOverlay(data, sequence_id);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendLandParcelOverlay method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendLandParcelOverlay Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Land Properties Method Test
        /// Documentation   :  
        /// Method Signature:  void SendLandProperties( int sequence_id, bool snap_selection, int request_result, ILandObject lo, float simObjectBonusFactor, int parcelObjectCapacity, int simObjectCapacity, uint regionFlags)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendLandPropertiesTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            int sequence_id = 123;
			bool snap_selection = true;
			int request_result = 123;
            // ILandObject lo = new ILandObject();
			float simObjectBonusFactor = 2.99999f;
			int parcelObjectCapacity = 123;
			int simObjectCapacity = 123;
			uint regionFlags = 123;

            // _llClientView.SendLandProperties(sequence_id, snap_selection, request_result, lo, simObjectBonusFactor, parcelObjectCapacity, simObjectCapacity, regionFlags);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendLandProperties method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendLandProperties Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Land Stat Reply Method Test
        /// Documentation   :  
        /// Method Signature:  void SendLandStatReply(uint reportType, uint requestFlags, uint resultCount, LandStatReportItem[] lsrpia)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendLandStatReplyTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            uint reportType = 123;
			uint requestFlags = 123;
			uint resultCount = 123;
            // LandStatReportItem[] lsrpia = new LandStatReportItem[1];

            // _llClientView.SendLandStatReply(reportType, requestFlags, resultCount, lsrpia);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendLandStatReply method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendLandStatReply Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Layer Data Method Test
        /// Documentation   :  Send the region heightmap to the client
        /// Method Signature:  void SendLayerData(float[] map)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendLayerData1Test()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            float[] map = new float[1];

             _llClientView.SendLayerData(map);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendLayerData1 method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendLayerData1 Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Layer Data Method Test
        /// Documentation   :  Sends a set of four patches (x, x+1, ..., x+3) to the client
        /// Method Signature:  void SendLayerData(int px, int py, float[] map)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendLayerData2Test()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            int px = 123;
			int py = 123;
            float[] map = new float[1];

             _llClientView.SendLayerData(px, py, map);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendLayerData2 method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendLayerData2 Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Layer Data Method Test
        /// Documentation   :  Sends a terrain packet for the point specified.
        /// Method Signature:  void SendLayerData(int px, int py, HeightMapTerrainData terrData)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendLayerData3Test()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            int px = 123;
			int py = 123;
            // HeightMapTerrainData terrData = new HeightMapTerrainData();

            // _llClientView.SendLayerData(px, py, terrData);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendLayerData3 method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendLayerData3 Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Leave Group Reply Method Test
        /// Documentation   :  
        /// Method Signature:  void SendLeaveGroupReply(UUID groupID, bool success)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendLeaveGroupReplyTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID groupID = new UUID();
			bool success = true;

             _llClientView.SendLeaveGroupReply(groupID, success);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendLeaveGroupReply method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendLeaveGroupReply Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Load Universal Resource Locator Method Test
        /// Documentation   :  
        /// Method Signature:  void SendLoadURL(string objectname, UUID objectID, UUID ownerID, bool groupOwned, string message, string url)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendLoadURLTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            string objectname = "test";
			UUID objectID = new UUID();
			UUID ownerID = new UUID();
			bool groupOwned = true;
			string message = "test";
			string url = "test";

             _llClientView.SendLoadURL(objectname, objectID, ownerID, groupOwned, message, url);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendLoadURL method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendLoadURL Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Local Teleport Method Test
        /// Documentation   :  
        /// Method Signature:  void SendLocalTeleport(Vector3 position, Vector3 lookAt, uint flags)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendLocalTeleportTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            Vector3 position = new Vector3();
			Vector3 lookAt = new Vector3();
			uint flags = 123;

             _llClientView.SendLocalTeleport(position, lookAt, flags);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendLocalTeleport method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendLocalTeleport Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Logout Packet Method Test
        /// Documentation   :  
        /// Method Signature:  void SendLogoutPacket()
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendLogoutPacketTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            
             _llClientView.SendLogoutPacket();
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendLogoutPacket method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendLogoutPacket Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Map Block Method Test
        /// Documentation   :  
        /// Method Signature:  void SendMapBlock(List<MapBlockData> mapBlocks, uint flag)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendMapBlockTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            // List<MapBlockData> mapBlocks = new List<MapBlockData>();
			uint flag = 123;

            // _llClientView.SendMapBlock(mapBlocks, flag);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendMapBlock method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendMapBlock Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Map Item Reply Method Test
        /// Documentation   :  
        /// Method Signature:  void SendMapItemReply(mapItemReply[] replies, uint mapitemtype, uint flags)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendMapItemReplyTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            // mapItemReply[] replies = new mapItemReply[1];
			uint mapitemtype = 123;
			uint flags = 123;

            // _llClientView.SendMapItemReply(replies, mapitemtype, flags);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendMapItemReply method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendMapItemReply Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Money Balance Method Test
        /// Documentation   :  
        /// Method Signature:  void SendMoneyBalance(UUID transaction, bool success, byte[] description, int balance, int transactionType, UUID sourceID, bool sourceIsGroup, UUID destID, bool destIsGroup, int amount, string item)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendMoneyBalanceTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID transaction = new UUID();
			bool success = true;
			byte[] description = new byte[20];
			int balance = 123;
			int transactionType = 123;
			UUID sourceID = new UUID();
			bool sourceIsGroup = true;
			UUID destID = new UUID();
			bool destIsGroup = true;
			int amount = 123;
			string item = "test";

             _llClientView.SendMoneyBalance(transaction, success, description, balance, transactionType, sourceID, sourceIsGroup, destID, destIsGroup, amount, item);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendMoneyBalance method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendMoneyBalance Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Mute List Update Method Test
        /// Documentation   :  
        /// Method Signature:  void SendMuteListUpdate(string filename)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendMuteListUpdateTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            string filename = "test";

             _llClientView.SendMuteListUpdate(filename);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendMuteListUpdate method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendMuteListUpdate Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Name Reply Method Test
        /// Documentation   :  
        /// Method Signature:  void SendNameReply(UUID profileId, string firstname, string lastname)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendNameReplyTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID profileId = new UUID();
			string firstname = "test";
			string lastname = "test";

             _llClientView.SendNameReply(profileId, firstname, lastname);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendNameReply method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendNameReply Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Object Properties Family Data Method Test
        /// Documentation   :  
        /// Method Signature:  void SendObjectPropertiesFamilyData(ISceneEntity entity, uint requestFlags)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendObjectPropertiesFamilyDataTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            // ISceneEntity entity = new ISceneEntity();
			uint requestFlags = 123;

            // _llClientView.SendObjectPropertiesFamilyData(entity, requestFlags);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendObjectPropertiesFamilyData method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendObjectPropertiesFamilyData Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Object Properties Reply Method Test
        /// Documentation   :  
        /// Method Signature:  void SendObjectPropertiesReply(ISceneEntity entity)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendObjectPropertiesReplyTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            // ISceneEntity entity = new ISceneEntity();

            // _llClientView.SendObjectPropertiesReply(entity);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendObjectPropertiesReply method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendObjectPropertiesReply Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Offer Calling Card Method Test
        /// Documentation   :  
        /// Method Signature:  void SendOfferCallingCard(UUID srcID, UUID transactionID)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendOfferCallingCardTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID srcID = new UUID();
			UUID transactionID = new UUID();

             _llClientView.SendOfferCallingCard(srcID, transactionID);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendOfferCallingCard method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendOfferCallingCard Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Parcel Dwell Reply Method Test
        /// Documentation   :  
        /// Method Signature:  void SendParcelDwellReply(int localID, UUID parcelID, float dwell)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendParcelDwellReplyTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            int localID = 123;
			UUID parcelID = new UUID();
			float dwell = 2.99999f;

             _llClientView.SendParcelDwellReply(localID, parcelID, dwell);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendParcelDwellReply method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendParcelDwellReply Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Parcel Info Method Test
        /// Documentation   :  
        /// Method Signature:  void SendParcelInfo(RegionInfo info, LandData land, UUID parcelID, uint x, uint y)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendParcelInfoTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            // RegionInfo info = new RegionInfo();
            // LandData land = new LandData();
			UUID parcelID = new UUID();
			uint x = 123;
			uint y = 123;

            // _llClientView.SendParcelInfo(info, land, parcelID, x, y);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendParcelInfo method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendParcelInfo Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Parcel Media Command Method Test
        /// Documentation   :  
        /// Method Signature:  void SendParcelMediaCommand(uint flags, ParcelMediaCommandEnum command, float time)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendParcelMediaCommandTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            uint flags = 123;
            // ParcelMediaCommandEnum command = new ParcelMediaCommandEnum();
			float time = 2.99999f;

            // _llClientView.SendParcelMediaCommand(flags, command, time);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendParcelMediaCommand method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendParcelMediaCommand Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Parcel Media Update Method Test
        /// Documentation   :  
        /// Method Signature:  void SendParcelMediaUpdate(string mediaUrl, UUID mediaTextureID, byte autoScale, string mediaType, string mediaDesc, int mediaWidth, int mediaHeight, byte mediaLoop)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendParcelMediaUpdateTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            string mediaUrl = "test";
			UUID mediaTextureID = new UUID();
			byte autoScale = 0x2A;
			string mediaType = "test";
			string mediaDesc = "test";
			int mediaWidth = 123;
			int mediaHeight = 123;
			byte mediaLoop = 0x2A;

             _llClientView.SendParcelMediaUpdate(mediaUrl, mediaTextureID, autoScale, mediaType, mediaDesc, mediaWidth, mediaHeight, mediaLoop);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendParcelMediaUpdate method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendParcelMediaUpdate Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Part Physics Proprieties Method Test
        /// Documentation   :  
        /// Method Signature:  void SendPartPhysicsProprieties(ISceneEntity entity)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendPartPhysicsProprietiesTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            // ISceneEntity entity = new ISceneEntity();

            // _llClientView.SendPartPhysicsProprieties(entity);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendPartPhysicsProprieties method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendPartPhysicsProprieties Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Pay Price Method Test
        /// Documentation   :  
        /// Method Signature:  void SendPayPrice(UUID objectID, int[] payPrice)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendPayPriceTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID objectID = new UUID();
            int[] payPrice = new int[1];

             _llClientView.SendPayPrice(objectID, payPrice);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendPayPrice method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendPayPrice Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Pick Info Reply Method Test
        /// Documentation   :  
        /// Method Signature:  void SendPickInfoReply(UUID pickID, UUID creatorID, bool topPick, UUID parcelID, string name, string desc, UUID snapshotID, string user, string originalName, string simName, Vector3 posGlobal, int sortOrder, bool enabled)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendPickInfoReplyTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID pickID = new UUID();
			UUID creatorID = new UUID();
			bool topPick = true;
			UUID parcelID = new UUID();
			string name = "test";
			string desc = "test";
			UUID snapshotID = new UUID();
			string user = "test";
			string originalName = "test";
			string simName = "test";
			Vector3 posGlobal = new Vector3();
			int sortOrder = 123;
			bool enabled = true;

             _llClientView.SendPickInfoReply(pickID, creatorID, topPick, parcelID, name, desc, snapshotID, user, originalName, simName, posGlobal, sortOrder, enabled);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendPickInfoReply method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendPickInfoReply Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Play Attached Sound Method Test
        /// Documentation   :  
        /// Method Signature:  void SendPlayAttachedSound(UUID soundID, UUID objectID, UUID ownerID, float gain, byte flags)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendPlayAttachedSoundTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID soundID = new UUID();
			UUID objectID = new UUID();
			UUID ownerID = new UUID();
			float gain = 2.99999f;
			byte flags = 0x2A;

             _llClientView.SendPlayAttachedSound(soundID, objectID, ownerID, gain, flags);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendPlayAttachedSound method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendPlayAttachedSound Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Pre Load Sound Method Test
        /// Documentation   :  
        /// Method Signature:  void SendPreLoadSound(UUID objectID, UUID ownerID, UUID soundID)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendPreLoadSoundTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID objectID = new UUID();
			UUID ownerID = new UUID();
			UUID soundID = new UUID();

             _llClientView.SendPreLoadSound(objectID, ownerID, soundID);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendPreLoadSound method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendPreLoadSound Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Rebake Avatar Textures Method Test
        /// Documentation   :  Calculate the number of packets required to send the asset to the client.
        /// Method Signature:  void SendRebakeAvatarTextures(UUID textureID)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendRebakeAvatarTexturesTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID textureID = new UUID();

             _llClientView.SendRebakeAvatarTextures(textureID);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendRebakeAvatarTextures method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendRebakeAvatarTextures Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Region Handle Method Test
        /// Documentation   :  
        /// Method Signature:  void SendRegionHandle(UUID regionID, ulong handle)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendRegionHandleTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID regionID = new UUID();
			ulong handle = 123;

             _llClientView.SendRegionHandle(regionID, handle);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendRegionHandle method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendRegionHandle Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Region Handshake Method Test
        /// Documentation   :  
        /// Method Signature:  void SendRegionHandshake(RegionInfo regionInfo, RegionHandshakeArgs args)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendRegionHandshakeTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            // RegionInfo regionInfo = new RegionInfo();
            // RegionHandshakeArgs args = new RegionHandshakeArgs();

            // _llClientView.SendRegionHandshake(regionInfo, args);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendRegionHandshake method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendRegionHandshake Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Region Info To Estate Menu Method Test
        /// Documentation   :  
        /// Method Signature:  void SendRegionInfoToEstateMenu(RegionInfoForEstateMenuArgs args)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendRegionInfoToEstateMenuTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            // RegionInfoForEstateMenuArgs args = new RegionInfoForEstateMenuArgs();

            // _llClientView.SendRegionInfoToEstateMenu(args);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendRegionInfoToEstateMenu method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendRegionInfoToEstateMenu Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Region Teleport Method Test
        /// Documentation   :  
        /// Method Signature:  void SendRegionTeleport(ulong regionHandle, byte simAccess, IPEndPoint newRegionEndPoint, uint locationID, uint flags, string capsURL)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendRegionTeleportTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            ulong regionHandle = 123;
			byte simAccess = 0x2A;
            // IPEndPoint newRegionEndPoint = new IPEndPoint();
			uint locationID = 123;
			uint flags = 123;
			string capsURL = "test";

            // _llClientView.SendRegionTeleport(regionHandle, simAccess, newRegionEndPoint, locationID, flags, capsURL);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendRegionTeleport method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendRegionTeleport Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Remove Inventory Item Method Test
        /// Documentation   :  
        /// Method Signature:  void SendRemoveInventoryItem(UUID itemID)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendRemoveInventoryItemTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID itemID = new UUID();

             _llClientView.SendRemoveInventoryItem(itemID);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendRemoveInventoryItem method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendRemoveInventoryItem Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Script Question Method Test
        /// Documentation   :  This is the entry point for the UDP route by which the client can retrieve asset data. If the request
        /// Method Signature:  void SendScriptQuestion(UUID taskID, string taskName, string ownerName, UUID itemID, int question)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendScriptQuestionTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID taskID = new UUID();
			string taskName = "test";
			string ownerName = "test";
			UUID itemID = new UUID();
			int question = 123;

             _llClientView.SendScriptQuestion(taskID, taskName, ownerName, itemID, question);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendScriptQuestion method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendScriptQuestion Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Script Running Reply Method Test
        /// Documentation   :  
        /// Method Signature:  void SendScriptRunningReply(UUID objectID, UUID itemID, bool running)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendScriptRunningReplyTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID objectID = new UUID();
			UUID itemID = new UUID();
			bool running = true;

             _llClientView.SendScriptRunningReply(objectID, itemID, running);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendScriptRunningReply method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendScriptRunningReply Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Script Teleport Request Method Test
        /// Documentation   :  
        /// Method Signature:  void SendScriptTeleportRequest(string objName, string simName, Vector3 pos, Vector3 lookAt)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendScriptTeleportRequestTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            string objName = "test";
			string simName = "test";
			Vector3 pos = new Vector3();
			Vector3 lookAt = new Vector3();

             _llClientView.SendScriptTeleportRequest(objName, simName, pos, lookAt);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendScriptTeleportRequest method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendScriptTeleportRequest Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Set Follow Cam Properties Method Test
        /// Documentation   :  
        /// Method Signature:  void SendSetFollowCamProperties(UUID objectID, SortedDictionary<int, float> parameters)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendSetFollowCamPropertiesTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID objectID = new UUID();
            // SortedDictionary<int, float> parameters = new SortedDictionary<int, float>();

            // _llClientView.SendSetFollowCamProperties(objectID, parameters);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendSetFollowCamProperties method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendSetFollowCamProperties Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Shutdown Connection Notice Method Test
        /// Documentation   :  
        /// Method Signature:  void SendShutdownConnectionNotice()
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendShutdownConnectionNoticeTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            
             _llClientView.SendShutdownConnectionNotice();
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendShutdownConnectionNotice method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendShutdownConnectionNotice Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Sim Stats Method Test
        /// Documentation   :  
        /// Method Signature:  void SendSimStats(SimStats stats)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendSimStatsTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            // SimStats stats = new SimStats();

            // _llClientView.SendSimStats(stats);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendSimStats method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendSimStats Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Sit Response Method Test
        /// Documentation   :  
        /// Method Signature:  void SendSitResponse(UUID TargetID, Vector3 OffsetPos, Quaternion SitOrientation, bool autopilot, Vector3 CameraAtOffset, Vector3 CameraEyeOffset, bool ForceMouseLook)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendSitResponseTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID TargetID = new UUID();
			Vector3 OffsetPos = new Vector3();
			Quaternion SitOrientation = new Quaternion();
			bool autopilot = true;
			Vector3 CameraAtOffset = new Vector3();
			Vector3 CameraEyeOffset = new Vector3();
			bool ForceMouseLook = true;

             _llClientView.SendSitResponse(TargetID, OffsetPos, SitOrientation, autopilot, CameraAtOffset, CameraEyeOffset, ForceMouseLook);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendSitResponse method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendSitResponse Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Start Ping Check Method Test
        /// Documentation   :  
        /// Method Signature:  void SendStartPingCheck(byte seq)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendStartPingCheckTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            byte seq = 0x2A;

             _llClientView.SendStartPingCheck(seq);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendStartPingCheck method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendStartPingCheck Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Sun Pos Method Test
        /// Documentation   :  
        /// Method Signature:  void SendSunPos(Vector3 Position, Vector3 Velocity, ulong CurrentTime, uint SecondsPerSunCycle, uint SecondsPerYear, float OrbitalPosition)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendSunPosTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            Vector3 Position = new Vector3();
			Vector3 Velocity = new Vector3();
			ulong CurrentTime = 123;
			uint SecondsPerSunCycle = 123;
			uint SecondsPerYear = 123;
			float OrbitalPosition = 2.99999f;

             _llClientView.SendSunPos(Position, Velocity, CurrentTime, SecondsPerSunCycle, SecondsPerYear, OrbitalPosition);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendSunPos method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendSunPos Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Take Controls Method Test
        /// Documentation   :  
        /// Method Signature:  void SendTakeControls(int controls, bool passToAgent, bool TakeControls)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendTakeControlsTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            int controls = 123;
			bool passToAgent = true;
			bool TakeControls = true;

             _llClientView.SendTakeControls(controls, passToAgent, TakeControls);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendTakeControls method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendTakeControls Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Task Inventory Method Test
        /// Documentation   :  
        /// Method Signature:  void SendTaskInventory(UUID taskID, short serial, byte[] fileName)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendTaskInventoryTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID taskID = new UUID();
			short serial = 123;
			byte[] fileName = new byte[20];

             _llClientView.SendTaskInventory(taskID, serial, fileName);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendTaskInventory method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendTaskInventory Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Telehub Info Method Test
        /// Documentation   :  
        /// Method Signature:  void SendTelehubInfo(UUID ObjectID, string ObjectName, Vector3 ObjectPos, Quaternion ObjectRot, List<Vector3> SpawnPoint)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendTelehubInfoTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID ObjectID = new UUID();
			string ObjectName = "test";
			Vector3 ObjectPos = new Vector3();
			Quaternion ObjectRot = new Quaternion();
			List<Vector3> SpawnPoint = new List<Vector3>();

             _llClientView.SendTelehubInfo(ObjectID, ObjectName, ObjectPos, ObjectRot, SpawnPoint);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendTelehubInfo method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendTelehubInfo Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Teleport Failed Method Test
        /// Documentation   :  Inform the client that a teleport attempt has failed
        /// Method Signature:  void SendTeleportFailed(string reason)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendTeleportFailedTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            string reason = "test";

             _llClientView.SendTeleportFailed(reason);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendTeleportFailed method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendTeleportFailed Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Teleport Progress Method Test
        /// Documentation   :  
        /// Method Signature:  void SendTeleportProgress(uint flags, string message)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendTeleportProgressTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            uint flags = 123;
			string message = "test";

             _llClientView.SendTeleportProgress(flags, message);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendTeleportProgress method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendTeleportProgress Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Teleport Start Method Test
        /// Documentation   :  
        /// Method Signature:  void SendTeleportStart(uint flags)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendTeleportStartTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            uint flags = 123;

             _llClientView.SendTeleportStart(flags);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendTeleportStart method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendTeleportStart Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Terminate Friend Method Test
        /// Documentation   :  
        /// Method Signature:  void SendTerminateFriend(UUID exFriendID)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendTerminateFriendTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID exFriendID = new UUID();

             _llClientView.SendTerminateFriend(exFriendID);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendTerminateFriend method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendTerminateFriend Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Texture Method Test
        /// Documentation   :  
        /// Method Signature:  void SendTexture(AssetBase TextureAsset)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendTextureTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            // AssetBase TextureAsset = new AssetBase();

            // _llClientView.SendTexture(TextureAsset);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendTexture method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendTexture Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Triggered Sound Method Test
        /// Documentation   :  
        /// Method Signature:  void SendTriggeredSound(UUID soundID, UUID ownerID, UUID objectID, UUID parentID, ulong handle, Vector3 position, float gain)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendTriggeredSoundTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID soundID = new UUID();
			UUID ownerID = new UUID();
			UUID objectID = new UUID();
			UUID parentID = new UUID();
			ulong handle = 123;
			Vector3 position = new Vector3();
			float gain = 2.99999f;

             _llClientView.SendTriggeredSound(soundID, ownerID, objectID, parentID, handle, position, gain);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendTriggeredSound method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendTriggeredSound Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Use Cached Mute List Method Test
        /// Documentation   :  
        /// Method Signature:  void SendUseCachedMuteList()
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendUseCachedMuteListTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            
             _llClientView.SendUseCachedMuteList();
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendUseCachedMuteList method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendUseCachedMuteList Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send User Info Reply Method Test
        /// Documentation   :  
        /// Method Signature:  void SendUserInfoReply(bool imViaEmail, bool visible, string email)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendUserInfoReplyTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            bool imViaEmail = true;
			bool visible = true;
			string email = "test";

             _llClientView.SendUserInfoReply(imViaEmail, visible, email);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendUserInfoReply method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendUserInfoReply Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Viewer Effect Method Test
        /// Documentation   :  
        /// Method Signature:  void SendViewerEffect(ViewerEffectPacket.EffectBlock[] effectBlocks)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendViewerEffectTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            // ViewerEffectPacket.EffectBlock[1] effectBlocks = new ViewerEffectPacket.EffectBlock[1];

            // _llClientView.SendViewerEffect(effectBlocks);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendViewerEffect method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendViewerEffect Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Viewer Time Method Test
        /// Documentation   :  
        /// Method Signature:  void SendViewerTime(int phase)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendViewerTimeTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            int phase = 123;

             _llClientView.SendViewerTime(phase);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendViewerTime method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendViewerTime Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Wearables Method Test
        /// Documentation   :  
        /// Method Signature:  void SendWearables(AvatarWearable[] wearables, int serial)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendWearablesTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            // AvatarWearable[] wearables = new AvatarWearable[1];
			int serial = 123;

            // _llClientView.SendWearables(wearables, serial);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendWearables method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendWearables Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Wind Data Method Test
        /// Documentation   :  Send the wind matrix to the client
        /// Method Signature:  void SendWindData(Vector2[] windSpeeds)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendWindDataTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            Vector2[] windSpeeds = new Vector2[1];

             _llClientView.SendWindData(windSpeeds);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendWindData method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendWindData Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Xfer Packet Method Test
        /// Documentation   :  
        /// Method Signature:  void SendXferPacket(ulong xferID, uint packet, byte[] data)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendXferPacketTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            ulong xferID = 123;
			uint packet = 123;
			byte[] data = new byte[20];

             _llClientView.SendXferPacket(xferID, packet, data);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendXferPacket method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendXferPacket Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Xfer Request Method Test
        /// Documentation   :  
        /// Method Signature:  void SendXferRequest(ulong XferID, short AssetType, UUID vFileID, byte FilePath, byte[] FileName)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendXferRequestTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            ulong XferID = 123;
			short AssetType = 123;
			UUID vFileID = new UUID();
			byte FilePath = 0x2A;
			byte[] FileName = new byte[20];

             _llClientView.SendXferRequest(XferID, AssetType, vFileID, FilePath, FileName);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendXferRequest method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SendXferRequest Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Set Child Agent Throttle Method Test
        /// Documentation   :  Sets the throttles from values supplied by the client
        /// Method Signature:  void SetChildAgentThrottle(byte[] throttles)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SetChildAgentThrottleTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            byte[] throttles = new byte[20];

             _llClientView.SetChildAgentThrottle(throttles);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SetChildAgentThrottle method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SetChildAgentThrottle Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Set Client Info Method Test
        /// Documentation   :  
        /// Method Signature:  void SetClientInfo(ClientInfo info)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SetClientInfoTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            // ClientInfo info = new ClientInfo();

            // _llClientView.SetClientInfo(info);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SetClientInfo method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SetClientInfo Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Set Client Option Method Test
        /// Documentation   :  
        /// Method Signature:  void SetClientOption(string option, string value)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SetClientOptionTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            string option = "test";
			string value = "test";

             _llClientView.SetClientOption(option, value);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.SetClientOption method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.SetClientOption Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Start Method Test
        /// Documentation   :  
        /// Method Signature:  void Start()
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void StartTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            
             _llClientView.Start();
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.Start method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.Start Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Stop Method Test
        /// Documentation   :  
        /// Method Signature:  void Stop()
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void StopTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            
             _llClientView.Stop();
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.Stop method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.Stop Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Try Get< T> Method Test
        /// Documentation   :  Register an interface on this client, should only be called in the constructor.
        /// Method Signature:  bool TryGet<T>(out T iface)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void TryGetTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
            bool expected = true;
            bool results;

            //Parameters
            object iface = new object();

            results = _llClientView.TryGet<object>(out iface);
            Assert.AreEqual(expected, results, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.TryGet<object> method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.TryGet<object> Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Update Method Test
        /// Documentation   :  
        /// Method Signature:  void Update(ObjectPropertyUpdate update)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void UpdateTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            // ObjectPropertyUpdate update = new ObjectPropertyUpdate();

            // _llClientView.Update(update);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.Update method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.Update Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// X Report Method Test
        /// Documentation   :  
        /// Method Signature:  string XReport(string uptime, string version)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void XReportTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
            string expected = "test";
            string results;

            //Parameters
            string uptime = "test";
			string version = "test";

            results = _llClientView.XReport(uptime, version);
            Assert.AreEqual(expected, results, "OpenSim.Region.ClientStack.LindenUDP.LLClientView.XReport method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.LLClientView.XReport Time Elapsed: {0}", methodDuration.ToString()));
        }


        #endregion

    }
}
