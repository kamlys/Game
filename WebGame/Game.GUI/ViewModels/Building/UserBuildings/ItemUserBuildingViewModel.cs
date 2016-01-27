using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.Building.UserBuildings
{
    public class ItemUserBuildingViewModel
    {
        public int ID { get; set; }
        public int User_ID { get; set; }
        public string User_Login { get; set; }
        public int X_pos { get; set; }
        public int Y_pos { get; set; }
        public int Lvl { get; set; }
        public int Building_ID { get; set; }
        public string Building_Name { get; set; }
        public string Status { get; set; }
        public int Produkcja { get; set; }
        public bool ifCan { get; set; }
        public int Percent_price_per_lvl { get; set; }
        public int Percent_product_per_lvl { get; set; }
        public string Color { get; set; }
        public bool Stock { get; set; }
    }
}