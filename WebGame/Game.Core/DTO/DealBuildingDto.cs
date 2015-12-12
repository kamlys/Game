using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.DTO
{
    public class DealBuildingDto
    {
        public int ID { get; set; }
        public int User_ID { get; set; }
        public int Building_ID { get; set; }
        public string Building_Name { get; set; }
    }
}
