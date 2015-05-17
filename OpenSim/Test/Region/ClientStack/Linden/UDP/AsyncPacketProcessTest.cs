#region Includes
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using OpenSim.Region.ClientStack.LindenUDP;
using OpenMetaverse;
#endregion

///////////////////////////////////////////////////////////////////////////////
// Copyright 2015 (c) by Open Sim All Rights Reserved.
//  
// Project:      Region
// Module:       AsyncPacketProcessTest.cs
// Description:  Tests for the Async Packet Process class in the OpenSim.Region.ClientStack.LindenUDP assembly.
//  
// Date:       Author:           Comments:
// 16.05.2015 21:44  akira     Module created.
///////////////////////////////////////////////////////////////////////////////
namespace OpenSim.Region.ClientStack.LindenUDPTest
{

    /// <summary>
    /// Tests for the Async Packet Process Class
    /// Documentation: 
    /// </summary>
    [TestFixture(Description="Tests for Async Packet Process")]
    public class AsyncPacketProcessTest
    {
        #region Class Variables
        private LLClientView.AsyncPacketProcess _asyncPacketProcess = null;
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
            //New instance of Async Packet Process
            // _asyncPacketProcess = new LLClientView.AsyncPacketProcess(new LLClientView(),new PacketMethod(),new Packet());
        }

        /// <summary>
        /// Code that is run after each test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {
            //TODO:  Put dispose in here for _asyncPacketProcess or delete this line
        }
        #endregion

        #region Property Tests

//No public properties were found. No tests are generated for non-public scoped properties.

        #endregion

        #region Method Tests


        /// <summary>
        /// Async Packet Process Constructor Test
        /// Documentation   :  
        /// Constructor Signature:  AsyncPacketProcess(LLClientView pClientview, PacketMethod pMethod, Packet pPack)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void AsyncPacketProcessConstructorTest()
        {
            System.DateTime constructorStartTime = System.DateTime.Now;
                        
            //Parameters
            // LLClientView pClientview = new LLClientView();
            // PacketMethod pMethod = new PacketMethod();
            // Packet pPack = new Packet();

            // LLClientView.AsyncPacketProcess asyncPacketProcess = new LLClientView.AsyncPacketProcess(pClientview, pMethod, pPack);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.AsyncPacketProcess.AsyncPacketProcessConstructor constructor test failed");

            System.TimeSpan constructorDuration = System.DateTime.Now.Subtract(constructorStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.AsyncPacketProcess.AsyncPacketProcessConstructor Time Elapsed: {0}", constructorDuration.ToString()));
        }

        /// <summary>
        /// Build Event Method Test
        /// Documentation   :  
        /// Method Signature:  OSD BuildEvent(string eventName, OSD eventBody)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void BuildEventTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
            // OSD expected = new OSD();
            // OSD results;

            //Parameters
            string eventName = "test";
            // OSD eventBody = new OSD();

            // results = AsyncPacketProcess.BuildEvent(eventName, eventBody);
            // Assert.AreEqual(expected, results, "OpenSim.Region.ClientStack.LindenUDP.AsyncPacketProcess.BuildEvent method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.AsyncPacketProcess.BuildEvent Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Agent Terse Update Method Test
        /// Documentation   :  
        /// Method Signature:  void SendAgentTerseUpdate(ISceneEntity p)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendAgentTerseUpdateTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            // ISceneEntity p = new ISceneEntity();

            // _asyncPacketProcess.SendAgentTerseUpdate(p);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.AsyncPacketProcess.SendAgentTerseUpdate method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.AsyncPacketProcess.SendAgentTerseUpdate Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Avatar Interests Reply Method Test
        /// Documentation   :  
        /// Method Signature:  void SendAvatarInterestsReply(UUID avatarID, uint wantMask, string wantText, uint skillsMask, string skillsText, string languages)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendAvatarInterestsReplyTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID avatarID = new UUID();
			uint wantMask = 123;
			string wantText = "test";
			uint skillsMask = 123;
			string skillsText = "test";
			string languages = "test";

            // _asyncPacketProcess.SendAvatarInterestsReply(avatarID, wantMask, wantText, skillsMask, skillsText, languages);
            // Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.AsyncPacketProcess.SendAvatarInterestsReply method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.AsyncPacketProcess.SendAvatarInterestsReply Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Bulk Update Inventory Method Test
        /// Documentation   :  
        /// Method Signature:  void SendBulkUpdateInventory(InventoryFolderBase[] folders, InventoryItemBase[] items)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendBulkUpdateInventoryTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            // InventoryFolderBase[] folders = new InventoryFolderBase[1];
            // InventoryItemBase[] items = new InventoryItemBase[1];

            // _asyncPacketProcess.SendBulkUpdateInventory(folders, items);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.AsyncPacketProcess.SendBulkUpdateInventory method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.AsyncPacketProcess.SendBulkUpdateInventory Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Change User Rights Method Test
        /// Documentation   :  
        /// Method Signature:  void SendChangeUserRights(UUID agentID, UUID friendID, int rights)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendChangeUserRightsTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID agentID = new UUID();
			UUID friendID = new UUID();
			int rights = 123;

            //  _asyncPacketProcess.SendChangeUserRights(agentID, friendID, rights);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.AsyncPacketProcess.SendChangeUserRights method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.AsyncPacketProcess.SendChangeUserRights Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Places Reply Method Test
        /// Documentation   :  
        /// Method Signature:  void SendPlacesReply(UUID queryID, UUID transactionID, PlacesReplyData[] data)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendPlacesReplyTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID queryID = new UUID();
			UUID transactionID = new UUID();
            // PlacesReplyData[] data = new PlacesReplyData[1];

            // _asyncPacketProcess.SendPlacesReply(queryID, transactionID, data);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.AsyncPacketProcess.SendPlacesReply method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.AsyncPacketProcess.SendPlacesReply Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Remove Inventory Folders Method Test
        /// Documentation   :  
        /// Method Signature:  void SendRemoveInventoryFolders(UUID[] folders)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendRemoveInventoryFoldersTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID[] folders = new UUID[1];

            // _asyncPacketProcess.SendRemoveInventoryFolders(folders);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.AsyncPacketProcess.SendRemoveInventoryFolders method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.AsyncPacketProcess.SendRemoveInventoryFolders Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Remove Inventory Items Method Test
        /// Documentation   :  
        /// Method Signature:  void SendRemoveInventoryItems(UUID[] items)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendRemoveInventoryItemsTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            UUID[] items = new UUID[1];

            // _asyncPacketProcess.SendRemoveInventoryItems(items);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.AsyncPacketProcess.SendRemoveInventoryItems method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.AsyncPacketProcess.SendRemoveInventoryItems Time Elapsed: {0}", methodDuration.ToString()));
        }

        /// <summary>
        /// Send Text Box Request Method Test
        /// Documentation   :  
        /// Method Signature:  void SendTextBoxRequest(string message, int chatChannel, string objectname, UUID ownerID, string ownerFirstName, string ownerLastName, UUID objectId)
        /// </summary>
        [Test]
        [Ignore("Please Implement")]
        public void SendTextBoxRequestTest()
        {
            System.DateTime methodStartTime = System.DateTime.Now;
                        
            //Parameters
            string message = "test";
			int chatChannel = 123;
			string objectname = "test";
			UUID ownerID = new UUID();
			string ownerFirstName = "test";
			string ownerLastName = "test";
			UUID objectId = new UUID();

            // _asyncPacketProcess.SendTextBoxRequest(message, chatChannel, objectname, ownerID, ownerFirstName, ownerLastName, objectId);
            Assert.AreEqual(String.Empty, String.Empty, "OpenSim.Region.ClientStack.LindenUDP.AsyncPacketProcess.SendTextBoxRequest method test failed");

            System.TimeSpan methodDuration = System.DateTime.Now.Subtract(methodStartTime);
            System.Console.WriteLine(String.Format("OpenSim.Region.ClientStack.LindenUDP.AsyncPacketProcess.SendTextBoxRequest Time Elapsed: {0}", methodDuration.ToString()));
        }


        #endregion

    }
}
