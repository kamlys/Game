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

        public void DeleteMessage(int id)
        {
            _message.Delete(_message.Get(id));
            _unitOfWork.Commit();
        }

        public void ReadMessage(MessageDto message)
        {
            throw new NotImplementedException();
        }

        public bool SentMessage(MessageDto message)
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

        public List<MessageDto> GetSentMessage(string User)
        {
            List<MessageDto> message = new List<MessageDto>();
            int uID = _user.GetAll().First(i => i.Login == User).ID;
            foreach (var item in _message.GetAll().Where(i => i.Sender_ID == uID))
            {
                message.Add(new MessageDto
                {
                    ID = item.ID,
                    Customer_Login = _user.GetAll().First(i => i.ID == item.Customer_ID).Login,
                    Theme = item.Theme,
                    Content = item.Content,
                    PostDate = (DateTime)item.PostDate,
                    IfRead = item.IfRead
                });
            }

            return message.OrderByDescending(i => i.PostDate).ToList();
        }

        public List<MessageDto> GetReceivedMessages(string User)
        {
            List<MessageDto> message = new List<MessageDto>();
            int uID = _user.GetAll().First(i => i.Login == User).ID;
            foreach (var item in _message.GetAll().Where(i => i.Customer_ID == uID))
            {
                message.Add(new MessageDto
                {
                    ID = item.ID,
                    Sender_Login = _user.GetAll().First(i => i.ID == item.Sender_ID).Login,
                    Theme = item.Theme,
                    Content = item.Content,
                    PostDate = (DateTime)item.PostDate,
                    IfRead = item.IfRead
                });
            }

            return message.OrderByDescending(i=> i.PostDate).ToList();
        }

        public MessageDto ConentMessage(int ID, string User)
        {
            MessageDto message = new MessageDto();

            foreach (var item in _message.GetAll().Where(i=> i.ID == ID))
            {
                message.ID = item.ID;
                message.Customer_Login = _user.Get(item.Customer_ID).Login;
                message.Sender_Login = _user.Get(item.Sender_ID).Login;
                message.Theme = item.Theme;
                message.Content = item.Content;
                message.PostDate = (DateTime)item.PostDate;

                if(item.Customer_ID == _user.GetAll().First(i=> i.Login == User).ID)
                {
                    item.IfRead = true;
                    _unitOfWork.Commit();
                }
            }

            return message;
        }
    }
}
