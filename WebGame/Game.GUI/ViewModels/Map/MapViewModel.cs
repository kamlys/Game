﻿using Game.Core.DTO;
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
        public List<UserBuildings.UserBuildingsViewModel> UserBuildings;
        public String UserProducts;
        public String ProductNames;
        public String BuildingsArray;
    }
}