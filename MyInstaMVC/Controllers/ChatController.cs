using MyInstaMVC.Models;
using MyInstaMVC.CustomAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyInstaMVC.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        // GET: Chat
        public ActionResult Index()
        {
            var dbUser = BLL.Data.GetUser(((CustomPrincipal)User).UserId);
            //var model = new UserMode l(dbUser);
            var model = new ChatModel();
            //model.Chats = BLL.Data.GetChatMessages();
            model.Chats = BLL.Data.GetChatMessages();
            model.currentChatUser = new UserModel(dbUser);
            return View(model);
        }

        public ActionResult Avatar(long Id)
        {
            var avatar = BLL.Data.GetAvatar(Id);
            if (avatar == null)
                return HttpNotFound();
            return File(avatar.Content, avatar.Mime);
        }

    }
}