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
    public class ProjectViewModel : LoginPartialViewModel
    {
        public Leaf.DAL.ScaffoldedModels.Projet Project { get; set; }

        public bool IsModification { get; set; }

        public bool IsProjectManager { get; set; }

        public string ValidationErrorMessage { get; set; }

        //Project's fields for creation and modification

        [Required(ErrorMessage = "Un nom est requis pour le projet")]
        [Display(Name = "Nom du projet: *")]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "Une date de démarrage est requise pour le projet")]
        [Display(Name = "Date de départ du projet: *")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Une date de fin est requise pour le projet")]
        [Display(Name = "Date de fin du projet: *")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EndDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if ((EndDate - StartDate).TotalMilliseconds < 0)
            {
                yield return
                  new ValidationResult(errorMessage: "La date de début doit être avant la date de fin",
                                       memberNames: new[] { "EndDate" });
            }
        }

        [Required(ErrorMessage = "Un chef de projet associé est requis")]
        [Display(Name = "Chef de projet: *")]
        public int ProjectLeader { get; set; }

        [Required(ErrorMessage = "Un client associé est requis")]
        [Display(Name = "Client associé: *")]
        /*public int ProjectClient { get; set; }*/
        private int _temp;

        public int ProjectClient
        {
            get { return _temp; }
            set { _temp = value; }
        }

        //For client dropdown list
        private List<Client> __clients;

        public List<Client> _clients
        {
            get { return __clients ?? new List<Client>(); }
            set { __clients = value; }
        }

        public IEnumerable<SelectListItem> ListClient
        {
            get
            {
                Client client = new Client();
                return new SelectList(_clients, nameof(client.Id), nameof(client.Compagnie));
            }
        }


        //public List<Collaborateurs> _projectManagerList;
        private List<Collaborateurs> __projectManagerList;

        public List<Collaborateurs> _projectManagerList
        {
            get { return __projectManagerList ?? new List<Collaborateurs>(); }
            set { __projectManagerList = value; }
        }


        public IEnumerable<SelectListItem> ListCollabro
        {
            get
            {
                Collaborateurs collabro = new Collaborateurs();
                return new SelectList(_projectManagerList, nameof(collabro.Id), nameof(collabro.Identifiant));
            }
        }

    }
}
