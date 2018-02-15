using Leaf.DAL.ScaffoldedModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Leaf.DAL.Utilities.Translator
{
    public class AdminTranslator
    {
        public static Admin DalToDto(ScaffoldedModels.Admin pAdmin)
        {
            return new Admin
            {
                Id = pAdmin.Id,
                IdNavigation = CollaborateursTranslator.DalToDto(pAdmin.IdNavigation),
                SuperAdmin = pAdmin.SuperAdmin
            };
        }
    }
}
