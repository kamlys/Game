using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.Building.QueueBuilding
{
    public class QueueViewModel
    {
        public string[] allUser { get; set; }
        public ItemQueueViewModel viewModel { get; set; }
        public List<ItemQueueViewModel> listModel { get; set; }
    }
}