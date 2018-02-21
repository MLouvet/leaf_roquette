using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Leaf.Web.Models;
using Microsoft.AspNetCore.Http;
using Leaf.DAL.ScaffoldedModels;
using Leaf.DAL.Services;
using Leaf.DAL;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Leaf.Web.Controllers
{
    public class ProjectsController : Controller
    {
        public ProjectsController(LeafContext context)
        {
            Dal.SetBDD(context);
        }

        // GET: /<controller>/
        public IActionResult ProjectList()
        {
            //IDal d = new Dal();
            // TODO changer 2 en numéro actuel du collab connecté
            //Collaborateurs c = d.GetCollaborateurs(2);
            //List<DAL.DTO.Projet> p = d.;

            // TODO changer 2 en numéro actuel du collab connecté
            Dal dal = new Dal();
            Collaborateurs c = dal.GetCollaborateurs(HttpContext.User.Identity.Name);

            var model = new ProjectsViewModel
            {
                projets = dal.GetProjets(c).ToList()
            };
            foreach(var projet in model.projets)
            {
                projet.ClientNavigation = dal.GetClient(projet.Client);
                projet.ResponsableNavigation = dal.GetCollaborateurs(projet.Responsable);
            }

            return View(model);
        }

        public IActionResult Project(int? id)
        {
            Dal dal = new Dal();
            Collaborateurs c = dal.GetCollaborateurs(HttpContext.User.Identity.Name);

            Projet projectToDisplay = dal.GetProjet((int)id);
            projectToDisplay.ClientNavigation = dal.GetClient(projectToDisplay.Client);
            projectToDisplay.ResponsableNavigation = dal.GetCollaborateurs(projectToDisplay.Responsable);

            if (id.HasValue)
            {
                var model = new ProjectViewModel
                {
                    Project = projectToDisplay
                };

                return View("Project", model);
            }

            return View("Error");
        }
    }
}
