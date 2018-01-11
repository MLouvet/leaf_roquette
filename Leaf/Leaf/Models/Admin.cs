using System;
using System.Collections.Generic;

namespace Leaf.Models
{
    public partial class Admin
    {
        public int Id { get; set; }

        public Collaborateurs IdNavigation { get; set; }
        public SuperAdmin SuperAdmin { get; set; }
    }
}
