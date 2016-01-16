using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.Market
{
    public class ItemMarketViewModel
    {
        public int ID { get; set; }
        public int User_ID { get; set; }
        public string User_Login { get; set; }
        public int Product_ID { get; set; }
        public string Product_Name { get; set; }
        public int Number { get; set; }
        public int Price { get; set; }
        public bool TypeOffer { get; set; }
    }
}