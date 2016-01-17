using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.DTO
{
    public class DealBuildingDto : BuildingDto
    {
        public int DealBuilding_ID { get; set; }
        public int User_ID { get; set; }
        public int Building_ID { get; set; }
        public int Deal_ID { get; set; }
    }
}
