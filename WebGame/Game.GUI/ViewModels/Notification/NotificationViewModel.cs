using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.Notification
{
    public class NotificationViewModel
    {
        public ItemNotificationViewModel viewModel { get; set; }
        public List<ItemNotificationViewModel> listModel { get; set; }
        public IPagedList pagedModel { get; set; }
    }
}