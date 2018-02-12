using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Leaf.DAL.DTO;
using Leaf.Web.ViewModel;

namespace Leaf
{
    public class ProfileViewModel : LoginPartialViewModel
    {
        public List<Notification> notifications;
        public List<Tache> taches;
    }
}
