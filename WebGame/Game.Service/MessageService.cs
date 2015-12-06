using Game.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Core.DTO;
using Game.Dal.Repository;
using Game.Dal.Model;
using Game.Dal;

namespace Game.Service
{
    public class MessageService : IMessageService
    {
        private IRepository<Users> _user;
        private IRepository<Messages> _message;
        private IUnitOfWork _unitOfWork;
         
        public MessageService(IRepository<Users> user, IRepository<Messages> message, IUnitOfWork unitOfWork)
        {
            _user = user;
            _message = message;
            _unitOfWork = unitOfWork;
        }

        public bool DeleteMessage(MessageDto message)
        {
            throw new NotImplementedException();
        }

        public void ReadMessage(MessageDto message)
        {
            throw new NotImplementedException();
        }

        public bool SendMessage(MessageDto message)
        {
            try
            {
                _message.Add(new Messages
                {
                    Customer_ID = _user.GetAll().First(i => i.Login == message.Customer_Login).ID,
                    Sender_ID = _user.GetAll().First(i => i.Login == message.Sender_Login).ID,
                    Theme = message.Theme,
                    Content = message.Content,
                    PostDate = DateTime.Now,
                    IfRead = false
                });
                _unitOfWork.Commit();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<MessageDto> GetPostMessage(string User)
        {
            List<MessageDto> message = new List<MessageDto>();
            int uID = _user.GetAll().First(i => i.Login == User).ID;
            foreach (var item in _message.GetAll().Where(i=> i.Sender_ID == uID))
            {
                message.Add(new MessageDto
                {
                    Customer_Login = _user.GetAll().First(i => i.ID == item.Sender_ID).Login,
                    Theme = item.Theme,
                    Content = item.Content,
                    PostDate = (DateTime)item.PostDate
                });
            }

            return message;
        }

        public List<MessageDto> GetReceivedMessages(string User)
        {
            List<MessageDto> message = new List<MessageDto>();
            int uID = _user.GetAll().First(i => i.Login == User).ID;
            foreach (var item in _message.GetAll().Where(i => i.Customer_ID == uID))
            {
                message.Add(new MessageDto
                {
                    Sender_Login = _user.GetAll().First(i => i.ID == item.Sender_ID).Login,
                    Theme = item.Theme,
                    Content = item.Content,
                    PostDate = (DateTime)item.PostDate
                });
            }

            return message;
        }
    }
}
