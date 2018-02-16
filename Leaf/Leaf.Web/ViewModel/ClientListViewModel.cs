﻿using System.Collections.Generic;
using Leaf.DAL.ScaffoldedModels;
using Leaf.Web.ViewModel;

namespace Leaf
{
    public class ClientListViewModel : LoginPartialViewModel
    {
        public List<Leaf.DAL.ScaffoldedModels.Client> clients;
    }
}
