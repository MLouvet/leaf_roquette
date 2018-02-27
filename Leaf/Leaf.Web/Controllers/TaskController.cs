using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Leaf.DAL;
using Leaf.DAL.ScaffoldedModels;
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
                IDal dal = new Dal();
                Tache taskTemp = dal.GetTache((int)id);

                Projet projTemp = dal.GetProjet(taskTemp.IdProj);
                projTemp.ClientNavigation = dal.GetClient(projTemp.Client);

                taskTemp.IdProjNavigation = projTemp;
                taskTemp.Collab = dal.GetCollaborateurs(taskTemp.CollabId);

                var model = new TaskViewModel
                {
                    Task = taskTemp,
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
            IDal dal = new Dal();
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
        public IActionResult TaskCreation()
        {
            IDal dal = new Dal();
            Collaborateurs c = dal.GetCollaborateurs(HttpContext.User.Identity.Name);
            var collaborateurs = dal.GetCollaborateurs(c.Id);

            var model = new TaskViewModel
            {
                Nom = "",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
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
            IDal dal = new Dal();
            Collaborateurs c = dal.GetCollaborateurs(HttpContext.User.Identity.Name);
            var collaborateurs = dal.GetCollaborateurs(c.Id);

            Tache taskTemp = dal.GetTache((int)id);

            var model = new TaskViewModel
            {
                Task = taskTemp,
                Nom = taskTemp.Nom,
                StartDate = (System.DateTime) taskTemp.Debut,
                EndDate = (System.DateTime)taskTemp.Fin,
                ChargeEstimee = taskTemp.ChargeEstimee,
                Progres = taskTemp.Progres,
                IdProj = taskTemp.IdProj,
                CollabId = taskTemp.CollabId,
                SuperTache = (int) taskTemp.SuperTache,
                //TODO add depends list task
            };

            return View("TaskModification", model);
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