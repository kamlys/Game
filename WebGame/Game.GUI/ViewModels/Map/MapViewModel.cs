using Game.Core.DTO;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Game.GUI.ViewModels.Map
{
    public class MapViewModel
    {
        public MapDto Map;
        public List<Building.UserBuildings.UserBuildingsViewModel> UserBuildings;
        public String UserProducts;
        public String ProductNames;
        public String BuildingsArray;

        public string[] allUser { get; set; }
        public ItemMapViewModel viewModel { get; set; }
        public List<ItemMapViewModel> listModel { get; set; }
    }
}