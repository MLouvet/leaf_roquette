using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Leaf.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Adresse e-mail")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Mot de passe")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Retenir mes identifiants sur cette machine")]
        public bool RememberMe { get; set; }
    }
}
