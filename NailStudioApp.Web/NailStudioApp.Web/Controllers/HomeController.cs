using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
//using NailStudioApp.Web.ViewModels;

namespace NailStudioApp.Web.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Home Page";
            ViewData["Message"] = "Welcome to the Nail Studio Web App";

            return View();
        }

    }
}
