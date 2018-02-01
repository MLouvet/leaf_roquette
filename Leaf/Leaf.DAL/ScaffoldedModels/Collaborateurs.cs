using System;
using System.Collections.Generic;

namespace Leaf.DAL.ScaffoldedModels
{
    public partial class Collaborateurs
    {
        public Collaborateurs()
        {
            Notification = new HashSet<Notification>();
            Projet = new HashSet<Projet>();
            Tache = new HashSet<Tache>();
        }

        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Identifiant { get; set; }
        public string Mdp { get; set; }
        public string Mail { get; set; }
        public string Statut { get; set; }

        public Roles StatutNavigation { get; set; }
        public Admin Admin { get; set; }
        public ICollection<Notification> Notification { get; set; }
        public ICollection<Projet> Projet { get; set; }
        public ICollection<Tache> Tache { get; set; }
    }
}
