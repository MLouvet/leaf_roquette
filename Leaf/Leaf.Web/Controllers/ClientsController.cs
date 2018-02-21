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

        public IActionResult ClientCreation()
        {
            Dal dal = new Dal();
            Collaborateurs c = dal.GetCollaborateurs(HttpContext.User.Identity.Name);
            var collaborateurs = dal.GetCollaborateurs(c.Id);

            var model = new ClientCreationViewModel
            {
                clientnew = new Client(),
                isModification = false
            };

            return View(model);
        }

        public IActionResult ClientModification(int? id)
        {
            Dal dal = new Dal();
            Collaborateurs c = dal.GetCollaborateurs(HttpContext.User.Identity.Name);

            Leaf.DAL.ScaffoldedModels.Client clientModif;
            clientModif = dal.GetClient((int)id);

            var model = new ClientCreationViewModel
            {
                clientnew = clientModif,
                Company = clientModif.Compagnie,
                Adress = clientModif.Adresse,
                ReferentName = clientModif.Nom,
                //ReferentSurname = clientModif.Prenom,
                ReferentMail = clientModif.Mail,
                ReferentPhone = clientModif.Telephone,
                isModification = true
            };

            return View("ClientModification", model);
        }

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

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult SaveClient(ClientCreationViewModel model, int? id)
        {
            Dal dal = new Dal();

            if (ModelState.IsValid)
            {
                model.clientnew = dal.GetClient((int)id);

                model.clientnew.Adresse = model.Adress;
                model.clientnew.Compagnie = model.Company;
                model.clientnew.Nom = model.ReferentName;
                model.clientnew.Telephone = model.ReferentPhone;
                model.clientnew.Mail = model.ReferentMail;

                bool result;

                result = dal.ModifyClient(model.clientnew);

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
