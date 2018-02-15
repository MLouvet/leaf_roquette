﻿using System;
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
            var collaborateurs = dal.GetCollaborateurs(c.Id);

            var model = new ProjectViewModel
            {
                projets = dal.GetProjetByCollaborateur(collaborateurs).ToList()
            };
            foreach(var projet in model.projets)
            {
                projet.ClientNavigation = dal.GetClient(projet.Client);
                projet.ResponsableNavigation = dal.GetCollaborateurs(projet.Responsable);
            }

            

            return View(model);
        }
    }
}
