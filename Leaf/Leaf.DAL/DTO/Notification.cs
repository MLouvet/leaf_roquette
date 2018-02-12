using System;
using System.Collections.Generic;
using System.Text;

namespace Leaf.DAL.DTO
{
    public class Notification
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public bool Lue { get; set; }
        public DateTime? Horodatage { get; set; }
        public int? Destinataire { get; set; }
        public Collaborateurs DestinataireNavigation { get; set; }
        //public Tache TacheNavigation { get; set; }
    }
}
