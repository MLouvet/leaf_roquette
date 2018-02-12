using System;
using System.Collections.Generic;

namespace Leaf.DAL.ScaffoldedModels
{
    public partial class Projet
    {
        public Projet()
        {
            Tache = new HashSet<Tache>();
        }

        public int Id { get; set; }
        public string Nom { get; set; }
        public DateTime Debut { get; set; }
        public DateTime Echeance { get; set; }
        public int Client { get; set; }
        public int Responsable { get; set; }

        public Client ClientNavigation { get; set; }
        public Collaborateurs ResponsableNavigation { get; set; }
        public ICollection<Tache> Tache { get; set; }
    }
}
