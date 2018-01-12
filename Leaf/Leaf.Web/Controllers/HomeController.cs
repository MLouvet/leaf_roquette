using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Leaf.DAL.ScaffoldedModels;
using Leaf.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Leaf.Web.Controllers
{
    public class HomeController : Controller
    {

        public HomeController(LeafContext context)
        {
            Dal.SetBDD(context);
        }

        // GET: Clients
        public IActionResult Index()
        {
            IDal d = new Dal();
            Collaborateurs c = d.GetCollaborateurs(2);
            HomeViewModel model = new HomeViewModel
            {
                notifications = c.Notification.ToList(),
                taches = d.GetTaches(c)
            };

            foreach (Tache t in model.taches)
            {
                t.IdProjNavigation = d.GetProjet(t.IdProj);
            }

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