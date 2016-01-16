using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Game.Service.Interfaces;
using Game.Core.DTO;
using System.Web.Routing;
using System.Configuration;
using Microsoft.AspNet.SignalR.Hubs;

namespace Game.GUI
{
    public class GameHub : Hub
    {
        private static string conString = ConfigurationManager.ConnectionStrings["gameDbContext"].ToString();

        [HubMethodName("sentNotification")]

        public void sentnotification(string user, string pin)
        {
            IHubContext context = GlobalHost.ConnectionManager.GetHubContext<GameHub>();

            context.Clients.User(user).shownotification(user, pin);

        }
    }
}