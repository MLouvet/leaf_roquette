using Leaf.DAL;
using Leaf.DAL.ScaffoldedModels;
using Leaf.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leaf.Web.Data
{
    public static class DalExtensions
    {
        private static bool userManagerLoaded = false;
        public static async void SetAndInitBDD(this Dal dal, LeafContext leafContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            if (!userManagerLoaded)
            {
                //Cleaning previous data loaded
                Dal.SetBDD(leafContext);
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseSqlServer(Dal.Configuration.GetConnectionString("DefaultConnection"));

                //If logged in
                if (signInManager.Context.User.Identity.IsAuthenticated)
                {
                    await signInManager.SignOutAsync();
                }

                using (var context = new ApplicationDbContext(optionsBuilder.Options))
                {
                    foreach (ApplicationUser user in context.Users)
                    {
                        try
                        {
                            await userManager.DeleteAsync(user);
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
                var l = leafContext.Collaborateurs.ToList();
                foreach (Collaborateurs c in l)
                {
                    var user = new ApplicationUser { UserName = c.Mail, Email = c.Mail };
                    try
                    {
                        var result = await userManager.CreateAsync(user, c.Mdp);
                        if (!result.Succeeded)
                        {
                            foreach (IdentityError e in result.Errors)
                            {
                                Console.Write("ERRRREUUUUURRRSS ########### ");
                                Console.WriteLine(e.Description); //La console n'affichera rien
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }


                }
                userManagerLoaded = true;
            }
        }
    }
}
