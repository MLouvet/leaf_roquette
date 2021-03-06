﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Leaf.DAL.ScaffoldedModels;
using Leaf.Web.ViewModel;

namespace Leaf
{
    public class TaskViewModel : LoginPartialViewModel
    {
        public Leaf.DAL.ScaffoldedModels.Tache Task { get; set; }

        public bool IsTaskResponsible { get; set; }

        public bool IsProjectManager { get; set; }

        public string ValidationErrorMessage { get; set; }

        public int ProjectId { get; set; }

        public int? TaskId { get; set; }

        //Fields to use while creating or modifying a task
        public int Id { get; set; }

        [Required(ErrorMessage = "Un nom est requis pour la tâche")]
        [Display(Name = "Nom de la tâche: *")]
        public string TaskName { get; set; }

        [Required(ErrorMessage = "Un nom est requis pour la tâche")]
        [Display(Name = "Description de la tâche: *")]
        public string TaskDescription { get; set; }

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

        [Required(ErrorMessage = "Une charge estimée est requise")]
        [Display(Name = "Charge estimée: *")]
        public int ChargeEstimee { get; set; }

        [Required(ErrorMessage = "Le progrès est requis pour la tâche (si création, laisser 0): ")]
        [Display(Name = "Progrès: *")]
        public int Progres { get; set; }

        //TODO Don't forget to set projId!
        public int IdProj { get; set; }

        [Required(ErrorMessage = "Un collaborateur associé est requis")]
        [Display(Name = "Collaborateur: *")]
        public int CollabId { get; set; }

        private List<Collaborateurs> __collaboratorList;

        public List<Collaborateurs> _collaboratorList
        {
            get { return __collaboratorList ?? new List<Collaborateurs>(); }
            set { __collaboratorList = value; }
        }

        public IEnumerable<SelectListItem> ListCollabro
        {
            get
            {
                Collaborateurs collabro = new Collaborateurs();
                return new SelectList(_collaboratorList, nameof(collabro.Id), nameof(collabro.Identifiant));
            }
        }

        [Display(Name = "Tâche mère: ")]
        public int? SuperTache { get; set; }

        private List<Tache> __superTaskList;

        public List<Tache> _superTaskList
        {
            get { return __superTaskList ?? new List<Tache>();  }
            set { __superTaskList = value;  }
        }

        public IEnumerable<SelectListItem> ListEligibleSuperTask
        {
            get
            {
                Tache task = new Tache();
                return new SelectList(_superTaskList, nameof(task.Id), nameof(task.Nom));
            }
        }

        [Display(Name = "Tâches précédentes: ")]
        public List<int> Depends { get; set; }

        private List<Tache> __EligiblePreviousTasks;

        public List<Tache> _EligiblePreviousTasks
        {
            get { return __EligiblePreviousTasks ?? new List<Tache>(); }
            set { __EligiblePreviousTasks = value; }
        }

        public IEnumerable<SelectListItem> ListEligiblePreviousTask
        {
            get
            {
                Tache taskTemp = new Tache();
                return new MultiSelectList(_EligiblePreviousTasks, nameof(taskTemp.Id), nameof(taskTemp.Nom));
            }
        }
        
        [Display(Name = "Tâche précédentes: ")]
        public List<int> DependsMod { get; set; }

    }
}
