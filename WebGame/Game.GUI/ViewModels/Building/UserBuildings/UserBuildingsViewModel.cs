﻿using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.Building.UserBuildings
{
    public class UserBuildingsViewModel
    {
        public int id;
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
        public string Color;
        public int DestPrice;
        public bool CanDelete;
        public string Alias { get; set; }

        public string[] allProduct { get; set; }
        public string[] allBuilding { get; set; }
        public string[] allUser { get; set; }

        public ItemUserBuildingViewModel viewModel { get; set; }
        public List<ItemUserBuildingViewModel> listModel { get; set; }
        public IPagedList<ItemUserBuildingViewModel> pagedList { get; set; }

    }
}