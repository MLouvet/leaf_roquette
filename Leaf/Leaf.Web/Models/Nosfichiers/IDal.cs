using Leaf.DAL.ScaffoldedModels;
using System;
using System.Collections.Generic;

namespace Leaf.Web.Models
{
    public interface IDal : IDisposable
    {
        List<Tache> GetTaches(Collaborateurs c);
        Tache GetTache(int id);
        List<Notification> GetNotifications(Collaborateurs c);
        void DeleteNotification(Collaborateurs c, Notification n);
        void ReadNotification(Notification n);
        void UnreadNotification(Notification n);
        List<Notification> GetRecentNotifications(Collaborateurs c, int n);
        Collaborateurs GetCollaborateurs(int id);
        Collaborateurs GetCollaborateurs(string email);
        Projet GetProjet(int id);
    }
}
