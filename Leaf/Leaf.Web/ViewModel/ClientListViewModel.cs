using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Leaf.DAL.DTO;
using Leaf.Web.ViewModel;

namespace Leaf
{
    public class ClientListViewModel : LoginPartialViewModel
    {
        public List<DAL.DTO.Client> clients;
    }
}
