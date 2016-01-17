using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.DTO
{
    public class NotificationDto
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string User_Login { get; set; }
        public string SenderLogin { get; set; }
        public int Temp_ID { get; set; }
        public string Description { get; set; }
        public bool IfRead { get; set; }
    }
}
