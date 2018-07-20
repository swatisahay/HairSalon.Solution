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
      return RedirectToAction("Index");
    }

    [HttpGet("/client/{id}/update")]
  public ActionResult UpdateForm(int id)
  {
    Client thisClient = Client.Find(id);
    return View(thisClient);
  }
  [HttpPost("/client/{id}/update")]
  public ActionResult Update(int id)
  {
    Client thisClient = Client.Find(id);
    thisClient.Edit(Request.Form["updatename"], int.Parse(Request.Form["updatestylistid"]));
    return RedirectToAction("Index");
  }

  [HttpGet("/client/{id}/delete")]
  public ActionResult Delete(int id)
  {
    Client thisClient = Client.Find(id);
    thisClient.Delete();
    return RedirectToAction("Index");
  }
  [HttpPost("/client/delete")]
   public ActionResult DeleteAll()
   {
    Client.DeleteAll();
     return View();
   }

  }
}
