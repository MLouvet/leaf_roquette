using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leaf.Models
{
    public class Dal : IDal
    {
        private LeafDbContext bdd;

        public Dal() { bdd = new LeafDbContext(); }

        public List<Notification> DeleteNotification(Collaborateurs c, Notification n)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<Notification> GetNotifications(Collaborateurs c)
        {
            throw new NotImplementedException();
        }

        public List<Notification> GetRecentNotifications(Collaborateurs c, int n)
        {
            throw new NotImplementedException();
        }

        public List<Tache> GetTaches(Collaborateurs c)
        {
            Projet p1 = new Projet() { Nom = "2D" };
            Projet p2 = new Projet() { Nom = "Zhou" };
            Tache t1 = new Tache
            {
                Id = 0,
                Debut = new DateTime(2017, 09, 24),
                Fin = new DateTime(2017, 09, 28),
                Nom = "Implémenter back-face culling",
                IdProjNavigation = p1
            };
            Tache t2 = new Tache
            {
                Id = 0,
                Debut = new DateTime(2017, 10, 01),
                Fin = new DateTime(2017, 10, 09),
                Nom = "Ajout de l'interface",
                IdProjNavigation = p2
            };
            return new List<Tache>() { t1, t2 };
        }

        public List<Notification> ReadNotification(Collaborateurs c, Notification n)
        {
            throw new NotImplementedException();
        }
    }
}
