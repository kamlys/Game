using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.DTO
{
    public class MessageDto
    {
        public int ID { get; set; }
        public int Sender_ID { get; set; }
        public string Sender_Login { get; set; }
        public int Customer_ID { get; set; }
        public string Customer_Login { get; set; }
        public string Theme { get; set; }
        public string Content { get; set; }
        public DateTime PostDate { get; set; }
        public bool IfRead { get; set; }
    }
}
