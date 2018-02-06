using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Leaf.DAL.ScaffoldedModels;
using Leaf.DAL.Services;
using Leaf.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Leaf.Web.Controllers
{
    public class ClientController : Controller
    {
        private static ClientService _clientService;
        private static CollaborateursService _collaborateurService;

        public ClientController(LeafContext context)
        {
            Dal.SetBDD(context);
            _clientService = new ClientService(context);
            _collaborateurService = new CollaborateursService(context);
        }

        public IActionResult Profile()
        {

            var collaborateur = _collaborateurService.GetById(2);
            var model = new ClientViewModel
            {
                clients = _clientService.GetByCollaborateur(collaborateur)
            };

            return View(model);
        }
    }
}