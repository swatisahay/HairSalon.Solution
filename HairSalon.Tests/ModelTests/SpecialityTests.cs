using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class SpecialityTests : IDisposable
  {
    public void Dispose()
    {
      Speciality.DeleteAll();
      Stylist.DeleteAll();
    }
    public SpecialityTests()
    {
        DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=swati_sahay_test;";
    }
    [TestMethod]
    public void Save_GetAllSpecialitys_Test()
    {
      //Arrange
      Speciality newSpeciality = new Speciality("Tom cruise style");
      Speciality newSpeciality1 = new Speciality("Layer cut");
      newSpeciality.Save();
      newSpeciality1.Save();

      //Act
      List<Speciality> expectedResult = new List<Speciality>{newSpeciality, newSpeciality1};
      List<Speciality> result = Speciality.GetAllSpecialitys();

      //Assert
      CollectionAssert.AreEqual(expectedResult, result);
    }
    [TestMethod]
    public void Find_Test()
    {
      //Arrange
      Speciality newSpeciality = new Speciality("layer cut");
      newSpeciality.Save();

      //Act
      Speciality result = Speciality.Find(newSpeciality.GetSpecialityId());

      //Assert
      Assert.AreEqual(newSpeciality, result);
    }
    [TestMethod]
    public void GetStylists_Test()
    {
      //Arrange
      Speciality newSpeciality = new Speciality("Tom cruise style");
      newSpeciality.Save();
      Stylist newStylist = new Stylist("Swati", "senior stylist");
      newStylist.Save();
      Stylist newStylist1 = new Stylist("Rakesh", "senior stylist");
      newStylist1.Save();

      //Act
      newSpeciality.AddStylist(newStylist);
      newSpeciality.AddStylist(newStylist1);

      List<Stylist> expectedResult = new List<Stylist>{newStylist, newStylist1};
      List<Stylist> result = newSpeciality.GetStylists();

      //Assert
      CollectionAssert.AreEqual(expectedResult, result);
    }
    [TestMethod]
    public void Edit_Test()
    {
      //Arrange
      Speciality newSpeciality = new Speciality("layer cut");
      newSpeciality.Save();
      Speciality expectedSpeciality = new Speciality("hair coloring", newSpeciality.GetSpecialityId());
      //Act
      newSpeciality.Edit("hair coloring");

      //Assert
      Assert.AreEqual(expectedSpeciality, newSpeciality);
    }
    [TestMethod]
    public void Delete_Test()
    {
      //Arrange
      Speciality newSpeciality = new Speciality("keratin express");
      newSpeciality.Save();
      Speciality newSpeciality1 = new Speciality("hair perming");
      newSpeciality1.Save();

      //Act
      newSpeciality.Delete();
      List<Speciality> result = Speciality.GetAllSpecialitys();
      List<Speciality> expectedResult = new List<Speciality>{newSpeciality1};
      //Assert
      CollectionAssert.AreEqual(expectedResult, result);
    }
  }
}
