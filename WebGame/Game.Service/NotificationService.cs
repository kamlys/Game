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
        private IRepository<Messages> _message;
        private IRepository<Deals> _deal;
        private IRepository<Notifications> _notification;
        private IRepository<Users> _users;

        private IUnitOfWork _unitOfWork;

        public NotificationService(IRepository<Notifications> notification, IRepository<Users> users, IRepository<Messages> message, IRepository<Deals> deal, IUnitOfWork unitOfWork)
        {
            _notification = notification;
            _users = users;
            _message = message;
            _deal = deal;
            _unitOfWork = unitOfWork;
        }

        public void SentNotification(NotificationDto not)
        {
            int temp = 0;
            if (not.Description.Contains("zaproszenie"))
            {
                temp = _users.GetAll().First(i => i.Login == not.SenderLogin).ID;
            }
            else if (not.Description.Contains("wiadomość"))
            {
                int uID = _users.GetAll().First(i => i.Login == not.User_Login).ID;
                temp = _message.GetAll().OrderByDescending(i => i.ID).First(i => i.Customer_ID == uID).ID;
            }
            else if (not.Description.Contains("umowa"))
            {
                int uID = _users.GetAll().First(i => i.Login == not.User_Login).ID;
                temp = _deal.GetAll().OrderByDescending(i => i.ID).First(i => i.User2_ID == uID).ID;

            }

            _notification.Add(new Notifications
            {
                User_ID = _users.GetAll().First(i => i.Login == not.User_Login).ID,
                Temp_ID = temp,
                Description = not.Description,
                IfRead = false
            });
            _unitOfWork.Commit();
        }

        public List<NotificationDto> GetUserNotification(string User)
        {
            int uID = _users.GetAll().First(i => i.Login == User).ID;
            string senderLogin = String.Empty;
            List<NotificationDto> notificationList = new List<NotificationDto>();
            foreach (var item in _notification.GetAll().Where(i => i.User_ID == uID))
            {
                if (item.Description.Contains("zaproszenie"))
                {
                    senderLogin = _users.GetAll().First(i => i.ID == item.Temp_ID).Login;
                }
                else
                {
                    senderLogin = null;
                }
                notificationList.Add(new NotificationDto
                {
                    User_Login = _users.Get(item.User_ID).Login,
                    Description = item.Description,
                    ID = item.ID,
                    SenderLogin = senderLogin,
                    Temp_ID = item.Temp_ID
                });
            }

            return notificationList;
        }

        public bool RemoveNotification(int id, string User)
        {
            int uID = _users.GetAll().First(i => i.Login == User).ID;
            var notification = _notification.GetAll().Where(a => a.User_ID == uID && a.ID == id).First();
            if (notification != null)
            {
                _notification.Delete(notification);
                _unitOfWork.Commit();
                return true;
            }
            return false;
        }
    }
}
