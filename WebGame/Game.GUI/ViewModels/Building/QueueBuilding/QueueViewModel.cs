using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.Building.QueueBuilding
{
    public class QueueViewModel
    {
        public ItemQueueViewModel viewModel { get; set; }
        public IPagedList<ItemQueueViewModel> listModel { get; set; }
    }
}