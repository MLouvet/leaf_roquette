using Leaf.DAL;
using Leaf.DAL.ScaffoldedModels;
using Leaf.Web.Data;
using Leaf.Web.Models;
using Leaf.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static Leaf.Web.ViewModel.LoginPartialViewModel;

namespace Leaf.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            ILogger<AccountController> logger,
            LeafContext context)
        {
            new Dal().SetAndInitBDD(context, userManager, signInManager);
        }

        // GET: Clients
        public IActionResult Index()
        {

            /*
             * 
             * 
             * 
             * 
             * 
             * Exemple d'accès aux données de l'utilisateur, ici
             *
             * 
             * 
             */
            IDal dal = new Dal();
            Collaborateurs collaborateurs = dal.GetCollaborateurs(HttpContext.User.Identity.Name);



            StatutEnum status;
            if (collaborateurs.Statut == "CHEF_PROJET")
                status = StatutEnum.ChefDeProjet;
            else if (collaborateurs.Statut == "COLLABORATEUR")
                status = StatutEnum.Collaborateur;
            else if (collaborateurs.Statut == "ADMIN")
                status = StatutEnum.Admin;
            else
                status = StatutEnum.SuperAdmin;

            var model = new HomeViewModel
            {

                Statut = status,
                displayName = collaborateurs.Prenom + " " + collaborateurs.Nom,
                notifications = dal.GetRecentNotifications(collaborateurs, 5),
                taches = dal.GetTaches(collaborateurs)
            };

            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}