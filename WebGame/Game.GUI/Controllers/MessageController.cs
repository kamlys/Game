using Game.Core.DTO;
using Game.GUI.ViewModels;
using Game.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Game.GUI.Controllers
{
    public class MessageController : Controller
    {
        private IMessageService _messageService;


        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        // GET: Message
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SendMessage(ListTableViewModel messageView)
        {
            MessageDto message = new MessageDto();

            message.Sender_Login = User.Identity.Name;
            message.Customer_Login = messageView.tableView.Login;
            message.Theme = messageView.tableView.Theme;
            message.Content = messageView.tableView.Content;

            _messageService.SendMessage(message);

            return RedirectToAction("Index");
        }

    }
}