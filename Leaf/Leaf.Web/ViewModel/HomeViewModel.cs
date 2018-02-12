using Leaf.DAL.DTO;
using Leaf.Web.ViewModel;
using System.Collections.Generic;

namespace Leaf
{
    public class HomeViewModel : LoginPartialViewModel
    {
        public string displayName;
        public List<Notification> notifications;
        public List<Tache> taches;
    }
}
