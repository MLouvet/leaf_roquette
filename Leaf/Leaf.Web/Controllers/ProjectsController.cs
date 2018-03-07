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
        private static int pId = -1;
        public ProjectsController(LeafContext context)
        {
            Dal.SetBDD(context);
        }

        // GET: /<controller>/
        public IActionResult ProjectList()
        {
            Dal dal = new Dal();
            Collaborateurs c = dal.GetCollaborateurs(HttpContext.User.Identity.Name);

            var model = new ProjectsViewModel
            {
                projets = dal.GetProjets(c).ToList()
            };
            foreach (var projet in model.projets)
            {
                projet.ClientNavigation = dal.GetClient(projet.Client);
                projet.ResponsableNavigation = dal.GetCollaborateurs(projet.Responsable);
            }

            return View(model);
        }

        /// <summary>
        /// Function to prepare the display of a single project
        /// </summary>
        /// <param name="id">the id of the project which will be displayed</param>
        /// <returns>a view with it's view model</returns>
        public IActionResult Project(int? id)
        {
            Dal dal = new Dal();
            Collaborateurs c = dal.GetCollaborateurs(HttpContext.User.Identity.Name);

            Projet projectToDisplay = dal.GetProjet((int)id);
            projectToDisplay.ClientNavigation = dal.GetClient(projectToDisplay.Client);
            projectToDisplay.ResponsableNavigation = dal.GetCollaborateurs(projectToDisplay.Responsable);

            projectToDisplay.Tache = dal.GetTaskByProjects(projectToDisplay.Id, c.Id);

            bool IsProjectManagerTemp = dal.IsProjectManager(HttpContext.User.Identity.Name, projectToDisplay.Id);

            if (id.HasValue)
            {
                pId = (int)id;
                var model = new ProjectViewModel
                {
                    Project = projectToDisplay,
                    IsProjectManager = IsProjectManagerTemp
                };

                return View("Project", model);
            }

            return View("Error");
        }

        /// <summary>
        /// Function called when the creation formular is displayed
        /// </summary>
        /// <returns>A view with the ProjectViewModel associated</returns>
        public IActionResult ProjectCreation()
        {
            Dal dal = new Dal();
            Collaborateurs c = dal.GetCollaborateurs(HttpContext.User.Identity.Name);

            var model = new ProjectViewModel
            {
                Project = new Leaf.DAL.ScaffoldedModels.Projet(),
                IsModification = false,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today,
                _clients = dal.GetClients(c),
                _projectManagerList = dal.getProjectManagers()
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
            Collaborateurs c = dal.GetCollaborateurs(HttpContext.User.Identity.Name);

            //Verification
            if (model.StartDate.CompareTo(model.EndDate) < 0)
            {
                model.ValidationErrorMessage = "La date de début est après la date de fin";
                return View("ProjectCreation", model);
            }

            if (ModelState.IsValid)
            {
                var project = new Leaf.DAL.ScaffoldedModels.Projet { Nom = model.ProjectName, Debut = model.StartDate, Echeance = model.EndDate, Client = model.ProjectClient, Responsable = model.ProjectLeader };

                bool result = dal.SaveNewProject(project);

                if (result)
                {
                    var returnModel = new ProjectsViewModel
                    {
                        projets = dal.GetProjets(c)
                    };
                    foreach (var projet in returnModel.projets)
                    {
                        projet.ClientNavigation = dal.GetClient(projet.Client);
                        projet.ResponsableNavigation = dal.GetCollaborateurs(projet.Responsable);
                    }
                    if(c.Id == project.Responsable)
                    {
                        dal.AddNotification(project.Responsable, project.Id, null, "Votre nouveau projet a bien été créé.", DateTime.Now);
                    }
                    else
                    {
                        if (dal.IsProjectManager(c.Mail, project.Id))
                        {
                            dal.AddNotification(project.Responsable, project.Id, null, "Vous avez été assigné à un nouveau projet par son chef :  " + c.Prenom + " " + c.Nom + ".", DateTime.Now);
                        }
                        else
                        {
                            dal.AddNotification(project.Responsable, project.Id, null, "Vous avez été assigné à un nouveau projet par un administrateur.", DateTime.Now);
                        }
                    }

                    return View("ProjectList", returnModel);
                }
            };

            
            model._clients = dal.GetClients(c);
            model._projectManagerList = dal.getProjectManagers();

            return View("ProjectCreation", model);
        }

        /// <summary>
        /// To prepare the display 
        /// </summary>
        /// <param name="id">The id of the project to modify</param>
        /// <returns>A View containing a ProjectViewModel</returns>
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
                ProjectName = projectToModify.Nom,
                ProjectClient = projectToModify.Client,
                ProjectLeader = projectToModify.Responsable,
                StartDate = projectToModify.Debut,
                EndDate = projectToModify.Echeance,
                _clients = dal.GetClients(c),
                _projectManagerList = dal.getProjectManagers()
            };

            return View(model);
        }


        /// <summary>
        /// Save in the database the modification
        /// </summary>
        /// <param name="model">the model which have the modifications we want</param>
        /// <param name="id">the id of the project whih we want to save the modification</param>
        /// <returns>If the save succeeded, return the list of project, else return to the formular</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult SaveProjectModification(ProjectViewModel model, int? id)
        {
            Dal dal = new Dal();

            if (ModelState.IsValid)
            {
                Projet projectTemp = new Projet();

                projectTemp.Id = (int)id;
                projectTemp.Nom = model.ProjectName;

                projectTemp.Debut = model.StartDate;
                projectTemp.Echeance = model.EndDate;

                projectTemp.Client = model.ProjectClient;
                projectTemp.Responsable = model.ProjectLeader;

                bool result = dal.ModifyProject(projectTemp);

                if (result)
                {
                    Collaborateurs c = dal.GetCollaborateurs(HttpContext.User.Identity.Name);
                    var returnModel = new ProjectsViewModel
                    {
                        projets = dal.GetProjets(c)
                    };
                    foreach (var projet in returnModel.projets)
                    {
                        projet.ClientNavigation = dal.GetClient(projet.Client);
                        projet.ResponsableNavigation = dal.GetCollaborateurs(projet.Responsable);
                    }
                    return View("ProjectList", returnModel);
                }
            }

            return View(model);
        }

        public bool IsProjectManager(int projectId)
        {
            Dal dal = new Dal();
            return dal.IsProjectManager(HttpContext.User.Identity.Name, projectId);
        }

        public JsonResult Data()
        {
            Dal dal = new Dal();
            var tasks = dal.GetTaskByProjects(pId);
            var dataList = new List<Tuple<int, string, DateTime, int, int, float, int?>>();
            var linkDataList = new List<Tuple<int, int, int, string>>();

            //building data list
            foreach (Tache task in tasks)
            {
                dataList.Add(new Tuple<int, string, DateTime, int, int, float, int?>(task.Id, task.Nom,
                    ((DateTime)task.Debut), task.ChargeEstimee, 1,
                    (float)(task.ChargeConsommee) / (float)(task.ChargeConsommee + task.ChargeEstimeeRestante) / 100f, //consommé / (consommé + reste à faire)
                    task.SuperTache
                    ));
            }

            //Building links
            foreach (Tache tache in tasks)
            {
                if (tache.SuperTache.HasValue)
                {
                    linkDataList.Add(new Tuple<int, int, int, string>(linkDataList.Count, tache.Id, (int)tache.SuperTache, "0"));
                }
            }


            var jsonData = new
            {
                // create tasks array
                data = (
                    from t in dataList
                    select new
                    {
                        id = t.Item1,
                        text = t.Item2,
                        start_date = t.Item3.ToString("u"),
                        duration = t.Item4,
                        order = 1,
                        progress = t.Item6, //consommé / (consommé + reste à faire)
                        open = true,
                        parent = t.Item7,
                        type = string.Empty
                    }
                ).ToArray(),
                // create links array
                links = (
                    from l in linkDataList
                    select new
                    {
                        id = l.Item1,
                        source = l.Item2,
                        target = l.Item3,
                        type = l.Item4
                    }
                ).ToArray()
            };

            return new JsonResult(jsonData);
        }

    }
}
