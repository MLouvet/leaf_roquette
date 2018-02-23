using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Leaf.Web.Models;
using Microsoft.AspNetCore.Http;
using Leaf.DAL.ScaffoldedModels;
using Microsoft.AspNetCore.Authorization;
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

            bool IsProjectManagerTemp = dal.IsProjectManager(HttpContext.User.Identity.Name, projectToDisplay.Id);

            if (id.HasValue)
            {
                var model = new ProjectViewModel
                {
                    Project = projectToDisplay,
                    IsProjectManager = IsProjectManagerTemp
                };

                return View("Project", model);
            }

            return View("Error");
        }

        //Function call when the creation formular is displayed
        public IActionResult ProjectCreation()
        {
            Dal dal = new Dal();
            Collaborateurs c = dal.GetCollaborateurs(HttpContext.User.Identity.Name);

            var model = new ProjectViewModel
            {
                Project = new Leaf.DAL.ScaffoldedModels.Projet(),
                IsModification = false,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today
            };

            return View(model);
        }

        //To save a new project in DB
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult SaveNewProject(ProjectViewModel model)
        {
            Dal dal = new Dal();
            if(ModelState.IsValid)
            {
                var project = new Leaf.DAL.ScaffoldedModels.Projet { Nom = model.ProjectName, Debut = model.StartDate, Echeance = model.EndDate, Client = model.ProjectClient.Id, Responsable = model.ProjectLeader.Id };

                bool result = dal.SaveNewProject(project);

                if(result)
                {
                    Collaborateurs c = dal.GetCollaborateurs(HttpContext.User.Identity.Name);
                    var returnModel = new ProjectsViewModel
                    {
                        projets = dal.GetProjets(c)
                    };
                    return View("ProjectList", returnModel);
                }
            };

            return View("ProjectCreation", model);
        }

        public IActionResult ProjectModification(int? id)
        {
            Dal dal = new Dal();
            Collaborateurs c = dal.GetCollaborateurs(HttpContext.User.Identity.Name);
            Leaf.DAL.ScaffoldedModels.Projet projectToModify = dal.GetProjet((int)id);
            projectToModify.ClientNavigation = dal.GetClient(projectToModify.Client);
            projectToModify.ResponsableNavigation = dal.GetCollaborateurs(projectToModify.Responsable);

            var model = new ProjectViewModel
            {
                Project = projectToModify,
                ProjectClient = projectToModify.ClientNavigation,
                ProjectLeader = projectToModify.ResponsableNavigation,
                StartDate = projectToModify.Debut,
                EndDate = projectToModify.Echeance
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult SaveProjectModification(ProjectViewModel model, int? id)
        {
            Dal dal = new Dal();

            if(ModelState.IsValid)
            {
                Projet projectTemp = new Projet();

                projectTemp.Id = (int) id;
                projectTemp.Nom = model.ProjectName;

                projectTemp.Debut = model.StartDate;
                projectTemp.Echeance = model.EndDate;

                projectTemp.Client = model.ProjectClient.Id;
                projectTemp.Responsable = model.ProjectLeader.Id;

                bool result = dal.ModifyProject(projectTemp);

                if(result)
                {
                    Collaborateurs c = dal.GetCollaborateurs(HttpContext.User.Identity.Name);
                    var returnModel = new ProjectsViewModel
                    {
                        projets = dal.GetProjets(c)
                    };
                    return View("Project", returnModel);
                }
            }

            return View(model);
        }

        public bool IsProjectManager(int projectId)
        {
            Dal dal = new Dal();
            return dal.IsProjectManager(HttpContext.User.Identity.Name, projectId);
        }
    }
}
