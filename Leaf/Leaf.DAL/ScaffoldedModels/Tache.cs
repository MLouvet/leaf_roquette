using System;
using System.Collections.Generic;

namespace Leaf.DAL.ScaffoldedModels
{
    public partial class Tache
    {
        public Tache()
        {
            InverseSuperTacheNavigation = new HashSet<Tache>();
            Notification = new HashSet<Notification>();
            PreviousTasksPreviousTaskNavigation = new HashSet<PreviousTasks>();
            PreviousTasksTaskNavigation = new HashSet<PreviousTasks>();
        }

        public int Id { get; set; }
        public string Nom { get; set; }
        public string Description { get; set; }
        public DateTime? Debut { get; set; }
        public DateTime? Fin { get; set; }
        public int ChargeEstimee { get; set; }
        public int ChargeConsommee { get; set; }
        public int ChargeEstimeeRestante { get; set; }
        public int Progres { get; set; }
        public int IdProj { get; set; }
        public int CollabId { get; set; }
        public int? SuperTache { get; set; }

        public Collaborateurs Collab { get; set; }
        public Projet IdProjNavigation { get; set; }
        public Tache SuperTacheNavigation { get; set; }
        public ICollection<Tache> InverseSuperTacheNavigation { get; set; }
        public ICollection<Notification> Notification { get; set; }
        public ICollection<PreviousTasks> PreviousTasksPreviousTaskNavigation { get; set; }
        public ICollection<PreviousTasks> PreviousTasksTaskNavigation { get; set; }
    }
}
