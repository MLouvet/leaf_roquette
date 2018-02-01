using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Leaf.DAL.ScaffoldedModels;
using Leaf.DAL.Services;
using Leaf.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Leaf.Web.Controllers
{
    public class HomeController : Controller
    {
        private static NotificationService _notificationService;
        private static CollaborateursService _collaborateursService;
        private static TacheService _tacheService;

        public HomeController(LeafContext context)
        {
            Dal.SetBDD(context);
            _notificationService = new NotificationService(context);
            _collaborateursService = new CollaborateursService(context);
            _tacheService = new TacheService(context);
        }

        // GET: Clients
        public IActionResult Index()
        {
            //IDal d = new Dal();
            //Collaborateurs c = d.GetCollaborateurs(2);
            //HomeViewModel model = new HomeViewModel
            //{
            //    notifications = d.GetNotifications(c).ToList(),
            //    taches = d.GetTaches(c)
            //};

            //foreach (Tache t in model.taches)
            //{
            //    t.IdProjNavigation = d.GetProjet(t.IdProj);
            //}
            var collaborateurs = _collaborateursService.GetById(2);
            var model = new HomeViewModel
            {
                notifications = _notificationService.GetNotificationsByCollaborateurs(collaborateurs),
                taches = _tacheService.GetByCollaborateurs(collaborateurs)
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