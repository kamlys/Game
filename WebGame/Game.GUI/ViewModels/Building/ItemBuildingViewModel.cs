﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.Building
{
    public class ItemBuildingViewModel
    {
        public int ID { get; set; }
        public string BuildingName { get; set; }
        public int Price { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int DealID { get; set; }
        public int DestPrice { get; set; }
        public int Percent_price_per_lvl { get; set; }
        public int Percent_product_per_lvl { get; set; }
        public int Product_ID { get; set; }
        public string Product_Name { get; set; }
        public int Product_per_sec { get; set; }
        public string Alias { get; set; }
        public int BuildingTime { get; set; }
        public int DestructionTime { get; set; }
        public bool buildingDiv { get; set; }
        public string Stock { get; set; }

    }
}