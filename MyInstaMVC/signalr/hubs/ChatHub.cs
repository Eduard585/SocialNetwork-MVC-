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
            try
            {
                // Call the broadcastMessage method to update clients.
                Clients.All.broadcastMessage(userId, message);
                var chat = new ChatDTO
                {
                    SenderId = userId,
                    RecipientId = 0,
                    Message = message,
                    Date = DateTime.Now
                };
                BLL.Data.CreateMessage(chat);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void SendPrivate(string userId, string message)
        {
            Clients.User(userId).sendPrivateMessage(message);
        }
    }
}