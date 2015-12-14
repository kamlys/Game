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
        private INotificationService _notificationService;

        public AjaxController(IMapService mapService, IUserBuildingService userBuildingService, IProductService productService, IBuildingHelper buildingsHelper, INotificationService notificationService)
        {
            _mapService = mapService;
            _userBuildingsService = userBuildingService;
            _buildingsHelper = buildingsHelper;
            _productService = productService;
            _notificationService = notificationService;
        }
        [HttpPost]
        [Authorize]
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
        [Authorize]
        public JsonResult Destroy(AjaxBuildViewModel a)
        {
            ShowProduct();
            _userBuildingsService.Destroy(User.Identity.Name, a.Id);
            return new JsonResult { Data = true };
        }

        [HttpPost]
        [Authorize]
        public void ProductUpdate()
        {
            _productService.UpdateUserProduct(User.Identity.Name);
        }

        [Authorize]
        public void RemoveNotification(String id)
        {
            _notificationService.RemoveNotification(Int32.Parse(id), User.Identity.Name);
        }

        [Authorize]
        public string[][] GetNotifications()
        {
            var notifs = _notificationService.GetUserNotification(User.Identity.Name);
            var ret = new List<string[]>();
            foreach(var n in notifs)
            {
                ret.Add(new string [2]{ n.ID.ToString(), n.Description });
            }

            return ret.ToArray();
        }

        [Authorize]
        public JsonResult ShowProduct()
        {
            ProductUpdate();
            int[][] addProduct = _buildingsHelper.AddProductValue(User.Identity.Name);

            return Json(addProduct, JsonRequestBehavior.AllowGet);
        }
    }
}