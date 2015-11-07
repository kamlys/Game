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
        private IBuildingHelper _buildingsHelper;
        private IProductService _productService;

        public AjaxController(IMapService mapService, IUserBuildingService userBuildingService, IProductService productService, IBuildingHelper buildingsHelper)
        {
            _mapService = mapService;
            _userBuildingsService = userBuildingService;
            _buildingsHelper = buildingsHelper;
            _productService = productService;
        }
        [HttpPost]
        public JsonResult Build(AjaxBuildViewModel a)
        {
            if (_buildingsHelper.BuildingValidation(a.Id, a.Col, a.Row, User.Identity.Name))
            {
                ProductUpdate();
                _userBuildingsService.Build(a.Id, a.Col, a.Row, User.Identity.Name);
                return new JsonResult { Data = true };
            }
            else
            {
                return new JsonResult { Data = false };
            }
        }

        [HttpPost]
        public JsonResult Destroy(AjaxBuildViewModel a)
        {
            ShowProduct();
            _userBuildingsService.Destroy(User.Identity.Name, a.Id);
            return new JsonResult { Data = true };
        }

        [HttpPost]
        public void ProductUpdate()
        {
            _productService.UpdateUserProduct(User.Identity.Name);
        }

        public JsonResult ShowProduct()
        {
            ProductUpdate();
            int[][] addProduct = _buildingsHelper.AddProductValue(User.Identity.Name);

            return Json(addProduct, JsonRequestBehavior.AllowGet);
        }
    }
}