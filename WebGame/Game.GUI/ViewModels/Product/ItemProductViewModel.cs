using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.Product
{
    public class ItemProductViewModel
    {
        public int ID { get; set; }
        public string ProductName { get; set; }
        public int Price_per_unit { get; set; }
        public string Unit { get; set; }
        public string Alias { get; set; }
    }
}