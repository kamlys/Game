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

        public ActionResult _NewMessage()
        {
            return View();
        }
        public ActionResult SentMessage(ListTableViewModel messageView)
        {
            MessageDto message = new MessageDto();

            message.Sender_Login = User.Identity.Name;
            message.Customer_Login = messageView.tableView.Login;
            message.Theme = messageView.tableView.Theme;
            message.Content = messageView.tableView.Content;

            _messageService.SentMessage(message);

            return RedirectToAction("Index");
        }

        public ActionResult SentMessageProfile(ListTableViewModel messageView)
        {
            MessageDto message = new MessageDto();

            message.Sender_Login = User.Identity.Name;
            message.Customer_Login = messageView.tableView.Login;
            message.Theme = messageView.tableView.Theme;
            message.Content = messageView.tableView.Content;

            _messageService.SentMessage(message);

            return RedirectToAction("Profil", "User", new { User = message.Customer_Login });
        }

        public ActionResult _SentMessage()
        {
            ListTableViewModel tableList = new ListTableViewModel();
            tableList.tableList = new List<TableViewModel>();

            foreach (var item in _messageService.GetSentMessage(User.Identity.Name))
            {
                tableList.tableList.Add(new TableViewModel
                {
                    ID = item.ID,
                    Customer_Login = item.Customer_Login,
                    Theme = item.Theme,
                    Content = item.Content,
                    PostDate = item.PostDate
                });
            }
            return View("~/Views/Message/_SentMessage.cshtml", tableList);
        }

        public ActionResult _ReceivedMessage()
        {
            ListTableViewModel tableList = new ListTableViewModel();
            tableList.tableList = new List<TableViewModel>();

            foreach (var item in _messageService.GetReceivedMessages(User.Identity.Name))
            {
                tableList.tableList.Add(new TableViewModel
                {
                    ID = item.ID,
                    Sender_Login = item.Sender_Login,
                    Theme = item.Theme,
                    Content = item.Content,
                    PostDate = item.PostDate,
                    IfRead = item.IfRead
                });
            }

            return View("~/Views/Message/_ReceivedMessage.cshtml", tableList);
        }

        public ActionResult Content(int MessageID)
        {
            ListTableViewModel tableList = new ListTableViewModel();
            tableList.tableView = new TableViewModel();

            var temp = _messageService.ConentMessage(MessageID, User.Identity.Name);

            tableList.tableView.Customer_Login = temp.Customer_Login;
            tableList.tableView.Sender_Login = temp.Sender_Login;
            tableList.tableView.Theme = temp.Theme;
            tableList.tableView.Content = temp.Content;
            tableList.tableView.PostDate = (DateTime)temp.PostDate;

            return View(tableList);
        }

    }
}