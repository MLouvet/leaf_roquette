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
            if (c.Compagnie != null && c.Adresse != null && c.Mail != null && c.Telephone != null
                && c.Nom != null)
            {
                bdd.Client.Add(c);
                bdd.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Modify a client in the database
        /// </summary>
        /// <param name="c">The new client values</param>
        /// <returns>True if the operation succeded</returns>
        public bool ModifyClient(Client c)
        {
            Client entity = bdd.Client.Find(c.Id);
            if (entity == null)
                return false;

            bdd.Entry(entity).CurrentValues.SetValues(c);
            bdd.SaveChanges();
            var temp = bdd.Client.Find(c.Id);
            return true;
        }

        /// <summary>
        /// Save a new Project in the DB
        /// </summary>
        /// <param name="project">The new Project to add</param>
        /// <returns>true if adding was successful, false in an other case</returns>
        public bool SaveNewProject(Projet project)
        {
            if (project.Nom != null && project.Debut != null && project.Echeance != null
                && bdd.Client.Find(project.Client) != null && bdd.Collaborateurs.Find(project.Responsable) != null)
            {
                project.ClientNavigation = bdd.Client.Find(project.Client);
                project.ResponsableNavigation = bdd.Collaborateurs.Find(project.Responsable);
                project.Tache = new List<Tache>();
                //Projet entity = bdd.Projet.Find(project.Id);
                bdd.Projet.Add(project);
                bdd.SaveChanges();
                return true;
            }

            return false;
        }

        public bool MakeNewCollab(Collaborateurs c)
        {
            bdd.Collaborateurs.Add(c);
            return bdd.SaveChanges() > 0;
        }
        /// <summary>
        /// Modify existing values for a project in the DB based on the ID
        /// </summary>
        /// <param name="project">The value which should replace the existing ones</param>
        /// <returns>True is the values have been correctly added</returns>
        public bool ModifyProject(Projet project)
        {
            Projet entity = bdd.Projet.Find(project.Id);
            if (entity == null)
                return false;

            entity.Nom = project.Nom;
            entity.Debut = project.Debut;
            entity.Echeance = project.Echeance;

            entity.Client = project.Client;
            entity.ClientNavigation = bdd.Client.Find(project.Client);
            entity.Responsable = project.Responsable;
            entity.ResponsableNavigation = bdd.Collaborateurs.Find(project.Responsable);

            if (entity.ResponsableNavigation == null || entity.ClientNavigation == null)
                return false;

            bdd.Entry(entity).CurrentValues.SetValues(project);
            bdd.SaveChanges();
            var temp = bdd.Client.Find(project.Id);
            return true;
        }

        /// <summary>
        /// Tells if the current user is the manager for the project
        /// </summary>
        /// <param name="name">The name/email of th user</param>
        /// <param name="projectId">the id of the project displayed</param>
        /// <returns>true if the urrentuser is the project manager</returns>
        public bool IsProjectManager(string name, int projectId)
        {
            if (this.GetCollaborateurs(name).Id == bdd.Projet.Find(projectId).Responsable)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Tells if the current user is a SuperAdmin
        /// </summary>
        public bool IsSuperAdmin(string name)
        {
            return bdd.SuperAdmin.Any(s => s.Id == GetCollaborateurs(name).Id);
        }
        /// <summary>
        /// Get all the people which have the status to manage a project
        /// </summary>
        /// <returns>A list of collaborateur containint all project managers (admin and super admin also)</returns>
        public List<Collaborateurs> getProjectManagers()
        {
            return bdd.Collaborateurs.Where(c => c.Statut != "COLLABORATEUR").ToList();
        }

        public Admin GetAdmin(int pId) => bdd.Admin.Where(a => a.Id == pId).SingleOrDefault();
        public Client GetClient(int id) => bdd.Client.Where(c => c.Id == id).SingleOrDefault();
        public Collaborateurs GetCollaborateurs(int id) => bdd.Collaborateurs.Where(c => c.Id == id).SingleOrDefault();
        public Collaborateurs GetCollaborateurs(string email) => bdd.Collaborateurs.Where(c => c.Mail == email).SingleOrDefault();
        public Projet GetProjet(int id) => bdd.Projet.Where(p => p.Id == id).SingleOrDefault();
        public Tache GetTache(int id) => bdd.Tache.Where(t => t.Id == id).SingleOrDefault();

        /// <summary>
        /// Change notification status to read
        /// </summary>
        /// <param name="n">the notification to consider</param>
        public void ReadNotification(Notification n)
        {
            n.Lue = true;
            bdd.SaveChanges();
        }

        /// <summary>
        /// Change notification status to unread
        /// </summary>
        /// <param name="n">The notification to consider</param>
        public void UnreadNotification(Notification n)
        {
            n.Lue = false;
            bdd.SaveChanges();
        }



        #endregion

        #region Collections

        /// <summary>
        /// Get all the collaborators in the database
        /// </summary>
        /// <returns>A list of all the collaborators in the database</returns>
        public List<Collaborateurs> AllCollaborateurs => bdd.Collaborateurs.ToList();

        /// <summary>
        /// Get all notifications for a collaborator
        /// </summary>
        /// <param name="c">The collaborator to consider</param>
        /// <returns>The list of notifications for the collaborator</returns>
        public List<Notification> GetNotifications(Collaborateurs c)
        {
            if (c == null)
                return new List<Notification>();
            return bdd.Notification.Where(n => n.DestinataireNavigation.Id == c.Id).ToList();
        }

        /// <summary>
        /// Get the list of the recent notification linked to a collaborator
        /// </summary>
        /// <param name="c">The collaborator</param>
        /// <param name="n">the max number oof notificcation returned, the number of notifications returned can be less than n</param>
        /// <returns>A list of notifications</returns>
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

        /// <summary>
        /// Get all tasks of a collaborator
        /// </summary>
        /// <param name="c">The collaborator form which we want to get the tasks</param>
        /// <returns>The list of tasks of the collaborator</returns>
        public List<Tache> GetTaches(Collaborateurs c)
        {
            if (c == null)
                return new List<Tache>();
            return bdd.Tache.Where(t => t.Collab.Id == c.Id).ToList();
        }

        /// <summary>
        /// Get all the projects linked to a collaborator, by a task or being the project manager
        /// </summary>
        /// <param name="collaborateur">The collaborator considered</param>
        /// <returns>A list of projects</returns>
        public List<Projet> GetProjets(Collaborateurs collaborateur)
        {
            var projetList = new List<Projet>();
            if (collaborateur == null)
                return projetList;

            foreach (var projet in bdd.Projet)
            {
                if (projet.Responsable == collaborateur.Id)
                {
                    projetList.Add(projet);
                    continue;
                }

                foreach (var tache in bdd.Tache.Where(t => t.IdProj == projet.Id && t.CollabId == collaborateur.Id))
                {
                    projetList.Add(projet);
                    break;
                }
            }

            return projetList;
        }

        /// <summary>
        /// Get all the clients in the database
        /// </summary>
        public List<Client> Clients => bdd.Client.ToList();

        /// <summary>
        /// Get all the clients linked by a task or a project to the collaborator
        /// </summary>
        /// <param name="collaborateur">the collaborator considered</param>
        /// <returns>A list of the clients linked one way or another to the collaborator in parameter</returns>
        public List<Client> GetClients(Collaborateurs collaborateur)
        {
            var clientList = new List<Client>();
            var projetList = new List<Projet>();

            var temp = new List<Projet>();
            temp = bdd.Projet.ToList();
            var inTemp = temp.Count;

            //Si c'est un admin ou un chef de projet, on retourne tous les clients
            if (collaborateur.Statut == "ADMIN" || collaborateur.Statut == "CHEF_PROJET")
            {
                foreach (var client in bdd.Client)
                    clientList.Add(client);

                return clientList;
            }

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

        #region TaskRelatedFunctions
        /// <summary>
        /// Returns a list containing all tasks linked to a project
        /// </summary>
        /// <param name="projectId">The id of the project linked to the tasks</param>
        /// <returns>The list of all task linked to the project, the id project being the parameter</returns>
        public List<Leaf.DAL.ScaffoldedModels.Tache> GetTaskByProjects(int projectId, int collabId)
        {
            List<Tache> taskList = new List<Tache>();
            Collaborateurs collabro = this.GetCollaborateurs(collabId);

            foreach (var taskTemp in bdd.Tache.Where(t => t.IdProj == projectId))
            {
                if (collabro.Statut == "ADMIN" || collabro.Statut == "SUPER_ADMIN" ||
                   (collabro.Statut == "CHEF_PROJET" && this.GetProjet(projectId).Responsable == collabId) || taskTemp.CollabId == collabId)
                {
                    taskList.Add(taskTemp);
                }
            }

            return taskList;
        }

        /// <summary>
        /// Get a list of all the tasks associated with a project
        /// </summary>
        /// <param name="projectId">the id of the project</param>
        /// <returns>The list of the tasks</returns>
        public List<Leaf.DAL.ScaffoldedModels.Tache> GetTaskByProjects(int projectId)
        {
            List<Tache> taskList = new List<Tache>();
            foreach (Tache tache in bdd.Tache.Where(t => t.IdProj == projectId))
            {
                taskList.Add(tache);
            }
            return taskList;
        }

        /// <summary>
        /// Get the hierarchy level of a task
        /// </summary>
        /// <param name="taskId">the task we want the level of hierarchy</param>
        /// <param name="hierarchyLevel">the current hierarchy level</param>
        /// <returns></returns>
        public int GetTaskLevelOfHierarchy(int taskId, int hierarchyLevel = 1)
        {
            Tache taskTemp = this.GetTache(taskId);
            if (taskTemp.SuperTache != null && taskTemp.SuperTache != taskTemp.Id)
            {
                hierarchyLevel = GetTaskLevelOfHierarchy((int) taskTemp.SuperTache, ++hierarchyLevel);
            }

            return hierarchyLevel;
        }

        /// <summary>
        /// Remove from the second parameter the task mother of the one considered
        /// </summary>
        /// <param name="motherTask">The id of the the mother task to consider</param>
        /// <param name="potentialPreviousTasks">The current list of </param>
        /// <returns>The list where the mother tasks have been removed</returns>
        public List<Tache> RemoveMotherTasks(int currentTask, List<Tache> potentialPreviousTasks, int motherMaxRank = 3)
        {
            Tache currentT = this.GetTache(currentTask);
            if (motherMaxRank > 0 && currentT.SuperTache != null)
            {
                potentialPreviousTasks = RemoveMotherTasks((int)currentT.SuperTache, potentialPreviousTasks, motherMaxRank - 1);
                potentialPreviousTasks.Remove(this.GetTache((int)currentT.SuperTache));
            }

            return potentialPreviousTasks;
        }

        /// <summary>
        /// Return a list of the potential supertask for the current task
        /// </summary>
        /// <param name="idCurrentTask">the id of the current task</param>
        /// <param name="projectId">the id of the project the task is in</param>
        /// <returns>a list of potential super task for the  current task</returns>
        public List<Tache> GetPotentialSuperTache(int projectId, List<int> previousTasksIDInViewModel, int idCurrentTask = -1)
        {
            List<Tache> listPotentialSuperTache = this.GetTaskByProjects(projectId);

            //Remove all the task that are currently saved as previous tasks for the idCurrentTask
            foreach(PreviousTasks pt in bdd.PreviousTasks.Where(pt => pt.Task == idCurrentTask).ToList())
            {
                listPotentialSuperTache.Remove(this.GetTache(pt.PreviousTask));
            }

            //Remove the task that are currenty in the previous task list in the view model
            foreach(int previousTaskID in previousTasksIDInViewModel)
            {
                listPotentialSuperTache.Remove(this.GetTache(previousTaskID));
            }

            //Remove the task that have currently 3 level of hierarchy
            foreach(Tache taskTemp in listPotentialSuperTache)
            {
                if (this.GetTaskLevelOfHierarchy(taskTemp.Id) > 3)
                    listPotentialSuperTache.Remove(taskTemp);

            }
            
            return listPotentialSuperTache;
        }

        /// <summary>
        /// Get a list of the task that eligible to be a previous task of the one currently considered
        /// </summary>
        /// <param name="projectId">THe id of the project</param>
        /// <param name="currentTaskId">The id of the task we consider</param>
        /// <returns>A list of tasks, which are suitable to be a previous task</returns>
        public List<Leaf.DAL.ScaffoldedModels.Tache> GetPotentialPreviousTasks(int projectId, List<int> PreviousTaskInViewModel, int? motherTaskInViewModel = -1, int currentTaskId = -1)
        {
            List<Tache> potentialPreviousTasks = new List<Tache>();

            Tache currentTask = new Tache();
            if (currentTaskId == -1)
            {
                foreach (var taskTemp in bdd.Tache.Where(t => t.IdProj == projectId))
                {
                    potentialPreviousTasks.Add(taskTemp);
                }
            }
            else
            {
                currentTask = this.GetTache(currentTaskId);
                foreach (var taskTemp in bdd.Tache.Where(t => t.IdProj == projectId
                    && t.Id != currentTaskId
                    && (((DateTime)t.Fin) - ((DateTime)currentTask.Debut)).Milliseconds < 0))
                {
                    potentialPreviousTasks.Add(taskTemp);
                }

                potentialPreviousTasks = RemoveMotherTasks(currentTaskId, potentialPreviousTasks, 3);
            }

            foreach (var t in PreviousTaskInViewModel)
            {
                potentialPreviousTasks.Remove(this.GetTache(t));
            }
            potentialPreviousTasks.Remove(this.GetTache((int)motherTaskInViewModel));

            return potentialPreviousTasks;
        }



        /// <summary>
        /// Save all the id in the list as previous task of the current task
        /// </summary>
        /// <param name="previousTasksId">the list of the task to be added as previous tasks</param>
        /// <param name="currentTask">the task which have the taks n the lists as previous task</param>
        /// <returns>A boolean telling if the save succeded</returns>
        public bool SavePreviousTaskSet(List<int> previousTasksId, int currentTask)
        {
            if(previousTasksId != null)
            {
                foreach (int taskId in previousTasksId)
                {
                    PreviousTasks newPrevious = new PreviousTasks
                    {
                        PreviousTask = taskId,
                        Task = currentTask
                    };

                    bdd.PreviousTasks.Add(newPrevious);
                    bdd.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Do the verification before adding the task to the database
        /// </summary>
        /// <param name="projectId">the id of the project in whih the task will be added</param>
        /// <returns>the result of the verification</returns>
        public string VerifyNewTask(int projectId)
        {
            // TODO
            return "";
        }

        /// <summary>
        /// Save a new task in the database
        /// </summary>
        /// <param name="newTask">the new Task To be saved</param>
        /// <returns>the id of the saved task</returns>
        public int? SaveNewTask(Tache newTask, List<int> previousTaskSetId)
        {
            if (newTask != null)
            {
                bdd.Tache.Add(newTask);
                bdd.SaveChanges();

                SavePreviousTaskSet(previousTaskSetId, newTask.Id);

                return newTask.Id;
            }

            return null;
        }

        /// <summary>
        /// Modify a task and it's associated previous task set in the database
        /// </summary>
        /// <param name="newTask">The task to save</param>
        /// <param name="previousTaskSetId">The list of id to save for the previous task</param>
        /// <returns></returns>
        public int? ModifyTask(Tache newTask, List<int> previousTaskSetId)
        {
            //TODO ENVOYER DES NOTIFICATIONS
            if (newTask != null)
            {
                Tache entity = bdd.Tache.Find(newTask.Id);
                if (entity == null)
                    return null;

                bdd.Entry(entity).CurrentValues.SetValues(newTask);
                

                foreach(var previous in  bdd.PreviousTasks.Where(p => p.Task == newTask.Id))
                {
                    PreviousTasks previousEntity = bdd.PreviousTasks.Find(previous.Id);
                    bdd.PreviousTasks.Remove(previousEntity);
                }

                SavePreviousTaskSet(previousTaskSetId, newTask.Id);
                bdd.SaveChanges();
                return newTask.Id;
            }

            return null;
        }

        #endregion

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
