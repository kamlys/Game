using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.User
{
    public class ItemUserViewModel
    {
        public int ID { get; set; }
        public string User_Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime LastUpdate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastLog { get; set; }
    }
}