using Game.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Service.Interfaces
{
    public interface IMessageService
    {
        bool SendMessage(MessageDto message);
        void ReadMessage(MessageDto message);
        void DeleteMessage(int id);
        MessageDto ConentMessage(int ID, string User);
        List<MessageDto> GetSentMessage(string User);
        List<MessageDto> GetReceivedMessages(string User);
        List<MessageDto> GetMessage();
        bool AddMessageAdmin(MessageDto messageDto);
        bool UpdateMessageAdmin(MessageDto messageDto);
        bool DeleteMessageAdmin(int id);


    }
}
