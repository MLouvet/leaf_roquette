using System;
using System.Collections.Generic;
using System.Text;

namespace Leaf.DAL.DTO
{
    public class Roles
    {
        public string Nom { get; set; }

        public ICollection<Collaborateurs> Collaborateurs { get; set; }
    }
}
