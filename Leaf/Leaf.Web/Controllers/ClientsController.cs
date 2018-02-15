using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Leaf.Web.Models;
using Microsoft.AspNetCore.Http;
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
    }
}
