using Leaf.DAL.ScaffoldedModels;
using System;
using System.Collections.Generic;

namespace Leaf.DAL
{
    public interface IDal : IDisposable
    {
        /*
         * Functions sorted by
         * 1 - Return type
         *  1a - Single return type or void
         *  2a - Collections
         * 2 - Name
         * 3 - Parameters
         */
        #region Single return-type

        /// <summary>
        /// Deletes a notification from a collaborator's list of notifications, returns true if the notification was found. Updates the database.
        /// </summary>
        /// <param name="c">Collaborator to update</param>
        /// <param name="n">Notification to delete</param>
        /// <returns>Returns true if the notification was found</returns>
        bool DeleteNotification(Collaborateurs c, Notification n);

        /// <summary>
        /// Gets an admin by its id or null if not found.
        /// </summary>
        /// <param name="id">Admin's id</param>
        /// <returns>The wanted admin, or null if not found.</returns>
        Admin GetAdmin(int id);

        Client GetClient(int id);

        /// <summary>
        /// Gets a collaborator by its id or null if not found.
        /// </summary>
        /// <param name="id">Collaborator's id</param>
        /// <returns>The wanted collaborator, or null if not found.</returns>
        Collaborateurs GetCollaborateurs(int id);
        /// <summary>
        /// Gets a collaborator by its e-mail or null if not found.
        /// </summary>
        /// <param name="id">Collaborator's e-mail</param>
        /// <returns>The wanted collaborator, or null if not found.</returns>
        Collaborateurs GetCollaborateurs(string email);

        /// <summary>
        /// Returns a project or null if the id doesn't correspond to an existing project.
        /// </summary>
        /// <param name="id">Project's id.</param>
        /// <returns>The wanted project or null if not found.</returns>
        Projet GetProjet(int id);

        /// <summary>
        /// Returns a task or null if the id doesn't correspond to an existing task.
        /// </summary>
        /// <param name="id">Task's id.</param>
        /// <returns>The wanted task or null if not found.</returns>
        Tache GetTache(int id);

        /// <summary>
        /// Modifies the database's state relative to this notifcation.
        /// </summary>
        /// <param name="n">Notification to mark as read</param>
        void ReadNotification(Notification n);
        /// <summary>
        /// Modifies the database's state relative to this notifcation.
        /// </summary>
        /// <param name="n">Notification to mark as unread</param>
        void UnreadNotification(Notification n);
        #endregion

        #region Collections
        /// <summary>
        /// Gets all the notifications of the collaborator, or an empty collection if he is not found.
        /// </summary>
        /// <param name="c">Collaborator whose notifcations are seeked.</param>
        /// <returns>Notifications of the collaborator or an empty collection.</returns>
        List<Notification> GetNotifications(Collaborateurs c);
        /// <summary>
        /// Gets the n last notifications of the collaborator, or an empty collection if he is not found.
        /// </summary>
        /// <param name="c">Collaborator whose notifcations are seeked.</param>
        /// <param name="n">Maximum number of notification to be retrieved</param>
        /// <returns>n last notifications of the collaborator or an empty collection.</returns>
        List<Notification> GetRecentNotifications(Collaborateurs c, int n);
        /// <summary>
        /// Gets all the projects to which a collaborator participates, might be an empty collection.
        /// </summary>
        /// <param name="collaborateur">Collaborator participating</param>
        /// <returns>Projects related to collaborator</returns>
        List<Projet> GetProjetByCollaborateur(Collaborateurs collaborateur);

        /// <summary>
        /// Gets all the tasks to which a collaborator participates, might be an empty collection.
        /// </summary>
        /// <param name="collaborateur">Collaborator participating</param>
        /// <returns>Tasks related to collaborator</returns>
        List<Tache> GetTaches(Collaborateurs c);

        #endregion

    }
}
