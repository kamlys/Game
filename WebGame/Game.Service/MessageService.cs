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

            return message.OrderByDescending(i => i.PostDate).ToList();
        }

        public MessageDto ConentMessage(int ID, string User)
        {
            MessageDto message = new MessageDto();

            foreach (var item in _message.GetAll().Where(i => i.ID == ID))
            {
                message.ID = item.ID;
                message.Customer_Login = _user.Get(item.Customer_ID).Login;
                message.Sender_Login = _user.Get(item.Sender_ID).Login;
                message.Theme = item.Theme;
                message.Content = item.Content;
                message.PostDate = (DateTime)item.PostDate;

                if (item.Customer_ID == _user.GetAll().First(i => i.Login == User).ID)
                {
                    item.IfRead = true;
                    _unitOfWork.Commit();
                }
            }

            return message;
        }

        public List<MessageDto> GetMessage()
        {
            List<MessageDto> messageDto = new List<MessageDto>();

            foreach (var item in _message.GetAll())
            {
                messageDto.Add(new MessageDto
                {
                    Content = item.Content,
                    Customer_ID = item.Customer_ID,
                    Customer_Login = _user.GetAll().First(i => i.ID == item.Customer_ID).Login,
                    ID = item.ID,
                    IfRead = item.IfRead,
                    PostDate = item.PostDate,
                    Sender_ID = item.Sender_ID,
                    Sender_Login = _user.GetAll().First(i => i.ID == item.Sender_ID).Login,
                    Theme = item.Theme
                });
            }

            return messageDto;
        }

        public bool AddMessageAdmin(MessageDto messageDto)
        {
            if (_user.GetAll().Any(i => i.Login == messageDto.Customer_Login)
                && (_user.GetAll().Any(i => i.Login == messageDto.Sender_Login))
                && messageDto.Theme.Length <= 150)
            {
                _message.Add(new Messages
                {
                    Theme = messageDto.Theme,
                    Content = messageDto.Content,
                    Customer_ID = _user.GetAll().First(i => i.Login == messageDto.Customer_Login).ID,
                    IfRead = messageDto.IfRead,
                    PostDate = DateTime.Now,
                    Sender_ID = _user.GetAll().First(i => i.Login == messageDto.Sender_Login).ID
                });

                _unitOfWork.Commit();
                return true;
            }
            return false;
        }

        public bool UpdateMessageAdmin(MessageDto messageDto)
        {
            if (_user.GetAll().Any(i => i.Login == messageDto.Customer_Login)
                && (_user.GetAll().Any(i => i.Login == messageDto.Sender_Login))
               && messageDto.Theme.Length <= 150)
            {
                foreach (var item in _message.GetAll().Where(i => i.ID == messageDto.ID))
                {
                    item.IfRead = messageDto.IfRead;
                    item.Sender_ID = _user.GetAll().First(i => i.Login == messageDto.Sender_Login).ID;
                    item.Content = messageDto.Content;
                    item.Customer_ID = _user.GetAll().First(i => i.Login == messageDto.Customer_Login).ID;
                    item.Theme = messageDto.Theme;
                    item.PostDate = messageDto.PostDate;
                }

                _unitOfWork.Commit();
                return true;
            }
            return false;
        }

        public bool DeleteMessageAdmin(int id)
        {
            try
            {
                _message.Delete(_message.Get(id));
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }
    }
}
