using System;
using System.Collections.Generic;
using System.Text;

namespace Leaf.DAL.Services
{
    public class BaseService
    {
        protected static ScaffoldedModels.LeafContext _context = null;

        public BaseService(ScaffoldedModels.LeafContext context)
        {
            _context = context;
        }
    }
}
