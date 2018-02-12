using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Leaf.Web.Models;
using Microsoft.AspNetCore.Http;
using Leaf.DAL.ScaffoldedModels;
using Leaf.DAL.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Leaf.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private static ProjetService _projetService;
        private static CollaborateursService _collaborateursService;
        private static ClientService _clientService;

        public ProjectsController(LeafContext context)
        {
            Dal.SetBDD(context);
            _projetService = new ProjetService(context);
            _collaborateursService = new CollaborateursService(context);
            _clientService = new ClientService(context);
        }

        // GET: /<controller>/
        public IActionResult ProjectList()
        {
            //IDal d = new Dal();
            // TODO changer 2 en numéro actuel du collab connecté
            //Collaborateurs c = d.GetCollaborateurs(2);
            //List<DAL.DTO.Projet> p = d.;

            // TODO changer 2 en numéro actuel du collab connecté
            Dal d = new Dal();
            Collaborateurs c = d.GetCollaborateurs(HttpContext.User.Identity.Name);
            var collaborateurs = _collaborateursService.GetById(c.Id);

            var model = new ProjectViewModel
            {
                projets = _projetService.GetByCollaborateur(collaborateurs)
            };
            foreach(var projet in model.projets)
            {
                projet.ClientNavigation = _clientService.GetById(projet.Client);
                projet.ResponsableNavigation = _collaborateursService.GetById(projet.Responsable);
            }

            

            return View(model);
        }
    }
}
