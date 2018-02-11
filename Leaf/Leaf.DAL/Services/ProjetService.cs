using Leaf.DAL.DTO;
using Leaf.DAL.Utilities.Translator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leaf.DAL.Services
{
    public class ProjetService : BaseService
    {
        public ProjetService(ScaffoldedModels.LeafContext context) : base(context) { }
        
        /// <summary>
        /// Return all the projetct's DTO's
        /// </summary>
        /// <returns>return a list of DTO projects</returns>
        public List<DTO.Projet> GetAllProjets()
        {
            var projetList = new List<DTO.Projet>();

            foreach(var projet in _context.Projet.ToList())
            {
                projetList.Add(ProjetTranslator.DalToDto(projet));
            }

            return projetList;
        }

        public DTO.Projet getProjetsById(int pId)
        {
            return ProjetTranslator.DalToDto(_context.Projet.Where(t => t.Id == pId).SingleOrDefault());
        }

        public List<DTO.Projet> GetByCollaborateur(Collaborateurs collaborateur)
        {
            var projetList = new List<DTO.Projet>();

            foreach(var projet in _context.Projet.ToList())
            {
                foreach(var tache in _context.Tache.Where(t => t.IdProj == projet.Id && t.CollabId == collaborateur.Id).ToList())
                {
                    projetList.Add(ProjetTranslator.DalToDto(projet));
                    break;
                }
            }

            return projetList;
        }
    }
}
