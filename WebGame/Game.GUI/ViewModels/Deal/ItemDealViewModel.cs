using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.Deal
{
    public class ItemDealViewModel
    {
        public int ID { get; set; }
        public string User1_Login { get; set; }
        public string User2_Login { get; set; }
        public int Percent_User1 { get; set; }
        public int Percent_User2 { get; set; }
        public string Building_Name { get; set; }
        public bool IsActive { get; set; }
        public string Active { get; set; }
        public bool Owner { get; set; }
        public int Value { get; set; }
        public int DealDay { get; set; }
    }
}