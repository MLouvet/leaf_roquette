using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Leaf.DAL;
using Leaf.DAL.ScaffoldedModels;
using Leaf.DAL.Services;
using Leaf.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Leaf
{
    public class ClientController : Controller
    {
        public ClientController(LeafContext context)
        {
            Dal.SetBDD(context);
        }

        public IActionResult Profile()
        {
            IDal dal = new Dal();
            var model = new ClientCreationViewModel
            {
                clientnew = dal.GetClient(2)
            };

            return View(model);
        }
    }
}