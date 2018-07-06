using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TesRoles.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View("Index","_AdminLTE");
        }
        [Authorize(Roles ="report")]
        public IActionResult Report()
        {
            return View();
        }
        [Authorize(Roles ="revenue")]
        public IActionResult Chart()
        {
            return View();
        }
    }
}