using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTests : IDisposable
  {
    public void Dispose()
    {
      Stylist.DeleteAll();
      Client.DeleteAll();
    }
    public StylistTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=swati_sahay_test;";
    }

    [TestMethod]
    public void GetAll_StylistEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Stylist.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }
    [TestMethod]
    public void Save_SavesToDatabase_StylistList()
    {
      //Arrange
      Stylist testStylist = new Stylist("swati", "senior stylist");

      //Act
      testStylist.Save();
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      Stylist testStylist = new Stylist("swati", "senior stylist");

      //Act
      testStylist.Save();
      Stylist savedStylist = Stylist.GetAll()[0];

      int result = savedStylist.GetId();
      int testId = testStylist.GetId();

      //Assert
      Assert.AreEqual(testId, result);

    }
    [TestMethod]
   public void Find_FindsStylistbyIdInDatabase_Item()
   {
     //Arranges
     Stylist testStylist = new Stylist("tom", "junior stylist");
     testStylist.Save();

     //Act
     Stylist foundItem = Stylist.Find(testStylist.GetId());

     //Assert
     Assert.AreEqual(testStylist, foundItem);
   }
   [TestMethod]
   public void GetClients_RetrievesAllClientsWithstylistId_ClientList()
   {
     Stylist testStylist = new Stylist("Tom","senior stylist");
     testStylist.Save();

     Client firstClient = new Client("Electrode",  testStylist.GetId());
     firstClient.Save();
     Client secondClient = new Client("Electrode", testStylist.GetId());
     secondClient.Save();

     List<Client> testClientList = new List<Client> {firstClient, secondClient};
     List<Client> resultClientList = testStylist.GetClients();

     CollectionAssert.AreEqual(testClientList, resultClientList);
   }
   [TestMethod]
  public void Edit_Test()
  {
    //Arrange
    Stylist newStylist = new Stylist("George", "senior stylist");
    newStylist.Save();
    Stylist expectedStylist = new Stylist("Martin","senior stylist", newStylist.GetId());
    //Act
    newStylist.Edit("Martin");

    //Assert
    Assert.AreEqual(expectedStylist, newStylist);
  }
  [TestMethod]
   public void Delete_Test()
   {
     //Arrange
     Stylist testStylist = new Stylist("Brunet", "senior stylist");
     testStylist.Save();
     Stylist newTestStylist = new Stylist("Nate", "junior stylist");
     newTestStylist.Save();
     List<Stylist> beforeDeleteList = new List<Stylist>{testStylist, newTestStylist};
     List<Stylist> afterDeleteList = new List<Stylist>{newTestStylist}; //Use to make test pass

     //Act
     Stylist.Find(testStylist.GetId()).Delete();
     List<Stylist> result = Stylist.GetAll();
     //Assert
     CollectionAssert.AreEqual(afterDeleteList, result);
   }
  }
}
