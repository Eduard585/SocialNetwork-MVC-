using MyInstaMVC.CustomAuth;
using MyInstaMVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity.Validation;


namespace MyInstaMVC.Controllers
{
   //[CustomAuthorize(Roles = "User, Admin")]
    
    public class UserController : Controller
    {
        public ActionResult Registration()
        {
            var model = new Models.RegistrationModel()
            { SharedProfile = true };

            return View(model);
        }

        [HttpPost]
        public ActionResult Registration(Models.RegistrationModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            try
            {
                var salt = BLL.Hash.CreateSalt(16);
                var passhash = BLL.Hash.GenerateSaltedHash(model.Password, salt);
                byte[] avatarcont = { 0, 16, 104, 213 };
                string avatarmime = "NULL";
                var res =  BLL.Data.CreateUpdateUser(new BLL.DTO.UserDTO
                {
                    Salt = Convert.ToBase64String(salt),
                    PasswordHash = Convert.ToBase64String(passhash),
                    NickName = model.NickName,
                    BirthDate = model.BirthDate,
                    RegDate = DateTime.Now,
                    SharedProfile = model.SharedProfile,
                    LoginName = model.LoginName,
                    Gender = model.Gender,
                    AvatarContent = avatarcont,
                    AvatarMime = avatarmime
                });
                
                ViewBag.Message = res;
               
            }
            catch (DbEntityValidationException ex)
            {

                foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                {
                    Response.Write("Object: " + validationError.Entry.Entity.ToString());
                    Response.Write(" ");
                    foreach (DbValidationError err in validationError.ValidationErrors)
                    {
                        Response.Write(err.ErrorMessage + "");
                    }
                }
            }


            return View(model);
        }
            
        public ActionResult Login(string ReturnUrl = "")
        {
            if (User.Identity.IsAuthenticated)
            {
                return LogOut();
            }
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        [HttpPost]
        public ActionResult Login(Models.LoginModel model, string ReturnUrl = "")
        {

            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.Login, model.Password))
                {
                    var user = (CustomMembershipUser)Membership.GetUser(model.Login, false);
                    if (user != null)
                    {
                        CustomSerializeModel userModel = new Models.CustomSerializeModel()
                        {
                            UserId = user.UserID,
                            Nickname = user.NickName,
                            Email = user.Email,
                            RoleName = user.Roles.Select(r=>r.RoleName).ToList()
                        };

                        string userData = JsonConvert.SerializeObject(userModel);
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
                            (
                            1, model.Login, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData
                            );

                        string enTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie faCookie = new HttpCookie("Cookie1", enTicket);
                        Response.Cookies.Add(faCookie);
                    }

                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            ModelState.AddModelError("", "Something Wrong : Username or Password invalid ^_^ ");
            return View(model);
        }

        public ActionResult LogOut()
        {
            HttpCookie cookie = new HttpCookie("Cookie1", "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);

            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User", null);
        }
    }
    [CustomAuthorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        
    }

}