using System;
using System.Collections.Generic;

namespace Leaf.ScaffoldedModels
{
    public partial class Notification
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public bool Lue { get; set; }
        public DateTime Horodatage { get; set; }
        public int Destinataire { get; set; }

        public Collaborateurs DestinataireNavigation { get; set; }
    }
}
