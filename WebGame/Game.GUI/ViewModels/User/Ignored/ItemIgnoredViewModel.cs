using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.User.Ignored
{
    public class ItemIgnoredViewModel
    {
        public int ID { get; set; }
        public int User_ID { get; set; }
        public string User_Login { get; set; }
        public int Ignored_ID { get; set; }
        public string Ignored_Login { get; set; }
    }
}