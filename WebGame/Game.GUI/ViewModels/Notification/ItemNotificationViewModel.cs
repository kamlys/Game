using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.Notification
{
    public class ItemNotificationViewModel
    {
        public int ID { get; set; }
        public string UserLogin { get; set; }
        public string SenderLogin { get; set; }
        public int Temp_ID { get; set; }
        public string Description { get; set; }
    }
}