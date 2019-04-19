using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using BLL.DTO;

namespace MyInstaMVC.signalr.hubs
{
    public class ChatHub : Hub
    {
        public void Send(long userId,string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(userId,message);
            var chat = new ChatDTO();
            chat.UserId = userId;
            chat.Message = message;
            chat.Date = DateTime.Now;
            BLL.Data.CreateMessage(chat);
        }
    }
}