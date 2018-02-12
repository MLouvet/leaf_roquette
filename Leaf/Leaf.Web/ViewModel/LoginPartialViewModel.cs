using Leaf.DAL.ScaffoldedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leaf.Web.ViewModel
{
    public class LoginPartialViewModel
    {

        public Collaborateurs collaborateur;
        public enum Statut
        {
            Admin = 2, SuperAdmin = 3, Collaborateur = 1, ChefDeProjet = 0, Unknown
        }
        public Statut statut;
        public LoginPartialViewModel()
        {
            statut = Statut.Unknown;
        }
    }
}
