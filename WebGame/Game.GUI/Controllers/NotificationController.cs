using Game.GUI.ViewModels;
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

        // GET: Notification
        public ActionResult _Notification()
        {
            ListTableViewModel tableList = new ListTableViewModel();
            tableList.tableList = new List<TableViewModel>();

            foreach (var item in _notificationService.GetUserNotification(User.Identity.Name))
            {
                tableList.tableList.Add(new TableViewModel
                {
                    Description = item.Description
                });
            }

            return View(tableList);
        }
    }
}