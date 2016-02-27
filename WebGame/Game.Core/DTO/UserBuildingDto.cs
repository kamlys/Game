using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core.DTO
{
    public class UserBuildingDto
    {
        public int ID { get; set; }
        public int User_ID { get; set; }
        public string Login { get; set; }
        public int X_pos { get; set; }
        public int Y_pos { get; set; }
        public int Lvl { get; set; }
        public int Building_ID { get; set; }
        public string Building_Name { get; set; }
        public string Status { get; set; }
        public int Produkcja { get; set; }
        public int ProdukcjaLvlUp { get; set; }
        public int PriceLvlUp { get; set; }
        public bool Owner { get; set; }
        public int Percent_Product { get; set; }
        public string Color { get; set; }
        public bool Stock { get; set; }
        public DateTime DateOfConstruction { get; set; }
        public int DestPrice { get; set; }
        public bool CanDelete { get; set; }
        public int? DealID { get; set; }
    }
}
