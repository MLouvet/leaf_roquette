using System;
using System.Collections.Generic;

namespace Leaf.Models
{
    public partial class Roles
    {
        public Roles()
        {
            Collaborateurs = new HashSet<Collaborateurs>();
        }

        public string Nom { get; set; }

        public ICollection<Collaborateurs> Collaborateurs { get; set; }
    }
}
