using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Leaf.Models;
using Leaf.ScaffoldedModels;
using System.Collections.Generic;

namespace Leaf.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //    IDal d = new Dal();
            //    Collaborateurs c = null;
            HomeViewModel model = new HomeViewModel();
            model.notifications = new List<Notification>();
            model.taches = new List<Tache>();

            return View(model);
        }

        public IActionResult About()
        {
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
