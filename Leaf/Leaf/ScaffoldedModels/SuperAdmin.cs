using System;
using System.Collections.Generic;

namespace Leaf.ScaffoldedModels
{
    public partial class SuperAdmin
    {
        public int Id { get; set; }

        public Admin IdNavigation { get; set; }
    }
}
