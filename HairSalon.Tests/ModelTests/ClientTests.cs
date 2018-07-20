using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class ClientTests : IDisposable
  {
    public void Dispose()
    {
      Client.DeleteAll();
    }
    public ClientTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=swati_sahay_test;";
    }

    [TestMethod]
    public void GetAll_ClientEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Client.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }
    [TestMethod]
    public void Save_SavesToDatabase_ClientList()
    {
      //Arrange
      Client testClient = new Client("swati", 1);

      //Act
      testClient.Save();
      List<Client> result = Client.GetAll();
      List<Client> testList = new List<Client>{testClient};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }
    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      Client testClient = new Client("swati", 1);

      //Act
      testClient.Save();
      Client savedClient = Client.GetAll()[0];

      int result = savedClient.GetId();
      int testId = testClient.GetId();

      //Assert
      Assert.AreEqual(testId, result);

    }
    [TestMethod]
   public void Find_FindsClientbyIdInDatabase_Item()
   {
     //Arranges
     Client testClient = new Client("tom", 1);
     testClient.Save();

     //Act
     Client foundItem = Client.Find(testClient.GetId());

     //Assert
     Assert.AreEqual(testClient, foundItem);
   }
   [TestMethod]
  public void Edit_Test()
  {
    //Arrange
    Client newClient = new Client("George",2);
    newClient.Save();
    Client expectedClient = new Client("Martin",2, newClient.GetId());
    //Act
    newClient.Edit("Martin", 1);

    //Assert
    Assert.AreEqual(expectedClient, newClient);
  }
  [TestMethod]
   public void Delete_Test()
   {
     //Arrange
     Client testClient = new Client("swati",2);
     testClient.Save();
     Client newTestClient = new Client("rakesh",3);
     newTestClient.Save();
     List<Client> beforeDeleteList = new List<Client>{testClient, newTestClient};
     List<Client> afterDeleteList = new List<Client>{newTestClient};

     //Act
     Client.Find(testClient.GetId()).Delete();
     List<Client> result = Client.GetAll();
     //Assert
     CollectionAssert.AreEqual(afterDeleteList, result);
   }
  }
}
