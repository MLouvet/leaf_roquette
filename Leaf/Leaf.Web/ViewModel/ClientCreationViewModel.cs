using Leaf.DAL.ScaffoldedModels;
using Leaf.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Leaf
{
    public class ClientCreationViewModel : LoginPartialViewModel
    {
        public Client clientnew;

        //Boolean, if it's a modification, it's true
        public bool isModification { get; set; }

        //Company info
        [Required(ErrorMessage = "Le nom de la compagnie est requis")]
        [Display(Name = "Entreprise: ")]
        public string Company { get; set; }

        [Required(ErrorMessage = "L'adresse de la compagnie est requise")]
        [Display(Name = "Adresse de l'entreprise: ")]
        public string Adress { get; set; }

        //Company referent info
        [Required(ErrorMessage = "Le nom du référent est requis")]
        [Display(Name = "Nom du référent: ")]
        public string ReferentName { get; set; }

        /*[Required(ErrorMessage = "Le prénom du référent est requis")]*/
        [Display(Name = "Prénom du référent: ")]
        public string ReferentSurname { get; set; }

        [Required(ErrorMessage = "Le téléphone du référent est requis")]
        [Display(Name = "Telephone: ")]
        public string ReferentPhone { get; set; }

        [Required(ErrorMessage = "L'adresse Email du référent est requise")]
        [EmailAddress]
        [Display(Name = "Email: ")]
        public string ReferentMail { get; set; }
    }
}
