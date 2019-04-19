using MyInstaMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Validation;
using MyInstaMVC.CustomAuth;

namespace MyInstaMVC.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        // GET: Chat
        public ActionResult Index()
        {           
            var model = new ChatModel();           
            model.Chats = BLL.Data.GetChatMessages();
            model.currentChatUser = ((CustomPrincipal)User).UserId;
            return View(model);
        }

       

        [HttpGet]        
        public JsonResult GetChatMessages(int currentPage)
        {
            var model = new ChatModel();

            model.Chats = BLL.Data.GetChatMessages(currentPage);

            return Json(new { model.Chats } , JsonRequestBehavior.AllowGet);
        }

    }
}