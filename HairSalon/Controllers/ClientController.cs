using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Controllers
{
  public class ClientController : Controller
  {

    [HttpGet("/client")]
    public ActionResult Index()
    {
        List<Client> allClients = Client.GetAll();
        return View(allClients);
    }

    [HttpGet("/client/new")]
    public ActionResult CreateForm()
    {
        return View();
    }
    [HttpPost("/client")]
    public ActionResult Create()
    {
      Client newClient = new Client (Request.Form["newname"], int.Parse(Request.Form["stylistid"]));
      // Item newItem = new Item ("Charizard", "Fire", 6);
      newClient.Save();
      List<Client> allClients = Client.GetAll();
      return View("Index",allClients);
    }

       //  [HttpGet("/items/{id}/delete")]
       // public ActionResult Delete(int id)
       // {
       //     Item thisItem = Item.Find(id);
       //     thisItem.Delete();
       //     return RedirectToAction("Index");
       // }



    // [HttpPost("/items/delete")]
    // public ActionResult DeleteAll()
    // {
    //     Item.ClearAll();
    //     return View();
    // }
    
  }
}
