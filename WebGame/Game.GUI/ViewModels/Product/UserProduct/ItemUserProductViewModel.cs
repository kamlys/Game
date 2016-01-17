using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.Product.UserProduct
{
    public class ItemUserProductViewModel
    {
        public int ID { get; set; }
        public int User_ID { get; set; }
        public string User_Login { get; set; }
        public int Product_ID { get; set; }
        public string Product_Name { get; set; }
        public int Value { get; set; }
        public int Price { get; set; }
    }
}