using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Leaf.DAL.ScaffoldedModels;
using Leaf.Web.ViewModel;

namespace Leaf
{
    public class CollabViewModel: LoginPartialViewModel
    {
        public Collaborateurs Collaborateur { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsSuperAdmin { get; set; }
        public string ValidationErrorMessage { get; set; }

        [Required(ErrorMessage = "Un prenom est requis pour un collaborateur")]
        [Display(Name = "Prénom du collaborateur")]
        public string CollabFirstName { get; set; }

        [Required(ErrorMessage = "Un nom est requis pour un collaborateur")]
        [Display(Name = "Nom du collaborateur")]
        public string CollabLastName { get; set; }

        [Required(ErrorMessage = "Un iddentifiant est requis pour un collaborateur")]
        [Display(Name = "Iddentifiant du collaborateur")]
        public string CollabId { get; set; }

        [Required(ErrorMessage = "Un mot de passe est requis pour un collaborateur")]
        [Display(Name = "Mot de passe du collaborateur")]
        public string CollabPasswrd { get; set; }

        [Required(ErrorMessage = "Une adresse email est requise pour un collaborateur")]
        [Display(Name = "Adresse email du collaborateur")]
        public string CollabMail { get; set; }

        [Required(ErrorMessage = "Un statut est requise pour un collaborateur")]
        [Display(Name = "Statut du collaborateur")]
        public string CollabStatus;

        private List<string> __status = new List<string> { "Chef_Projet", "Collaborateur" };

        public List<string> _status { get => __status; set => __status = value; }

        public CollabViewModel()
        {
            if (IsSuperAdmin) __status.Add("Admin");
        }


        public IEnumerable<SelectListItem> ListStatus
        {
            get
            {
                string status = "";
                return new SelectList(_status, status);
            }
        }
    }
}
