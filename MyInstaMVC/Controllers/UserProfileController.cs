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
        private UserProfileModel InitModel()
        {
            var dbUser = BLL.Data.GetUser(((CustomPrincipal)User).UserId);
            var model = new UserProfileModel
            {
                User = new UserModel(dbUser)
            };

            return model;
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
        public ActionResult Index()
        {

            return View(InitModel());
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

            //if (Request.Files != null && Request.Files.Count != 0 && Request.Files[0].ContentLength > 0)
            // {
            //    var file = Request.Files[0];
            if (AvatarImage != null)
            {

                BLL.Data.SetAvatar(model.Id, new BLL.DTO.ImageWrapper(AvatarImage));
            }
            // }

            
            user.LoginName = model.LoginName;
            user.NickName = model.NickName;
            user.Description = model.Description;
            user.SharedProfile = model.SharedProfile;
            user.Description = model.Description;
            BLL.Data.CreateUpdateUser(user);

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