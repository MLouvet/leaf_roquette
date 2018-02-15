using Leaf.DAL.ScaffoldedModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Leaf.DAL.Utilities.Translator
{
    public class NotificationTranslator
    {
        public static Notification DalToDto(ScaffoldedModels.Notification pNotification)
        {
            return new Notification
            {
                Destinataire = pNotification.Destinataire,
                //DestinataireNavigation = CollaborateursTranslator.DalToDto(pNotification.DestinataireNavigation),
                Horodatage = pNotification.Horodatage,
                Id = pNotification.Id,
                Lue = pNotification.Lue,
                Message = pNotification.Message,
                //Tâche = pNotification.TacheNavigation[pNotification.IdTache];
            };
        }

        public static ICollection<Notification> DalToDto(ICollection<ScaffoldedModels.Notification> pNotification)
        {
            var notifList = new Collection<Notification>();
            foreach(var notif in pNotification)
            {
                notifList.Add(DalToDto(notif));
            }

            return notifList;
        }
    }
}
