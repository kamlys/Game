using Game.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels
{
    public class TableViewModel
    {
        public int ID { get; set; }
        public int User_ID { get; set; }
        public string Description { get; set; }
        public DateTime Start_Date { get; set; }
        public DateTime Finish_Date { get; set; }
        public int UserBuilding_ID { get; set; }
        public string NewStatus { get; set; }
        public string Name { get; set; }
        public string Product_Name { get; set; }
        public int Price { get; set; }
        public int Percent_price_per_lvl { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Product_per_sec { get; set; }
        public int Dest_price { get; set; }
        public int Percent_product_per_lvl { get; set; }
        public int Product_ID { get; set; }
        public string Alias { get; set; }
        public int BuildingTime { get; set; }
        public int DestructionTime { get; set; }
        public int Value { get; set; }
        public int Price_per_unit { get; set; }
        public string Unit { get; set; }
        public int X_pos { get; set; }
        public int Y_pos { get; set; }
        public int Lvl { get; set; }
        public int Building_ID { get; set; }
        public string Status { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime Last_Log { get; set; }
        public DateTime Registration_Date { get; set; }
        public DateTime Last_Update { get; set; }

    }
}