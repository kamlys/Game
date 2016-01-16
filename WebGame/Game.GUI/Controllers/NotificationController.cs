using Game.GUI.ViewModels;
using Game.GUI.ViewModels.Notification;
using Game.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Game.GUI.Controllers
{
    public class NotificationController : Controller
    {
        private INotificationService _notificationService;

        public NotificationController(INotificationService notifciationService)
        {
            _notificationService = notifciationService;
        }

        [Authorize]
        // GET: Notification
        public ActionResult _Notification()
        {
            NotificationViewModel notificationModel = new NotificationViewModel();

            notificationModel.listModel = _notificationService.GetUserNotification(User.Identity.Name).Select(x => new ItemNotificationViewModel
            {
                UserLogin = x.User_Login,
                Description = x.Description,
                Temp_ID = x.Temp_ID,
                ID = x.ID,
                SenderLogin = x.SenderLogin
            }).ToList();

            return View(notificationModel);
        }
    }
}