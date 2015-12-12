using Game.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Service.Interfaces
{
    public interface INotificationService
    {
        void SentNotification(NotificationDto not);
        List<NotificationDto> GetUserNotification(string User);
        bool RemoveNotification(int id, string User);
    }
}
