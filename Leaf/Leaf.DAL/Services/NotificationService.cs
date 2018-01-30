using Leaf.DAL.DTO;
using Leaf.DAL.Utilities.Translator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Leaf.DAL.Services
{
    public class NotificationService : BaseService
    {
        public NotificationService(ScaffoldedModels.LeafContext context) : base(context) { }

        public List<Notification> GetNotificationsByCollaborateurs(Collaborateurs pCollaborateurs)
        {
            var notifList = new List<Notification>();

            foreach (var notif in _context.Notification.Where(n => n.DestinataireNavigation.Id == pCollaborateurs.Id).ToList())
            {
                notifList.Add(NotificationTranslator.DalToDto(notif));
            }

            return notifList;
        }
    }
}
