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
      return View("Index", allStylist);
    }
    // [HttpGet("/items/find")]
    // public ActionResult Find()
    // {
    //   return View();
    // }
    // [HttpPost("/items/found")]
    // public ActionResult Found()
    // {
    //   Item newItem = new Item("","",0, 0);
    //
    //   newItem = Item.Find(int.Parse(Request.Form["newid"]));
    //
    //   return View(newItem);
    // }
    // [HttpGet("/items/{id}/update")]
    //    public ActionResult UpdateForm(int id)
    //    {
    //        Item thisItem = Item.Find(id);
    //        return View(thisItem);
    //    }
       [HttpGet("/stylist/{id}/clients")]
        public ActionResult Clientdetail(int id)
        {
            Stylist thisStylist = Stylist.Find(id);
            List<Client> allClients = thisStylist.GetClients();
            return View(thisStylist);
        }
}
}
