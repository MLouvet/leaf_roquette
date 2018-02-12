using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Leaf.Web.Models;
using Microsoft.AspNetCore.Http;
using Leaf.DAL.ScaffoldedModels;
using Leaf.DAL.Services;

namespace Leaf.Web.Controllers
{
    public class ClientsController : Controller
    {
        private static ClientService _clientService;

        public ClientsController(LeafContext context)
        {
            Dal.SetBDD(context);
            _clientService = new ClientService(context);
        }

        public IActionResult Client(int? id)
        {
            if(id.HasValue)
            {
                var model = new ClientViewModel
                {
                    client = _clientService.GetById((int)id)
                };
                return View(model);
            }

            return View("Error");
        }
    }
}
