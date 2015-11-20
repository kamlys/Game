using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.DTO
{
    public class BuildingQueueDto
    {
        public int ID { get; set; }
        public int User_ID { get; set; }
        public string Login { get; set; }
        public int UserBuilding_ID { get; set; }
        public string BuildingName { get; set; }
        public DateTime FinishTime { get; set; }
        public string NewStatus { get; set; }


    }
}
