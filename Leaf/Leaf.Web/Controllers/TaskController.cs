using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Leaf.DAL;
using Leaf.DAL.ScaffoldedModels;
using Microsoft.AspNetCore.Authorization;
using Leaf.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Leaf.Web.Controllers
{
    public class TaskController : Controller
    {
        public TaskController(LeafContext context)
        {
            Dal.SetBDD(context);
        }

        /// <summary>
        /// Preparation to display a task
        /// </summary>
        /// <param name="id">Id of the task to display</param>
        /// <returns>the task view model</returns>
        public IActionResult Task(int? id)
        {
            if(id.HasValue)
            {
                Dal dal = new Dal();
                Tache taskTemp = dal.GetTache((int)id);
                Collaborateurs c = dal.GetCollaborateurs(HttpContext.User.Identity.Name);

                Projet projTemp = dal.GetProjet(taskTemp.IdProj);
                projTemp.ClientNavigation = dal.GetClient(projTemp.Client);

                taskTemp.IdProjNavigation = projTemp;
                taskTemp.Collab = dal.GetCollaborateurs(taskTemp.CollabId);

                var model = new TaskViewModel
                {
                    Task = taskTemp,
                    IsProjectManager = (projTemp.Responsable == c.Id),
                    IsTaskResponsible = (taskTemp.CollabId == c.Id)
                };
                return View(model);
            }
            return View("Error");
        }

        /// <summary>
        /// Preparation to display a list of tasks
        /// </summary>
        /// <returns>a taskListViewModel for display</returns>
        public IActionResult TaskList()
        {
            Dal dal = new Dal();
            Collaborateurs c = dal.GetCollaborateurs(HttpContext.User.Identity.Name);
            var collaborateurs = dal.GetCollaborateurs(c.Id);

            var model = new TaskListViewModel
            {
                TaskList = dal.GetTaches(collaborateurs)
            };

            return View(model);
        }

        /// <summary>
        /// Prepare task creation
        /// </summary>
        /// <returns>a viewModel for a new task </returns>
        public IActionResult TaskCreation(int? id)
        {
            Dal dal = new Dal();
            Collaborateurs c = dal.GetCollaborateurs(HttpContext.User.Identity.Name);
            var collaborateurs = dal.GetCollaborateurs(c.Id);

            List<Collaborateurs> listCollaborator = dal.AllCollaborateurs;
            List<Tache> listEligiblePreviousTasks = dal.GetPotentialPreviousTasks((int)id, new List<int>(), -1);
            List<Tache> potentialSuperTask = dal.GetPotentialSuperTache((int)id, new List<int>());

            var model = new TaskViewModel
            {
                ProjectId = (int) id,
                TaskName = "",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                _collaboratorList = listCollaborator,
                _EligiblePreviousTasks = listEligiblePreviousTasks,
                _superTaskList = potentialSuperTask,
            };

            return View("TaskCreation", model);
        }

        /// <summary>
        /// prepare data for task modification
        /// </summary>
        /// <param name="id">The id of the task to modify</param>
        /// <returns>the view to display</returns>
        public IActionResult TaskModification(int? id)
        {
            Dal dal = new Dal();
            Collaborateurs c = dal.GetCollaborateurs(HttpContext.User.Identity.Name);
            var collaborateurs = dal.GetCollaborateurs(c.Id);

            List<Collaborateurs> listCollaborator = dal.AllCollaborateurs;
            List<Tache> listEligiblePreviousTasks = dal.GetPotentialPreviousTasks((int)id, new List<int>(), -1);

            List<Tache> potentialSuperTask = dal.GetPotentialSuperTache((int)id, new List<int>());

            Tache taskTemp = dal.GetTache((int)id);

            var model = new TaskViewModel
            {
                Task = taskTemp,
                TaskName = taskTemp.Nom,
                TaskDescription = taskTemp.Description,
                StartDate = (System.DateTime) taskTemp.Debut,
                EndDate = (System.DateTime)taskTemp.Fin,
                ChargeEstimee = taskTemp.ChargeEstimee,
                Progres = taskTemp.Progres,
                IdProj = taskTemp.IdProj,
                CollabId = taskTemp.CollabId,
                SuperTache = (int) taskTemp.SuperTache,
                _superTaskList = potentialSuperTask,
                _collaboratorList = listCollaborator,
                _EligiblePreviousTasks = listEligiblePreviousTasks
            };

            return View("TaskModification", model);
        }

        /// <summary>
        /// Save the task from the model in the database
        /// </summary>
        /// <param name="model">the model containing the datas to be saved</param>
        /// <returns>the view to be displayed, depending if the save failed or not</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult SaveNewTask(TaskViewModel model, int? projectId)
        {
            Dal dal = new Dal();

            string validationMesg = dal.VerifyNewTask((int)projectId);

            Collaborateurs c = dal.GetCollaborateurs(HttpContext.User.Identity.Name);

            List<Collaborateurs> listCollaborator = dal.AllCollaborateurs;
            List<Tache> listEligiblePreviousTasks = dal.GetPotentialPreviousTasks((int)projectId, new List<int>(), (model.SuperTache == null ? -1 : model.SuperTache));

            model._collaboratorList = listCollaborator;
            model._EligiblePreviousTasks = listEligiblePreviousTasks;

            //Verification
            if (! (validationMesg == ""))
            {
                model.ValidationErrorMessage = validationMesg;
                model._collaboratorList = listCollaborator;
                model._EligiblePreviousTasks = listEligiblePreviousTasks;
                return View("TaskCreation", model);
            }

            if(ModelState.IsValid)
            {
                Tache newTask = new Leaf.DAL.ScaffoldedModels.Tache
                {
                    Nom = model.TaskName,
                    Description = model.TaskDescription,
                    Debut = model.StartDate,
                    Fin = model.EndDate,
                    ChargeConsommee = 0,
                    ChargeEstimee = model.ChargeEstimee,
                    Progres = model.Progres,
                    IdProj = (int) projectId,
                    CollabId = model.CollabId,
                    SuperTache = model.SuperTache,
                    
                };

                //Save the new task and the associated previous tasks
                int? newTaskID = dal.SaveNewTask(newTask, model.Depends);

                if(newTaskID != null)
                {
                    Projet projectToDisplay = dal.GetProjet((int)projectId);
                    projectToDisplay.ClientNavigation = dal.GetClient(projectToDisplay.Client);
                    projectToDisplay.ResponsableNavigation = dal.GetCollaborateurs(projectToDisplay.Responsable);

                    projectToDisplay.Tache = dal.GetTaskByProjects(projectToDisplay.Id, c.Id);

                    bool IsProjectManagerTemp = dal.IsProjectManager(HttpContext.User.Identity.Name, projectToDisplay.Id);

                    ProjectViewModel project = new ProjectViewModel
                    {
                        Project = projectToDisplay,
                        IsProjectManager = IsProjectManagerTemp
                    };

                    return View("../Projects/Project", project);
                }

            }

            var collaborateurs = dal.GetCollaborateurs(c.Id);

            model.ProjectId = (int)projectId;
            model.TaskName = "";
            model.StartDate = DateTime.Now;
            model.EndDate = DateTime.Now;
            model._collaboratorList = listCollaborator;
            model._EligiblePreviousTasks = listEligiblePreviousTasks;

            return View("TaskCreation", model);
        }

        // GET: Task
        public ActionResult Index()
        {
            return View();
        }

        // GET: Task/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Task/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Show(int id)
        {
            IDal d = new Dal();
            Tache t = d.GetTache(id);
            t.IdProjNavigation = d.GetProjet(t.IdProj);
            t.Collab = d.GetCollaborateurs(id);
            return View(t);
        }

        // POST: Task/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Task/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Task/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Task/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Task/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}