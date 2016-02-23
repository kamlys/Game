using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.Building
{
    public class BuildingViewModel
    {
        public int UserDolar { get; set; }
        public string[] productName { get; set; }
        public ItemBuildingViewModel viewModel { get; set; }
        public List<ItemBuildingViewModel> listModel { get; set; }
        public IPagedList<ItemBuildingViewModel> pagedModel { get; set; }

    }
}