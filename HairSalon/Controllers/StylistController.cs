using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System;


namespace HairSalon.Controllers
{
  public class StylistController : Controller
  {

    [HttpGet("/stylist")]
    public ActionResult Index()
    {
      List<Stylist> allStylist = Stylist.GetAll();
      return View(allStylist);
    }

    [HttpGet("/stylist/new")]
    public ActionResult CreateStylistForm()
    {
      return View();
    }
    [HttpPost("/stylist")]
    public ActionResult Create()
    {
      Stylist newStylist = new Stylist (Request.Form["name"], Request.Form["detail"]);
      // Item newItem = new Item ("Charizard", "Fire", 6);
      newStylist.Save();
      List<Stylist> allStylist = Stylist.GetAll();
      return RedirectToAction("Index");
    }
    [HttpGet("/stylist/find")]
    public ActionResult Find()
    {
      return View();
    }
    [HttpPost("/stylist/found")]
    public ActionResult Found()
    {
      Stylist newStylist = new Stylist("","");

      newStylist = Stylist.Find(int.Parse(Request.Form["newid"]));

      return View(newStylist);
     }
     [HttpGet("/stylist/{id}/update")]
     public ActionResult UpdateForm(int id)
     {
       Stylist thisStylist = Stylist.Find(id);
       return View(thisStylist);
     }
     [HttpPost("/stylist/{id}/update")]
     public ActionResult Update(int id)
     {
       Stylist thisStylist = Stylist.Find(id);
       thisStylist.Edit(Request.Form["updatename"]);
       return RedirectToAction("Index");
     }

     [HttpGet("/stylist/{id}/delete")]
     public ActionResult Delete(int id)
     {
       Stylist thisStylist = Stylist.Find(id);
       thisStylist.Delete();
       return RedirectToAction("Index");
     }
      [HttpGet("/stylist/{id}/clients")]
      public ActionResult Clientdetail(int id)
      {
        Stylist thisStylist = Stylist.Find(id);
        List<Client> allClients = thisStylist.GetClients();
        return View(thisStylist);
      }
    }
  }
