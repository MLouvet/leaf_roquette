using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Leaf.Web.ViewModel;
using System.ComponentModel.DataAnnotations;
using Leaf.DAL.ScaffoldedModels;

namespace Leaf
{
    public class TaskListViewModel : LoginPartialViewModel
    {
        public List<Leaf.DAL.ScaffoldedModels.Tache> TaskList { get; set; }
    }
}
