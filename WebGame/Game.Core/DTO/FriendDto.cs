using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.DTO
{
    public class FriendDto
    {
        public int ID { get; set; }
        public int User_ID { get; set; }
        public string User_Login { get; set; }
        public int Friend_ID { get; set; }
        public string Friend_Login { get; set; }
        public bool OrAccepted { get; set; }
    }
}
