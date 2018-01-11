using System;
using System.Collections.Generic;

namespace Leaf.Models
{
    public partial class Client
    {
        public Client()
        {
            Projet = new HashSet<Projet>();
        }

        public int Id { get; set; }
        public string Nom { get; set; }
        public string Compagnie { get; set; }
        public string Adresse { get; set; }
        public string Mail { get; set; }
        public string Telephone { get; set; }

        public ICollection<Projet> Projet { get; set; }
    }
}
