using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Leaf.DAL.ScaffoldedModels;
using Leaf.DAL.Services;
using Leaf.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Leaf
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

            var model = new ClientCreationViewModel
            {
                clientnew = _clientService.GetById(2)
            };

            return View(model);
        }
    }
}