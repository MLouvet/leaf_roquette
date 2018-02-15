using Leaf.DAL.ScaffoldedModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Leaf.DAL.Utilities.Translator
{
    public class CollaborateursTranslator
    {
        public static Collaborateurs DalToDto(ScaffoldedModels.Collaborateurs pCollaborateurs)
        {
            return new Collaborateurs
            {
                Admin = pCollaborateurs.Admin,
                Id = pCollaborateurs.Id,
                Identifiant = pCollaborateurs.Identifiant,
                Mail = pCollaborateurs.Mail,
                Nom = pCollaborateurs.Nom,
                Notification = NotificationTranslator.DalToDto(pCollaborateurs.Notification),
                Prenom = pCollaborateurs.Prenom,
                Projet = ProjetTranslator.DalToDto(pCollaborateurs.Projet),
                Statut = pCollaborateurs.Statut,
                StatutNavigation = pCollaborateurs.StatutNavigation == null ? null : RolesTranslator.DalToDto(pCollaborateurs.StatutNavigation),
                Tache = TacheTranslator.DalToDto(pCollaborateurs.Tache)
            };
        }

        public static ICollection<Collaborateurs> DalToDto(ICollection<ScaffoldedModels.Collaborateurs> pCollaborateurs)
        {
            var collabList = new Collection<Collaborateurs>();
            foreach (var collab in pCollaborateurs)
            {
                collabList.Add(DalToDto(collab));
            }

            return collabList;
        }
    }
}
