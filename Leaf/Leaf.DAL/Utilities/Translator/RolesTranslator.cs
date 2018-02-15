using Leaf.DAL.ScaffoldedModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Leaf.DAL.Utilities.Translator
{
    public class RolesTranslator
    {
        public static Roles DalToDto(ScaffoldedModels.Roles pRoles)
        {
            return new Roles
            {
                Collaborateurs = CollaborateursTranslator.DalToDto(pRoles.Collaborateurs),
                Nom = pRoles.Nom
            };
        }

        public static ICollection<Roles> DalToDto(ICollection<ScaffoldedModels.Roles> pRoles)
        {
            var rolesList = new Collection<Roles>();
            foreach (var roles in pRoles)
            {
                rolesList.Add(DalToDto(roles));
            }

            return rolesList;
        }
    }
}
