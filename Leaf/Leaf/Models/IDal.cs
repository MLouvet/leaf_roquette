using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leaf.Models
{
    public interface IDal : IDisposable
    {
        List<Tache> GetTaches(Collaborateurs c);
    }
}
