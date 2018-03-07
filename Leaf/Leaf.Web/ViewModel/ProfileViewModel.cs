using System.Collections.Generic;
using Leaf.DAL.ScaffoldedModels;
using Leaf.Web.ViewModel;

namespace Leaf
{
    public class ProfileViewModel : LoginPartialViewModel
    {
        public List<Notification> notifications;
        public List<Projet> projet;
        public List<Tache> taches;
    }
}
