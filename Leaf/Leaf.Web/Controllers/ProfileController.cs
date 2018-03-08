using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Leaf.DAL.ScaffoldedModels;
using Leaf.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Leaf.DAL.Services;
using Leaf.DAL;

namespace Leaf.Web.Controllers
{
    public class ProfileController : Controller
    {
        public ProfileController(LeafContext context)
        {
            Dal.SetBDD(context);
        }

        // GET: Clients
        public IActionResult Profile()
        {
            IDal dal = new Dal();
            // TODO changer 2 en numéro actuel du collab connecté
            Collaborateurs c = dal.GetCollaborateurs(HttpContext.User.Identity.Name);
            ProfileViewModel model = new ProfileViewModel
            {
                notifications = dal.GetNotifications(c).ToList(),
                taches = dal.GetTaches(c).ToList(),
                projet = dal.GetProjets(c).ToList()
            };

            foreach (Tache t in model.taches)
            {
                t.IdProjNavigation = dal.GetProjet(t.IdProj);
            }

            foreach (Notification n in model.notifications)
            {
                if (n.IdProjet != null)
                    n.IdProjetNavigation = dal.GetProjet((int) n.IdProjet);
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

        /// <summary>
        /// Switch vue to nonvu and reverse
        /// </summary>
        /// <param name="notifTemp"></param>
        /// <returns></returns>
        public IActionResult SwitchVue(int? notifTemp)
        {
            Dal dal = new Dal();
            Notification notif = dal.GetNotification((int) notifTemp);
            if (notif.Lue) dal.UnreadNotification(notif);
            else dal.ReadNotification(notif);

            Collaborateurs c = dal.GetCollaborateurs(HttpContext.User.Identity.Name);
            ProfileViewModel model = new ProfileViewModel
            {
                notifications = dal.GetNotifications(c).ToList(),
                taches = dal.GetTaches(c).ToList(),
                projet = dal.GetProjets(c).ToList()
            };

            foreach (Tache t in model.taches)
            {
                t.IdProjNavigation = dal.GetProjet(t.IdProj);
            }

            foreach (Notification n in model.notifications)
            {
                if (n.IdProjet != null)
                    n.IdProjetNavigation = dal.GetProjet((int)n.IdProjet);
            }
            return View("Profile", model);
        }

        /// <summary>
        /// Delete a notification from the list and from the BDD
        /// </summary>
        /// <param name="notifTemp">The id of the notif to delete</param>
        /// <returns>the new view to diplay</returns>
        public IActionResult DeleteNotif(int? notifTemp)
        {
            Dal dal = new Dal();
            if(notifTemp != null)
                dal.DeleteNotification(dal.GetCollaborateurs(HttpContext.User.Identity.Name), dal.GetNotification((int)notifTemp));

            Collaborateurs c = dal.GetCollaborateurs(HttpContext.User.Identity.Name);
            ProfileViewModel model = new ProfileViewModel
            {
                notifications = dal.GetNotifications(c).ToList(),
                taches = dal.GetTaches(c).ToList(),
                projet = dal.GetProjets(c).ToList()
            };

            foreach (Tache t in model.taches)
            {
                t.IdProjNavigation = dal.GetProjet(t.IdProj);
            }

            foreach (Notification n in model.notifications)
            {
                if (n.IdProjet != null)
                    n.IdProjetNavigation = dal.GetProjet((int)n.IdProjet);
            }
            return View("Profile", model);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}