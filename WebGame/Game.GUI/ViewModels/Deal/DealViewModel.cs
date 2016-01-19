using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.Deal
{
    public class DealViewModel
    {
        public string[] userList { get; set; }
        public string[] buildingList { get; set; }
        public ItemDealViewModel viewModel { get; set; }
        public List<ItemDealViewModel> listModel { get; set; }
        public IPagedList<ItemDealViewModel> pagedList { get; set; }
    }
}