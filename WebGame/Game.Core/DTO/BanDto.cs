using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.DTO
{
    public class BanDto
    {
        public int ID { get; set; }

        public int User_ID { get; set; }
        public string Login { get; set; }

        public string Description { get; set; }

        public DateTime Start_Date { get; set; }

        public DateTime Finish_Date { get; set; }
    }
}
