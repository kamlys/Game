﻿using Game.Core.DTO;
using Game.GUI.ViewModels;
using Game.GUI.ViewModels.Message;
using Game.GUI.ViewModels.User;
using Game.GUI.ViewModels.User.Friend;
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
        public ActionResult SendMessage(MessageViewModel messageModel)
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
            message.Customer_Login = messageModel.viewModel.Customer_Login;
            message.Theme = messageModel.viewModel.Theme;
            message.Content = messageModel.viewModel.Content;

            if (_messageService.SentMessage(message))
            {
                _notificationService.SentNotification(new NotificationDto
                {
                    SenderLogin = User.Identity.Name,
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
        public ActionResult SendMessageProfile(ProfileViewModel profilViewModel)
        {
            MessageDto message = new MessageDto();

            message.Sender_Login = User.Identity.Name;
            message.Customer_Login = profilViewModel.Login;
            message.Theme = profilViewModel.Theme;
            message.Content = profilViewModel.Content;

            _messageService.SentMessage(message);
            _notificationService.SentNotification(new NotificationDto
            {
                User_Login = message.Customer_Login,
                Description = "Nowa wiadomość",
            });
            return RedirectToAction("Profil", "User", new { User = message.Customer_Login });
        }

        [Authorize]
        public ActionResult SendFriendMessage(FriendViewModel friendViewModel)
        {
            MessageDto message = new MessageDto();

            message.Sender_Login = User.Identity.Name;
            message.Customer_Login = friendViewModel.viewModel.Friend_Login;
            message.Theme = friendViewModel.viewModel.Theme;
            message.Content = friendViewModel.viewModel.Content;

            _messageService.SentMessage(message);
            _notificationService.SentNotification(new NotificationDto
            {
                User_Login = message.Customer_Login,
                Description = "Nowa wiadomość",
            });
            return View("~/Views/Home/Index.cshtml");
        }

        [Authorize]
        public ActionResult _SentMessage(int? Page_No)
        {
            MessageViewModel messageModel = new MessageViewModel();
            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);

            messageModel.listModel = _messageService.GetSentMessage(User.Identity.Name).Select(x => new ItemMessageViewModel
            {
                ID = x.ID,
                Customer_Login = x.Customer_Login,
                Theme = x.Theme,
                Content = x.Content,
                PostDate = x.PostDate
            }).ToList().ToPagedList(No_Of_Page, Size_Of_Page);

            return View("~/Views/Message/_SentMessage.cshtml", messageModel);
        }

        [Authorize]
        public ActionResult _ReceivedMessage(int? Page_No)
        {
            MessageViewModel messageModel = new MessageViewModel();

            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);

            messageModel.listModel = _messageService.GetReceivedMessages(User.Identity.Name).Select(x=>new ItemMessageViewModel
            {
                ID = x.ID,
                Sender_Login = x.Sender_Login,
                Theme = x.Theme,
                Content = x.Content,
                PostDate = x.PostDate,
                IfRead = x.IfRead
            }).ToList().ToPagedList(No_Of_Page, Size_Of_Page);

            return View("~/Views/Message/_ReceivedMessage.cshtml", messageModel);
        }

        [Authorize]
        public ActionResult Content(int MessageID)
        {
            MessageViewModel messageModel = new MessageViewModel();
            messageModel.viewModel = new ItemMessageViewModel();

            var temp = _messageService.ConentMessage(MessageID, User.Identity.Name);

            messageModel.viewModel.ID = temp.ID;
            messageModel.viewModel.Customer_Login = temp.Customer_Login;
            messageModel.viewModel.Sender_Login = temp.Sender_Login;
            messageModel.viewModel.Theme = temp.Theme;
            messageModel.viewModel.Content = temp.Content;
            messageModel.viewModel.PostDate = (DateTime)temp.PostDate;

            return View(messageModel);
        }

        [Authorize]
        public void DeleteMessage(int a)
        {
            _messageService.DeleteMessage(a);
        }


    }
}