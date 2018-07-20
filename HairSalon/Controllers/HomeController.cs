using Microsoft.AspNetCore.Mvc;

namespace HairSalon.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet("/Success")]
        public ActionResult Success()
        {
            return View();
        }
        [HttpGet("/employer")]
        public ActionResult Employer()
        {
          return View();
          // return new EmptyResult();
        }
    }
}
