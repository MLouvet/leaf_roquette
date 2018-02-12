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
    public class ProfileController : Controller
    {

        public ProfileController(LeafContext context)
        {
            Dal.SetBDD(context);
        }

        // GET: Clients
        public IActionResult Profile(int id)
        {
            IDal d = new Dal();
            // TODO changer 2 en numéro actuel du collab connecté
            Collaborateurs c = d.GetCollaborateurs(id);
            ProfileViewModel model = new ProfileViewModel
            {
                notifications = d.GetNotifications(c),
                taches = d.GetTaches(c)
            };

            foreach (Tache t in model.taches)
            {
                t.IdProjNavigation = d.GetProjet(t.IdProj);
            }

            /*foreach (Notification n in model.notifications)
            {
                if (n.IdProjet.HasValue)
                    n.ProjetNavigation = d.GetProjet(n.IdProjet.Value);
                if (n.IdTache.HasValue)
                    n.TacheNavigation = d.GetTache(n.IdTache.Value);
            }*/

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