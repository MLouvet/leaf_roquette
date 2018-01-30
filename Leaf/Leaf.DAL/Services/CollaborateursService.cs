using Leaf.DAL.DTO;
using Leaf.DAL.Utilities.Translator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leaf.DAL.Services
{
    public class CollaborateursService : BaseService
    {
        public CollaborateursService(ScaffoldedModels.LeafContext context) : base(context) { }

        public Collaborateurs GetById(int pId)
        {
            return CollaborateursTranslator.DalToDto(_context.Collaborateurs.Where(c => c.Id == pId).SingleOrDefault());
        }
    }
}
