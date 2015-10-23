using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.DTO
{
    public class UserDto
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime Last_Log { get; set; }
        public DateTime Registration_Date { get; set; }
        public DateTime Last_Update { get; set; }
    }
}
