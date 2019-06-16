using MyInstaMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Validation;
using Microsoft.AspNet.SignalR;
using MyInstaMVC.CustomAuth;

namespace MyInstaMVC.Controllers
{
    [System.Web.Mvc.Authorize]
    public class ChatController : Controller, IUserIdProvider
    {
        private long? _currentUserId { get { return ((CustomAuth.CustomPrincipal)User)?.UserId; } }
        // GET: Chat
        public ActionResult Index()
        {           
            var model = new ChatModel();           
            model.Chats = BLL.Data.GetChatMessages(0);
            model.currentChatUser = ((CustomPrincipal)User).UserId;
            return View(model);
        }

       

        [HttpGet]        
        public JsonResult GetChatMessages(int currentPage)
        {
            var model = new ChatModel();

            model.Chats = BLL.Data.GetChatMessages(0,currentPage);

            return Json(new { model.Chats } , JsonRequestBehavior.AllowGet);
        }

        public ActionResult PrivateChat()
        {
            var model = new ChatModel();
            model.Chats = BLL.Data.GetChatMessages((long)_currentUserId);
            model.currentChatUser = ((CustomPrincipal)User).UserId;
            return View(model);
        }

        public string GetUserId(IRequest request)
        {
            return _currentUserId.ToString();
        }
    }
}