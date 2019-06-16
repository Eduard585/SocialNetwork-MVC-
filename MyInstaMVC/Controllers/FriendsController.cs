using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyInstaMVC.Models;

namespace MyInstaMVC.Controllers
{
    public class FriendsController : Controller
    {
        private long? _currentUserId { get { return ((CustomAuth.CustomPrincipal)User)?.UserId; } }
        // GET: Friends
        public ActionResult Index()
        {
            var users = BLL.Data.GetFriends(_currentUserId);
            IEnumerable<UserModel> userModel;
            userModel = users.Select(x => new UserModel()
            {
                LoginName = x.LoginName,
                NickName = x.NickName,
                Id = x.ID,
                Description = x.Description
            });
            return View(userModel);
        }
    }
}