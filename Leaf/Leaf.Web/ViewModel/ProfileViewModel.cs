using System.Collections.Generic;
using Leaf.DAL.ScaffoldedModels;
using Leaf.Web.ViewModel;

namespace Leaf
{
    public class ProfileViewModel : LoginPartialViewModel
    {
        public List<Leaf.DAL.ScaffoldedModels.Notification> notifications;
        public List<Leaf.DAL.ScaffoldedModels.Projet> projet;
        public List<Leaf.DAL.ScaffoldedModels.Tache> taches;
        public Notification toDelete;
    }
}
