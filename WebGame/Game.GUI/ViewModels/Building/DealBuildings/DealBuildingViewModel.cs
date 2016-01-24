using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.Building.DealBuildings
{
    public class DealBuildingViewModel
    {
        public string[] allBuilding { get; set; }
        public string[] allUser { get; set; }
        public ItemDealBuildingViewModel viewModel { get; set; }
        public List<ItemDealBuildingViewModel> listModel { get; set; }

    }
}