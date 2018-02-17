using Leaf.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Leaf.Web.Models.AccountViewModels
{
    public class LoginViewModel : LoginPartialViewModel
    {
        [Required(ErrorMessage = "L'adresse e-mail est requis")]
        [Display(Name = "Adresse e-mail")]
        [EmailAddress(ErrorMessage = "L'adresse e-mail n'est pas valide")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le mot de passe est requis")]
        [Display(Name = "Mot de passe")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Retenir mes identifiants sur cette machine")]
        public bool RememberMe { get; set; }
    }
}
