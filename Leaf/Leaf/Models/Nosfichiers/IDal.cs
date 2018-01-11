using Leaf.ScaffoldedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leaf.Models
{
    public interface IDal : IDisposable
    {
        List<Tache> GetTaches(Collaborateurs c);
        List<Notification> GetNotifications(Collaborateurs c);
        void DeleteNotification(Collaborateurs c, Notification n);
        void ReadNotification(Notification n);
        void UnreadNotification(Notification n);
        List<Notification> GetRecentNotifications(Collaborateurs c, int n);

    }
}
