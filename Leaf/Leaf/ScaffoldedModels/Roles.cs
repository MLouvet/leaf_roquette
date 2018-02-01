using System;
using System.Collections.Generic;

namespace Leaf.ScaffoldedModels
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
