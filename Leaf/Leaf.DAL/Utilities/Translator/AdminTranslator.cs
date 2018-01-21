using System;
using System.Collections.Generic;
using System.Text;

namespace Leaf.DAL.Utilities.Translator
{
    public class AdminTranslator
    {
        public static DTO.Admin DalToDto(ScaffoldedModels.Admin pAdmin)
        {
            return new DTO.Admin
            {
                Id = pAdmin.Id,
                IdNavigation = CollaborateursTranslator.DalToDto(pAdmin.IdNavigation),
                SuperAdmin = pAdmin.SuperAdmin
            };
        }
    }
}
