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
        List<Notification> DeleteNotification(Collaborateurs c, Notification n);
        List<Notification> ReadNotification(Collaborateurs c, Notification n);
        List<Notification> GetRecentNotifications(Collaborateurs c, int n);

    }
}
