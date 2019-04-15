using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyInstaMVC.Models
{
    public class ChatModel
    {
        public List<BLL.DTO.ChatDTO> Chats { get; set; }
        public long currentChatUser;
    }
}