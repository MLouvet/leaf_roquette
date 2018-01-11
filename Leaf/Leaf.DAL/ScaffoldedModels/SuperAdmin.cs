using System;
using System.Collections.Generic;

namespace Leaf.DAL.ScaffoldedModels
{
    public partial class SuperAdmin
    {
        public int Id { get; set; }

        public Admin IdNavigation { get; set; }
    }
}
