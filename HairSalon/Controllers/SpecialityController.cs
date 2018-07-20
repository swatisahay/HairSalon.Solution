using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Controllers
{
  public class SpecialityController : Controller
  {

    [HttpGet("/Specialitys")]
    public ActionResult Index()
    {
        List<Speciality> allSpeciality = Speciality.GetAllSpecialitys();
        return View(allSpeciality);
    }

    [HttpGet("/Specialitys/new")]
    public ActionResult CreateForm()
    {
      return View();
    }
    [HttpPost("/Specialitys")]
    public ActionResult Create()
    {
      Speciality newSpeciality = new Speciality(Request.Form["newSpeciality"]);
      newSpeciality.Save();
      return RedirectToAction("Success", "Home");
    }
    [HttpGet("/Specialitys/{id}/update")]
    public ActionResult UpdateForm(int id)
    {
      Speciality thisSpeciality = Speciality.Find(id);
      return View(thisSpeciality);
    }
    [HttpPost("/Specialitys/{id}/update")]
    public ActionResult Update(int id)
    {
      Speciality thisSpeciality = Speciality.Find(id);
      thisSpeciality.Edit(Request.Form["updateSpeciality"]);
      return RedirectToAction("Index");
    }

    [HttpGet("/Specialitys/{id}/delete")]
    public ActionResult Delete(int id)
    {
      Speciality thisSpeciality = Speciality.Find(id);
      thisSpeciality.Delete();
      return RedirectToAction("Index");
    }
    // [HttpPost("/Specialitys/delete")]
    // public ActionResult DeleteAll()
    // {
    //   Speciality.ClearAll();
    //   return View();
    // }
    [HttpGet("/Specialitys/{id}")]
    public ActionResult Details(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Speciality selectedSpeciality = Speciality.Find(id);
      List<Stylist> SpecialityStylists = selectedSpeciality.GetStylists();
      List<Stylist> allStylists = Stylist.GetAll();
      model.Add("selectedSpeciality", selectedSpeciality);
      model.Add("SpecialityStylists", SpecialityStylists);
      model.Add("allStylists", allStylists);
      return View(model);
    }
    [HttpPost("/Specialitys/{SpecialityId}/stylists/new")]
    public ActionResult AddStylist(int SpecialityId)
    {
      Speciality Speciality= Speciality.Find(SpecialityId);
      Stylist stylist = Stylist.Find(int.Parse(Request.Form["stylistid"]));
      Speciality.AddStylist(stylist);
      return RedirectToAction("Details",  new { id = SpecialityId });
    }
  }
}
