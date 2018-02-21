using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Leaf.DAL.ScaffoldedModels;
using Leaf.Web.ViewModel;

namespace Leaf
{
    public class ProjectViewModel : LoginPartialViewModel
    {
        public Leaf.DAL.ScaffoldedModels.Projet Project { get; set; }

        //Project's fields for creation and modification

        [Required(ErrorMessage = "Un nom est requis pour le projet")]
        [Display(Name = "Nom du projet: ")]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "Une date de démarrage est requise pour le projet")]
        [Display(Name = "Date de départ du projet: ")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Une date de fin est requise pour le projet")]
        [Display(Name = "Date de fin du projet: ")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EndDate { get; set; }
        
        [Required(ErrorMessage = "Un chef de projet associé est requis")]
        [Display(Name = "Chef de projet: ")]
        public Collaborateurs ProjectLeader { get; set; }

        [Required(ErrorMessage = "Un client associé est requis")]
        [Display(Name = "Client associé: ")]
        public Client ProjectClient { get; set; }
    }
}
