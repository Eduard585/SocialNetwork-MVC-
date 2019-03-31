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
        public void Send(string name, string message,long userId)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message,userId);
            var chat = new ChatDTO();
            chat.UserId = userId;
            chat.Message = message;
            chat.Date = DateTime.Now;
            BLL.Data.CreateMessage(chat);
        }
    }
}