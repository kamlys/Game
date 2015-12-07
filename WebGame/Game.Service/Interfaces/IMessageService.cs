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
        bool SentMessage(MessageDto message);
        void ReadMessage(MessageDto message);
        bool DeleteMessage(MessageDto message);
        List<MessageDto> GetSentMessage(string User);
        List<MessageDto> GetReceivedMessages(string User);
    }
}
