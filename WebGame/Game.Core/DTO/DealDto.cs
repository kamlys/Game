using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.DTO
{
    public class DealDto
    {
        public int ID { get; set; }
        public int User1_ID { get; set; }
        public string User1_Login { get; set; }
        public int User2_ID { get; set; }
        public string User2_Login { get; set; }
        public int Building_ID { get; set; }
        public string Building_Name { get; set; }
        public int Map_ID { get; set; }
        public string Product_Name { get; set; }
        public int Percent_User1 { get; set; }
        public int Percent_User2 { get; set; }
        public DateTime? FinishDate { get; set; }
        public int DayTime { get; set; }
        public bool IsActive { get; set; }
        public bool MyMap { get; set; }
        public string OwnerLogin { get; set; }

    }
}
