using Leaf.DAL.ScaffoldedModels;
using Leaf.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Leaf
{
    public class HomeViewModel : LoginPartialViewModel
    {
        public string displayName;
        public List<Notification> notifications;
        public List<Tache> taches;
        [DisplayName("Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }
    }
}
