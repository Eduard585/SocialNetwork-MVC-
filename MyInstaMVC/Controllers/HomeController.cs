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

        [HttpGet]
        public ActionResult GetLoves()
        {
            var model = new Models.LoveModel { };
            model.Loves = BLL.Data.GetLoves();
            return View("Index", model);
        }
    }
}