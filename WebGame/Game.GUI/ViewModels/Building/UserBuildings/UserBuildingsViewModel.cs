using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.Building.UserBuildings
{
    public class UserBuildingsViewModel
    {
        public int x_left;
        public int x_right;
        public int y_bottom;
        public int y_top;
        public string name;
        public int level;
        public int BuildingID;
        public int ID;
        public int BuildTime;
        public string Status;
        public int BuildDone;
        public string Alias { get; set; }

        public ItemUserBuildingViewModel viewModel { get; set; }
        public List<ItemUserBuildingViewModel> listModel { get; set; }
    }
}