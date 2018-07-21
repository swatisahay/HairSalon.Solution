using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Controllers
{
  public class SpecialityController : Controller
  {

    [HttpGet("/specialitys")]
    public ActionResult Index()
    {
        List<Speciality> allSpeciality = Speciality.GetAllSpecialitys();
        return View(allSpeciality);
    }

    [HttpGet("/specialitys/new")]
    public ActionResult CreateForm()
    {
      return View();
    }
    [HttpPost("/specialitys")]
    public ActionResult Create()
    {
      Speciality newSpeciality = new Speciality(Request.Form["newspeciality"]);
      newSpeciality.Save();
      return RedirectToAction("Success", "Home");
    }
    [HttpGet("/specialitys/{id}/update")]
    public ActionResult UpdateForm(int id)
    {
      Speciality thisSpeciality = Speciality.Find(id);
      return View(thisSpeciality);
    }
    [HttpPost("/specialitys/{id}/update")]
    public ActionResult Update(int id)
    {
      Speciality thisSpeciality = Speciality.Find(id);
      thisSpeciality.Edit(Request.Form["updatespeciality"]);
      return RedirectToAction("Index");
    }

    [HttpGet("/specialitys/{id}/delete")]
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
    [HttpGet("/specialitys/{id}")]
    public ActionResult Detail(int id)
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
    [HttpPost("/specialitys/{SpecialityId}/stylists/new")]
    public ActionResult AddStylist(int SpecialityId)
    {
      Speciality Speciality= Speciality.Find(SpecialityId);
      Stylist stylist = Stylist.Find(int.Parse(Request.Form["stylistid"]));
      Speciality.AddStylist(stylist);
      return RedirectToAction("Detail",  new { id = SpecialityId });
    }
  }
}
