using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Leaf.DAL.Utilities.Translator
{
    public class NotificationTranslator
    {
        public static DTO.Notification DalToDto(ScaffoldedModels.Notification pNotification)
        {
            return new DTO.Notification
            {
                Destinataire = pNotification.Destinataire,
                //DestinataireNavigation = CollaborateursTranslator.DalToDto(pNotification.DestinataireNavigation),
                Horodatage = pNotification.Horodatage,
                Id = pNotification.Id,
                Lue = pNotification.Lue,
                Message = pNotification.Message
            };
        }

        public static ICollection<DTO.Notification> DalToDto(ICollection<ScaffoldedModels.Notification> pNotification)
        {
            var notifList = new Collection<DTO.Notification>();
            foreach(var notif in pNotification)
            {
                notifList.Add(DalToDto(notif));
            }

            return notifList;
        }
    }
}
