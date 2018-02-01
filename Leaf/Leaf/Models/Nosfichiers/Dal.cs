using Leaf.ScaffoldedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Leaf.Models
{
    public class Dal : IDal
    {
        private LeafContext bdd;

        public Dal() { bdd = new LeafContext(new Microsoft.EntityFrameworkCore.DbContextOptions<LeafContext>()); }

        public void DeleteNotification(Collaborateurs c, Notification n)
        {
            bdd.Remove(n);
            bdd.SaveChanges();
        }

        public void Dispose()
        {
            bdd.Dispose();
        }

        public Collaborateurs GetCollaborateur(int idCollab)
        {
            IEnumerable<Collaborateurs> collab = bdd.Collaborateurs.Where(i => i.Id == idCollab);
            if (collab.Count() == 1) return collab.First();
            else return null;
        }

        public List<Notification> GetNotifications(Collaborateurs c)
        {
            return bdd.Notification.Where(n => n.DestinataireNavigation == c).ToList();
        }

        public List<Notification> GetRecentNotifications(Collaborateurs c, int n)
        {
            List<Notification> ret = new List<Notification>();
            int i = n;
            while (n >= 0)
            {
                ret.Add(bdd.Notification.Where(notif => notif.DestinataireNavigation == c).OrderByDescending(no => no.Id).ToList()[i - n]);
            }
            return ret;
        }

        public List<Tache> GetTaches(Collaborateurs c)
        {
            //Projet p1 = new Projet() { Nom = "2D" };
            //Projet p2 = new Projet() { Nom = "Zhou" };
            //Tache t1 = new Tache
            //{
            //    Id = 0,
            //    Debut = new DateTime(2017, 09, 24),
            //    Fin = new DateTime(2017, 09, 28),
            //    Nom = "Implémenter back-face culling",
            //    IdProjNavigation = p1
            //};
            //Tache t2 = new Tache
            //{
            //    Id = 0,
            //    Debut = new DateTime(2017, 10, 01),
            //    Fin = new DateTime(2017, 10, 09),
            //    Nom = "Ajout de l'interface",
            //    IdProjNavigation = p2
            //};
            return bdd.Tache.Where(t => t.Collab == c).ToList();
            //return new List<Tache>() { t1, t2 };
        }

        public void ReadNotification(Notification n)
        {
            n.Lue = true;
            bdd.SaveChanges();
        }

        public void UnreadNotification(Notification n)
        {
            n.Lue = false;
            bdd.SaveChanges();
        }
    }
}
