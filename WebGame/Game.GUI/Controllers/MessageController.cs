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

            return View("~/Views/Message/Index.cshtml",tableList);
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
                    Customer_Login = item.Customer_Login,
                    Theme = item.Theme,
                    Content = item.Content,
                    PostDate = item.PostDate
                });
            }

            return View("~/Views/Message/Index.cshtml", tableList);
        }

    }
}