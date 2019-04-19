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
    public class HomeController: Controller
    {
        private long? _currentUserId { get { return ((CustomAuth.CustomPrincipal)User)?.UserId; } }
        public ActionResult Index()
        {

            var model = new Models.LoveModel { };
            model.Loves = BLL.Data.GetLoves();
            return View("Index", model);
        }

        public ActionResult ViewPage1()
        {
            return View();
        }

        
        public JsonResult CreateLoves()
        {
            var result = new JsonResultResponse { Success = true };
            try
            {
                var userId = _currentUserId.Value;

                var loveNum = BLL.Data.CreateLove(new BLL.DTO.Love { ID = userId, Date = DateTime.Now});
                result.Result = "Ваша пара еще не найдена!";

            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Result = ex.Message;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}