using Leaf.DAL;
using Leaf.DAL.ScaffoldedModels;
using Microsoft.AspNetCore.Mvc;

namespace Leaf
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
                clientnew = new Client()
            };

            return View(model);
        }
    }
}
