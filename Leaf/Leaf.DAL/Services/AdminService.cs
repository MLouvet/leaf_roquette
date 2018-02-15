//using Leaf.DAL.DTO;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Linq;
//using Leaf.DAL.Utilities.Translator;

//namespace Leaf.DAL.Services
//{
//    public class AdminService : BaseService
//    {
//        public AdminService(ScaffoldedModels.LeafContext context) : base(context) { }

//        public static Admin GetById(int pId)
//        {
//            return AdminTranslator.DalToDto(_context.Admin.Where(a => a.Id == pId).Single());
//        }
//    }
//}
