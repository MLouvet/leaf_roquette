using Leaf.DAL.ScaffoldedModels;
using Leaf.DAL.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Leaf.DAL
{
    public class Dal : IDal
    {
        private static LeafContext bdd = null;
        public static IConfiguration Configuration { get; set; }

        //Singleton-services
        private static AdminService adminService;
        private static ClientService clientService;
        private static CollaborateursService collaborateursService;
        private static NotificationService notificationService;
        private static ProjetService projetService;
        private static TacheService tacheService;

      

        public Dal()
        {
            var optionsBuilder = new DbContextOptionsBuilder<LeafContext>();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("LeafDB"));
            SetBDD(bdd = new LeafContext(optionsBuilder.Options));
        }
        public static void SetBDD(LeafContext leafContext)
        {
            //bdd = leafContext;

            ////Refreshing singletons
            //adminService = new AdminService(bdd);
            //clientService = new ClientService(bdd);
            //collaborateursService = new CollaborateursService(bdd);
            //notificationService = new NotificationService(bdd);
            //projetService = new ProjetService(bdd);
            //tacheService = new TacheService(bdd);
        }

        public void Dispose()
        {
            bdd.Dispose();
        }

        #region IDal Methods
        /**
         * 
         * Please see see and make documentation in the IDal.cs file
         * 
         */

        #region Single return-type
        public bool DeleteNotification(Collaborateurs c, Notification n)
        {
            //Checking if notification actually exists.
            if (c == null || !c.Notification.Contains(n))
                return false;

            c.Notification.Remove(n);
            bdd.SaveChanges();
            return true;
        }

        /// <summary>
        /// Save  a new client in base if its calid
        /// </summary>
        /// <param name="c">The client to add</param>
        /// <returns> True if the object has been added, else false</returns>
        public bool SaveNewClient(Client c)
        {
            if(c.Compagnie != null && c.Adresse != null && c.Mail != null && c.Telephone != null
                && c.Nom != null)
            {
                bdd.Client.Add(c);
                bdd.SaveChanges();
                return true;
            }
            return false;
        }

        public Admin GetAdmin(int pId)                        =>              bdd.Admin.Where(a => a.Id == pId).SingleOrDefault();
        public Client GetClient(int id)                       =>              bdd.Client.Where(c => c.Id == id).SingleOrDefault();
        public Collaborateurs GetCollaborateurs(int id)       =>      bdd.Collaborateurs.Where(c => c.Id == id).SingleOrDefault();
        public Collaborateurs GetCollaborateurs(string email) => bdd.Collaborateurs.Where(c => c.Mail == email).SingleOrDefault();
        public Projet GetProjet(int id)                       =>              bdd.Projet.Where(p => p.Id == id).SingleOrDefault();
        public Tache GetTache(int id)                         =>               bdd.Tache.Where(t => t.Id == id).SingleOrDefault();

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


 
        #endregion

        #region Collections
        public List<Notification> GetNotifications(Collaborateurs c)
        {
            if (c == null)
                return new List<Notification>();
            return bdd.Notification.Where(n => n.DestinataireNavigation.Id == c.Id).ToList();
        }


        public List<Notification> GetRecentNotifications(Collaborateurs c, int n)
        {
            if (c == null)
                return new List<Notification>();

            List<Notification> notifications = bdd.Notification.Where(notif => notif.DestinataireNavigation == c).OrderByDescending(no => no.Horodatage).ToList();
            List<Notification> ret = new List<Notification>(Math.Min(n, notifications.Count));

            for (int i = 0; i < notifications.Count && i < n; i++)
            {
                ret.Add(notifications[i]);
            }

            return ret;
        }

        public List<Tache> GetTaches(Collaborateurs c)
        {
            if (c == null)
                return new List<Tache>();
            return bdd.Tache.Where(t => t.Collab.Id == c.Id).ToList();
        }


        public List<Projet> GetProjets(Collaborateurs collaborateur)
        {
            var projetList = new List<Projet>();
            if (collaborateur == null)
                return projetList;

            foreach (var projet in bdd.Projet)
            {
                foreach (var tache in bdd.Tache.Where(t => t.IdProj == projet.Id && t.CollabId == collaborateur.Id))
                {
                    projetList.Add(projet);
                    break;
                }
            }

            return projetList;
        }

        public List<Client> Clients => bdd.Client.ToList();

        public List<Client> GetClients(Collaborateurs collaborateur)
        {
            var clientList = new List<Client>();
            var projetList = new List<Projet>();

            var temp = new List<Projet>();
            temp = bdd.Projet.ToList();
            var inTemp = temp.Count;

            foreach (var projet in bdd.Projet.ToList())
            {
                if (projet.Responsable == collaborateur.Id)
                {
                    projetList.Add(projet);
                    continue;
                }

                foreach (var tache in bdd.Tache.Where(t => t.IdProj == projet.Id && t.CollabId == collaborateur.Id).ToList())
                {
                    projetList.Add(projet);
                    break;
                }
            }

            projetList.Distinct().ToList();

            foreach (var projet in projetList)
            {
                Client clientTemp = bdd.Client.Where((client => client.Id == projet.Client)).SingleOrDefault();
                clientList.Add(clientTemp);
            }

            clientList.Distinct().ToList();

            return clientList;
        }

        public List<Projet> Projets => bdd.Projet.ToList();
        #endregion

        #endregion

        #region Services
        private class AdminService : BaseService
        {
            public AdminService(LeafContext context) : base(context) { }
            public static Admin GetById(int pId) => (_context.Admin.Where(a => a.Id == pId).Single());
        }

        private class ClientService : BaseService
        {
            public ClientService(LeafContext context) : base(context) { }

            /// <summary>
            /// Get a client based on its id
            /// </summary>
            /// <param name="pId"> The ID of the client to get </param>
            /// <returns>The client DTO</returns>
            public Client GetById(int pId)
            {
                return /*ClientTranslator.DalToDto*/(_context.Client.Where(c => c.Id == pId).SingleOrDefault());
            }

            /// <summary>
            /// Return the list of Clients where the collab is a project manager or assigner to
            /// </summary>
            /// <param name="collaborateur"></param>
            /// <returns>the list of client where the current collab has work in common with (project or task in a project)</returns>
            public List<Client> GetByCollaborateur(Collaborateurs collaborateur)
            {
                var clientList = new List<Client>();
                var projetList = new List<Projet>();

                foreach (var projet in _context.Projet.ToList())
                {
                    if (projet.Responsable == collaborateur.Id)
                    {
                        projetList.Add(projet);
                        break;
                    }

                    foreach (var tache in _context.Tache.Where(t => t.IdProj == projet.Id && t.CollabId == collaborateur.Id).ToList())
                    {
                        projetList.Add(projet);
                        break;
                    }
                }

                projetList.Distinct().ToList();

                foreach (var projet in projetList)
                {
                    clientList.Add(/*ClientTranslator.DalToDto*/(projet.ClientNavigation));
                }

                clientList.Distinct().ToList();

                return clientList;
            }

            /// <summary>
            /// Get a list of all the client in the database
            /// </summary>
            /// <returns>A list of Client</returns>
            public List<Client> GetAllClient()
            {
                var clientList = new List<Client>();

                foreach (var client in _context.Client.ToList())
                {
                    clientList.Add(/*ClientTranslator.DalToDto*/(client));
                }

                return clientList;
            }
        }

        private class CollaborateursService : BaseService
        {
            public CollaborateursService(LeafContext context) : base(context) { }
            public Collaborateurs GetById(int pId) => (_context.Collaborateurs.Where(c => c.Id == pId).SingleOrDefault());
        }

        private class NotificationService : BaseService
        {
            public NotificationService(LeafContext context) : base(context) { }
            public List<Notification> GetNotificationsByCollaborateurs(Collaborateurs pCollaborateurs)
            {
                return _context.Notification.Where(n => n.DestinataireNavigation.Id == pCollaborateurs.Id).ToList();
            }

            public List<Notification> DeleteNotification(List<Notification> notifList, int id)
            {
                //TODO Actually save the database data
                foreach (var notif in notifList)
                {
                    if (notif.Id == id)
                    {
                        notifList.Remove(notif);
                    }
                }
                return notifList;
            }
        }

        private class ProjetService : BaseService
        {
            public ProjetService(LeafContext context) : base(context) { }

            /// <summary>
            /// Return all the projetct's DTO's
            /// </summary>
            /// <returns>return a list of DTO projects</returns>
            public List<Projet> GetAllProjets() => _context.Projet.ToList();
            public Projet GetProjetsById(int pId) => _context.Projet.Where(t => t.Id == pId).SingleOrDefault();

            public List<Projet> GetByCollaborateur(Collaborateurs collaborateur)
            {
                var projetList = new List<Projet>();

                foreach (var projet in _context.Projet.ToList())
                {
                    foreach (var tache in _context.Tache.Where(t => t.IdProj == projet.Id && t.CollabId == collaborateur.Id).ToList())
                    {
                        projetList.Add(projet);
                        break;
                    }
                }

                return projetList;
            }
        }

        private class TacheService : BaseService
        {
            public TacheService(LeafContext context) : base(context) { }

            public Tache GetById(int pId) => _context.Tache.Where(t => t.Id == pId).SingleOrDefault();

            public List<Tache> GetByCollaborateurs(Collaborateurs pCollaborateur) => _context.Tache.Where(t => t.Collab.Id == pCollaborateur.Id).ToList();
        }

        #endregion
    }
}
