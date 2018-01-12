using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Leaf.DAL.ScaffoldedModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Leaf.Web.Controllers
{
    public class HomeController : Controller
    {
        private LeafContext bdd;

        public HomeController(LeafContext context)
        {
            bdd = context;

        }

        // GET: Clients
        public IActionResult Index()
        {
            //IDal d = new Dal();
            Collaborateurs c = null;
            HomeViewModel model = new HomeViewModel
            {
                notifications = new List<Notification>(),
                taches = new List<Tache>()
            };

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
            return View();
        }
    }
}