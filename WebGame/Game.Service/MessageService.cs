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
        private IUnitOfWork _unitOfWork;
         
        public MessageService(IRepository<Users> user, IUnitOfWork unitOfWork)
        {
            _user = user;
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
            throw new NotImplementedException();
        }
    }
}
