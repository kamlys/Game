using Game.GUI.ViewModels.Building;
using Game.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Game.GUI.Controllers
{
    public class AjaxController : Controller
    {
        private IMapService _mapService;
        private IUserBuildingService _userBuildingsService;
        private IBuildingHelper _buildingsService;

        public AjaxController(IMapService mapService, IUserBuildingService userBuildingService, IBuildingHelper buildings)
        {
            _mapService = mapService;
            _userBuildingsService = userBuildingService;
            _buildingsService = buildings;
        }
        [HttpPost]
        public JsonResult Build(AjaxBuildViewModel a)
        {
            _userBuildingsService.Build(a.Id, a.Col, a.Row, User.Identity.Name);
            return new JsonResult { Data = true };
        }
    }
}