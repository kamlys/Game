using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.User.Friend
{
    public class ItemFriendViewModel
    {
        public int ID { get; set; }
        public int User_ID { get; set; }
        public string User_Login { get; set; }
        public int Friend_ID { get; set; }
        public string Friend_Login { get; set; }
        public bool OrAccepted { get; set; }
        public string Theme { get; set; }
        public string Content { get; set; }
    }
}