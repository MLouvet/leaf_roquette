using Leaf.DAL.DTO;
using Leaf.DAL.Utilities.Translator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leaf.DAL.Services
{
    public class ClientService : BaseService
    {
        public ClientService(ScaffoldedModels.LeafContext context) : base(context) { }

        /// <summary>
        /// Get a client based on its id
        /// </summary>
        /// <param name="pId"> The ID of the client to get </param>
        /// <returns>The client DTO</returns>
        public DAL.DTO.Client GetById(int pId)
        {
            return ClientTranslator.DalToDto(_context.Client.Where(c => c.Id == pId).SingleOrDefault());
        }

        /// <summary>
        /// Return the list of Clients where the collab is a project manager or assigner to
        /// </summary>
        /// <param name="collaborateur"></param>
        /// <returns>the list of client where the current collab has work in common with (project or task in a project)</returns>
        public List<DAL.DTO.Client> GetByCollaborateur(Collaborateurs collaborateur)
        {
            var clientList = new List<DAL.DTO.Client>();
            var projetList = new List<DAL.ScaffoldedModels.Projet>();

            foreach (var projet in _context.Projet.ToList())
            {
                if(projet.Responsable == collaborateur.Id)
                {
                    projetList.Add(projet);
                    break;
                }

                foreach (var tache in _context.Tache.Where(t => t.IdProj == projet.Id && t.CollabId == collaborateur.Id).ToList())
                {
                    projetList.Add(projet);
                    break;
                }
            }

            projetList.Distinct().ToList();

            foreach(var projet in projetList)
            {
                clientList.Add(ClientTranslator.DalToDto(projet.ClientNavigation));
            }

            clientList.Distinct().ToList();

            return clientList;
        }

        /// <summary>
        /// Get a list of all the client in the database
        /// </summary>
        /// <returns>A list of DAL.DTO.Client</returns>
        public List<DAL.DTO.Client> GetAllClient()
        {
            var clientList = new List<Client>();

            foreach(var client in _context.Client.ToList())
            {
                clientList.Add(ClientTranslator.DalToDto(client));
            }

            return clientList;
        }
    }
}
