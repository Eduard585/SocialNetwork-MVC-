using MyInstaMVC.Models;
using MyInstaMVC.CustomAuth;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.DTO;
namespace MyInstaMVC.Controllers
{
    [Authorize]
    public class UserProfileController : Controller
    {
        private long? _currentUserId { get { return ((CustomAuth.CustomPrincipal)User)?.UserId; } }
        private UserProfileModel InitModel()
        {
            var dbUser = BLL.Data.GetUser(_currentUserId);
            var model = new UserProfileModel
            {
                User = new UserModel(dbUser)
            };

            return model;
        }

        public string GetCurrentUserLogin()
        {
            return new CustomPrincipal(User.Identity.Name).Identity.Name;
        }

        private UserProfileModel InitModel(long userId)
        {
            var dbUser = BLL.Data.GetUser(userId);
            var model = new UserProfileModel
            {
                User = new UserModel(dbUser)
            };

            return model;
        }
        // GET: UserProfile
        public ActionResult MyProfile()
        {           
            return PartialView("Index",InitModel(_currentUserId.Value));
        }
        
        [HttpGet]
        public ActionResult Index(long userId)
        {

            return View(InitModel(userId));
        }

        [HttpGet]
        public ActionResult EditProfile()
        {
            return View(InitModel().User);
        }

        [HttpPost]
        public ActionResult EditProfile(UserModel model, HttpPostedFileBase AvatarImage)
        {
            var user = BLL.Data.GetUser(model.Id);
         
            if (AvatarImage != null)
            {

                BLL.Data.SetAvatar(model.Id, new BLL.DTO.ImageWrapper(AvatarImage));
            }
                     
            user.LoginName = model.LoginName;
            user.NickName = model.NickName;
            user.Description = model.Description;
            user.SharedProfile = model.SharedProfile;
            user.Description = model.Description;
            BLL.Data.UpdateUser(user);

            return View(model);
        }

        public ActionResult Avatar(long Id)
        {
            var avatar = BLL.Data.GetAvatar(Id);
            if (avatar == null)
                return HttpNotFound();
            return File(avatar.Content, avatar.Mime);
        }


        public JsonResult UpdateSubscribtion(long followingUserId)
        {
            var res = BLL.Data.UpdateSubscribing(new BLL.DTO.SubscribtionsDTO
            {
                Date = DateTime.Now,
                FollowingUserID = followingUserId,
                isActive = true,
                UserID = (long)_currentUserId
            });
            return Json(res);
        }
    }
}