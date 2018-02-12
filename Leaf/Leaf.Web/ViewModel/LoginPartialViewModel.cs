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
        private static StatutEnum staticStatut = StatutEnum.Unknown;
        public enum StatutEnum
        {
            Admin = 2, SuperAdmin = 3, Collaborateur = 1, ChefDeProjet = 0, Unknown
        }
        private StatutEnum statut;
        public StatutEnum Statut { get { return statut; } set { staticStatut = statut = value; } }
        public LoginPartialViewModel()
        {
            Statut = staticStatut;
        }
    }
}
