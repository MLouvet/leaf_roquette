using Leaf.DAL;
using Leaf.DAL.ScaffoldedModels;
using Leaf.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Leaf.Web.Controllers
{
    public class ClientController : Controller
    {
        public ClientController(LeafContext context)
        {
            Dal.SetBDD(context);
        }

        public IActionResult Profile(int? id)
        {
            IDal dal = new Dal();
            var model = new ClientCreationViewModel
            {
                Clientnew = dal.GetClient(/*(int) id*/ 2)
            };

            return View(model);
        }
    }
}