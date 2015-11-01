using Game.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Game.GUI.ViewModels.Map
{
    public class MapViewModel
    {
        public MapDto Map;
        public List<UserBuildings.UserBuildingsViewModel> UserBuildings;
    }
}