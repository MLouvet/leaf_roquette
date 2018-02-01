using Leaf.DAL.DTO;
using Leaf.DAL.Utilities.Translator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leaf.DAL.Services
{
    public class TacheService : BaseService
    {
        public TacheService(ScaffoldedModels.LeafContext context) : base(context) { }

        public Tache GetById(int pId)
        {
            return TacheTranslator.DalToDto(_context.Tache.Where(t => t.Id == pId).SingleOrDefault());
        }

        public List<Tache> GetByCollaborateurs(Collaborateurs pCollaborateur)
        {
            var tacheList = new List<Tache>();

            foreach(var tache in _context.Tache.Where(t => t.Collab.Id == pCollaborateur.Id).ToList())
            {
                tacheList.Add(TacheTranslator.DalToDto(tache));
            }

            return tacheList;
        }
    }
}
