using Leaf.DAL.ScaffoldedModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Leaf.DAL.Utilities.Translator
{
    public class ClientTranslator
    {
        public static Client DalToDto(ScaffoldedModels.Client pClient)
        {
            return new Client
            {
                Adresse = pClient.Adresse,
                Compagnie = pClient.Compagnie,
                Id = pClient.Id,
                Mail = pClient.Mail,
                Nom = pClient.Nom,
                Projet = ProjetTranslator.DalToDto(pClient.Projet),
                Telephone = pClient.Telephone
            };
        }
    }
}
