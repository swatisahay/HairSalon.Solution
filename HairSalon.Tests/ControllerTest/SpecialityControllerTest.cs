using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class SpecialityControllerTest
  {
    [TestMethod]
    public void Index_ReturnsCorrectView_True()
    {
      //Arrange
      SpecialityController controller = new SpecialityController();

      //Act
      ActionResult indexView = controller.Index();

      //Assert
      Assert.IsInstanceOfType(indexView, typeof(ViewResult));
    }
    [TestMethod]
    public void Index_HasCorrectModelType_SpecialityList()
    {
      //Arrange
      SpecialityController controller = new SpecialityController();
      IActionResult actionResult = controller.Index();
      ViewResult indexView = controller.Index() as ViewResult;

      //Act
      var result = indexView.ViewData.Model;

      //Assert
      Assert.IsInstanceOfType(result, typeof(List<Speciality>));
    }
    [TestMethod]
    public void Details_ReturnsCorrectView_True()
    {
      //Arrange
      SpecialityController controller = new SpecialityController();

      //Act
      ActionResult detailView = controller.Detail(1);

      //Assert
      Assert.IsInstanceOfType(detailView, typeof(ViewResult));
    }
    [TestMethod]
    public void Details_HasCorrectModelType_SpecialityList()
    {
      //Arrange
      SpecialityController controller = new SpecialityController();
      IActionResult actionResult = controller.Detail(1);
      ViewResult detailView = controller.Detail(1) as ViewResult;

      //Act
      var result = detailView.ViewData.Model;

      //Assert
      Assert.IsInstanceOfType(result, typeof(Dictionary<string, object>));
    }
    [TestMethod]
    public void CreateForm_ReturnsCorrectView_True()
    {
      //Arrange
      SpecialityController controller = new SpecialityController();

      //Act
      ActionResult createView = controller.CreateForm();

      //Assert
      Assert.IsInstanceOfType(createView, typeof(ViewResult));
    }

    [TestMethod]
    public void UpdateForm_ReturnsCorrectView_True()
    {
      //Arrange
      SpecialityController controller = new SpecialityController();

      //Act
      ActionResult updateView = controller.UpdateForm(1);

      //Assert
      Assert.IsInstanceOfType(updateView, typeof(ViewResult));
    }
  }
}
