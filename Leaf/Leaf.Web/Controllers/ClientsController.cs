using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Leaf.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Leaf.DAL.ScaffoldedModels;
using Leaf.DAL.Services;
using Leaf.DAL;
using Leaf.Web.ViewModel;

namespace Leaf.Web.Controllers
{
    public class ClientsController : Controller
    {

        public ClientsController(LeafContext context)
        {
            Dal.SetBDD(context);
        }

        public IActionResult Client(int? id)
        {
            if(id.HasValue)
            {
                IDal dal = new Dal();
                var model = new ClientViewModel
                {
                    client = dal.GetClient((int)id)
                };
                return View(model);
            }

            return View("Error");
        }

        public IActionResult ClientList()
        {
            Dal dal = new Dal();
            Collaborateurs c = dal.GetCollaborateurs(HttpContext.User.Identity.Name);
            var collaborateurs = dal.GetCollaborateurs(c.Id);

            var model = new ClientListViewModel
            {
                clients = dal.GetClients(collaborateurs)
            };
            return View(model);
        }

        /// <summary>
        /// Preparation to display the formular for creating a task
        /// </summary>
        /// <returns>a ClientCreationViewModel to display the task creation formular</returns>
        public IActionResult ClientCreation()
        {
            Dal dal = new Dal();
            Collaborateurs c = dal.GetCollaborateurs(HttpContext.User.Identity.Name);
            var collaborateurs = dal.GetCollaborateurs(c.Id);

            var model = new ClientCreationViewModel
            {
                Clientnew = new Client(),
                IsModification = false
            };

            return View(model);
        }

        /// <summary>
        /// Prepration to display the formular to modify a task
        /// </summary>
        /// <param name="id">The id of the task to modify</param>
        /// <returns>a ClientCreationViewModel to display the task modification formular</returns>
        public IActionResult ClientModification(int? id)
        {
            Dal dal = new Dal();
            Collaborateurs c = dal.GetCollaborateurs(HttpContext.User.Identity.Name);

            Leaf.DAL.ScaffoldedModels.Client clientModif;
            clientModif = dal.GetClient((int)id);

            var model = new ClientCreationViewModel
            {
                Clientnew = clientModif,
                Company = clientModif.Compagnie,
                Adress = clientModif.Adresse,
                ReferentName = clientModif.Nom,
                //ReferentSurname = clientModif.Prenom,
                ReferentMail = clientModif.Mail,
                ReferentPhone = clientModif.Telephone,
                IsModification = true
            };

            return View("ClientModification", model);
        }

        /// <summary>
        /// Action called when the formular is validated
        /// </summary>
        /// <param name="model">The model containing the data to be saved as a new task</param>
        /// <returns>A view, depending if the save failde or not</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult SaveNewClient(ClientCreationViewModel model)
        {
            Dal dal = new Dal();

            if (ModelState.IsValid)
            {
                var newClient = new Leaf.DAL.ScaffoldedModels.Client { Compagnie = model.Company, Adresse = model.Adress, Nom = model.ReferentName, Telephone = model.ReferentPhone, Mail = model.ReferentMail, Projet = new List<Projet>() };

                bool result;

                result = dal.SaveNewClient(newClient);

                if(result)
                {
                    Collaborateurs c = dal.GetCollaborateurs(HttpContext.User.Identity.Name);
                    var returnModel = new ClientListViewModel
                    {
                        clients = dal.GetClients(c)
                    };
                    return View("ClientList", returnModel);
                }

            }

            return View(model);
        }

        /// <summary>
        /// Action called when submitting the formular for 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult SaveClient(ClientCreationViewModel model, int? id)
        {
            Dal dal = new Dal();

            if (ModelState.IsValid)
            {
                model.Clientnew = dal.GetClient((int)id);

                model.Clientnew.Adresse = model.Adress;
                model.Clientnew.Compagnie = model.Company;
                model.Clientnew.Nom = model.ReferentName;
                model.Clientnew.Telephone = model.ReferentPhone;
                model.Clientnew.Mail = model.ReferentMail;

                bool result;

                result = dal.ModifyClient(model.Clientnew);

                if (result)
                {
                    Collaborateurs c = dal.GetCollaborateurs(HttpContext.User.Identity.Name);
                    var returnModel = new ClientListViewModel
                    {
                        clients = dal.GetClients(c)
                    };
                    return View("ClientList", returnModel);
                }

            }

            return View("ClientModification", model);
        }

        /// <summary>
        /// Redirect ot the home page
        /// </summary>
        /// <param name="returnUrl">the url to return to</param>
        /// <returns>A redirection action, redirect to another page</returns>
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}
