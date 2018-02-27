using System;
using System.Collections.Generic;

namespace Leaf.DAL.ScaffoldedModels
{
    public partial class PreviousTasks
    {
        public int Id { get; set; }
        public int PreviousTask { get; set; }
        public int Task { get; set; }

        public Tache PreviousTaskNavigation { get; set; }
        public Tache TaskNavigation { get; set; }
    }
}
