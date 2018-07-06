using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TesRoles.Models;

namespace TesRoles.Controllers
{
    public class HomeController : Controller
    {
        //const string SessionName = "_Name";
        //const string SessionAge = "_Age";
        public IActionResult Index()
        {
            //HttpContext.Session.SetString(SessionName, "Jarvik");
            //HttpContext.Session.SetInt32(SessionAge, 24);
            return View();
        }

        public IActionResult About()
        {
            //ViewBag.Name = HttpContext.Session.GetString(SessionName);
            //ViewBag.Age = HttpContext.Session.GetInt32(SessionAge);
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
