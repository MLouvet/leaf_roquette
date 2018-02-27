using System;
using System.Collections.Generic;

namespace Leaf.DAL.ScaffoldedModels
{
    public partial class Notification
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int? IdProjet { get; set; }
        public int? IdTache { get; set; }
        public bool Lue { get; set; }
        public DateTime Horodatage { get; set; }
        public int Destinataire { get; set; }

        public Collaborateurs DestinataireNavigation { get; set; }
        public Projet IdProjetNavigation { get; set; }
        public Tache IdTacheNavigation { get; set; }
    }
}
