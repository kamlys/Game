using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.Building.DealBuildings
{
    public class ItemDealBuildingViewModel
    {
        public int ID { get; set; }
        public int User_ID { get; set; }
        public string User_Login { get; set; }
        public int Building_ID { get; set; }
        public string Building_Name { get; set; }
        public int Deal_ID { get; set; }
    }
}