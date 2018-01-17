using Leaf.DAL.ScaffoldedModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Leaf.DAL.DTO
{
    public class Admin
    {
        public int Id { get; set; }

        public Collaborateurs IdNavigation { get; set; }
        public SuperAdmin SuperAdmin { get; set; }
    }
}
