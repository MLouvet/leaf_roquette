using System.ComponentModel.DataAnnotations;
using Leaf.DAL.ScaffoldedModels;

namespace Leaf.Web.ViewModel
{
    public class ClientCreationViewModel : LoginPartialViewModel
    {
        public Client Clientnew;

        //Boolean, if it's a modification, it's true
        public bool IsModification { get; set; }

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
