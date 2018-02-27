using Leaf.DAL;
using Leaf.DAL.ScaffoldedModels;
using Leaf.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Leaf.Web.Controllers
{
    public class ClientCreationController : Controller
    {
        public ClientCreationController(LeafContext context)
        {
            Dal.SetBDD(context);
        }

        public IActionResult ClientCreation()
        {

            //DAL.DTO.Client clientTemp = new DAL.DTO.Client();
            var model = new ClientCreationViewModel
            {
                Clientnew = new Client()
            };

            return View(model);
        }
    }
}
