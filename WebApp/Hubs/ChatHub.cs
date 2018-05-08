using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLogic;
using Microsoft.AspNet.SignalR;

namespace WebApp.Hubs
{
    public class ChatHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
        public void Send(string userName, string msgBy, string msgTo, string message)
        {
            GateWay.messageSender(message, Convert.ToInt32("0"), Convert.ToInt32(msgBy));
            Clients.All.sendMessage(userName, message,DateTime.Now.ToString("dd MMMM ,yyyy"),DateTime.Now.ToString("hh:mm:ss"));

        }
        public void login(int userId)
        {
       
            Clients.Caller.GetId(userId);
        }



    }
}