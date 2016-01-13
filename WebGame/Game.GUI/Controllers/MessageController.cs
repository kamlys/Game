using Game.Core.DTO;
using Game.GUI.ViewModels;
using Game.Service.Interfaces;
using PagedList;
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
        private INotificationService _notificationService;

        public MessageController(IMessageService messageService, INotificationService notificationSerwice)
        {
            _messageService = messageService;
            _notificationService = notificationSerwice;
        }

        // GET: Message
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult _NewMessage()
        {
            return View();
        }

        [Authorize]
        public ActionResult SentMessage(ListTableViewModel messageView)
        {
            List<string> errors;
            if (Session["val"] != null)
            {
                errors = ((string[])Session["val"]).ToList();
            }
            else
            {
                errors = new List<string>();
            }

            MessageDto message = new MessageDto();

            message.Sender_Login = User.Identity.Name;
            message.Customer_Login = messageView.tableView.Customer_Login;
            message.Theme = messageView.tableView.Theme;
            message.Content = messageView.tableView.Content;

            if (_messageService.SentMessage(message))
            {
                _notificationService.SentNotification(new NotificationDto
                {
                    User_Login = message.Customer_Login,
                    Description = "Nowa wiadomość",
                });
            }
            else
            {
                errors.Add("Błąd wysyłania. Spróbuj ponownie.");
                Session["val"] = errors.ToArray<string>();
            }


            return RedirectToAction("Index");

        }

        [Authorize]
        public ActionResult SentMessageProfile(ListTableViewModel messageView)
        {
            MessageDto message = new MessageDto();

            message.Sender_Login = User.Identity.Name;
            message.Customer_Login = messageView.tableView.Login;
            message.Theme = messageView.tableView.Theme;
            message.Content = messageView.tableView.Content;

            _messageService.SentMessage(message);
            _notificationService.SentNotification(new NotificationDto
            {
                User_Login = message.Customer_Login,
                Description = "Nowa wiadomość",
            });
            return RedirectToAction("Profil", "User", new { User = message.Customer_Login });
        }

        [Authorize]
        public ActionResult _SentMessage(int? Page_No)
        {
            List<TableViewModel> tableList = new List<TableViewModel>();
            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);

            foreach (var item in _messageService.GetSentMessage(User.Identity.Name))
            {
                tableList.Add(new TableViewModel
                {
                    ID = item.ID,
                    Customer_Login = item.Customer_Login,
                    Theme = item.Theme,
                    Content = item.Content,
                    PostDate = item.PostDate
                });
            }
            return View("~/Views/Message/_SentMessage.cshtml", tableList.ToPagedList(No_Of_Page, Size_Of_Page));
        }

        [Authorize]
        public ActionResult _ReceivedMessage(int? Page_No)
        {
            List<TableViewModel> tableList = new List<TableViewModel>();
            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);

            foreach (var item in _messageService.GetReceivedMessages(User.Identity.Name))
            {
                tableList.Add(new TableViewModel
                {
                    ID = item.ID,
                    Sender_Login = item.Sender_Login,
                    Theme = item.Theme,
                    Content = item.Content,
                    PostDate = item.PostDate,
                    IfRead = item.IfRead
                });
            }

            return View("~/Views/Message/_ReceivedMessage.cshtml", tableList.ToPagedList(No_Of_Page, Size_Of_Page));



            //ListTableViewModel tableList = new ListTableViewModel();
            //tableList.tableList = new List<TableViewModel>();

            //foreach (var item in _messageService.GetReceivedMessages(User.Identity.Name))
            //{
            //    tableList.tableList.Add(new TableViewModel
            //    {
            //        ID = item.ID,
            //        Sender_Login = item.Sender_Login,
            //        Theme = item.Theme,
            //        Content = item.Content,
            //        PostDate = item.PostDate,
            //        IfRead = item.IfRead
            //    });
            //}

            //return View("~/Views/Message/_ReceivedMessage.cshtml", tableList);
        }

        [Authorize]
        public ActionResult Content(int MessageID)
        {
            ListTableViewModel tableList = new ListTableViewModel();
            tableList.tableView = new TableViewModel();

            var temp = _messageService.ConentMessage(MessageID, User.Identity.Name);

            tableList.tableView.ID = temp.ID;
            tableList.tableView.Customer_Login = temp.Customer_Login;
            tableList.tableView.Sender_Login = temp.Sender_Login;
            tableList.tableView.Theme = temp.Theme;
            tableList.tableView.Content = temp.Content;
            tableList.tableView.PostDate = (DateTime)temp.PostDate;

            return View(tableList);
        }

        [Authorize]
        public void DeleteMessage(int a)
       {
            _messageService.DeleteMessage(a);
        }


    }
}