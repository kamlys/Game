using Game.Core.DTO;
using Game.Dal;
using Game.Dal.Model;
using Game.Dal.Repository;
using Game.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Service
{
    public class NotificationService : INotificationService
    {
        private IRepository<Notifications> _notification;
        private IRepository<Users> _users;

        private IUnitOfWork _unitOfWork;

        public NotificationService(IRepository<Notifications> notification, IRepository<Users> users, IUnitOfWork unitOfWork)
        {
            _notification = notification;
            _users = users;
            _unitOfWork = unitOfWork;
        }

        public void SentNotification(NotificationDto not)
        {
            _notification.Add(new Notifications
            {
                User_ID = _users.GetAll().First(i => i.Login == not.User_Login).ID,
                Description = not.Description,
                IfRead = false
            });
            _unitOfWork.Commit();
        }
    }
}
