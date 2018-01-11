using System;
using System.Collections.Generic;

namespace Leaf.Models
{
    public partial class SuperAdmin
    {
        public int Id { get; set; }

        public Admin IdNavigation { get; set; }
    }
}
