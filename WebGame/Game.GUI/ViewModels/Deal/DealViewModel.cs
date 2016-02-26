using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Game.GUI.ViewModels.Deal
{
    public class DealViewModel
    {
        public string[] userList { get; set; }
        public string[] buildingList { get; set; }
        public List<SelectListItem> buildingList2 { get; set; }
        public ItemDealViewModel viewModel { get; set; }
        public List<ItemDealViewModel> listModel { get; set; }
        public IPagedList<ItemDealViewModel> pagedList { get; set; }
    }
}