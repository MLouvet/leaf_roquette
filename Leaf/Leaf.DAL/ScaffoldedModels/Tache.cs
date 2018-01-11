using System;
using System.Collections.Generic;

namespace Leaf.DAL.ScaffoldedModels
{
    public partial class Tache
    {
        public Tache()
        {
            InverseDependsNavigation = new HashSet<Tache>();
            InverseSuperTacheNavigation = new HashSet<Tache>();
        }

        public int Id { get; set; }
        public string Nom { get; set; }
        public DateTime? Debut { get; set; }
        public DateTime? Fin { get; set; }
        public int ChargeEstimee { get; set; }
        public int Progres { get; set; }
        public int IdProj { get; set; }
        public int CollabId { get; set; }
        public int? SuperTache { get; set; }
        public int? Depends { get; set; }

        public Collaborateurs Collab { get; set; }
        public Tache DependsNavigation { get; set; }
        public Projet IdProjNavigation { get; set; }
        public Tache SuperTacheNavigation { get; set; }
        public ICollection<Tache> InverseDependsNavigation { get; set; }
        public ICollection<Tache> InverseSuperTacheNavigation { get; set; }
    }
}
