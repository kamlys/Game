using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.Building.QueueBuilding
{
    public class ItemQueueViewModel
    {
        public int ID { get; set; }
        public int User_ID { get; set; }
        public string User_Login { get; set; }
        public int UserBuilding_ID { get; set; }
        public string BuildingName { get; set; }
        public DateTime FinishDate { get; set; }
        public string NewStatus { get; set; }
        public int Second { get; set; }
    }
}