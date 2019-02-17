using MyInstaMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Validation;
using MyInstaMVC.CustomAuth;
namespace MyInstaMVC
{

    [Authorize]
    public class PostController : Controller
    {
        private long? _currentUserId { get { return ((CustomAuth.CustomPrincipal)User)?.UserId; } }
        // GET: Post
        public ActionResult Index()
        {          
            var user = (CustomAuth.CustomPrincipal)User;           
            var model = new Models.PostModel { FormMode = PostFormMode.all };
            model.Posts = BLL.Data.GetPosts(currentUserId: _currentUserId );
            model.NextExist = BLL.Data.GetPosts(1).Any();
            model.ShowAdminControl = user.IsInRole("Admin");
            return View(model);
        }

        public ActionResult My()
        {
            var model = new Models.PostModel { FormMode = PostFormMode.my };
            model.Posts = BLL.Data.GetPosts(userId: _currentUserId,currentUserId: _currentUserId);
            model.NextExist = BLL.Data.GetPosts(1, _currentUserId).Any();
            return View("Index", model);
        }

        [HttpPost]
        public JsonResult PublishPost(long id)
        {
            var result = new JsonResultResponse { Success = true };
            try
            {
                BLL.Data.PublishPost(id);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Result = ex.Message;
            }
            return Json(result);
        }

        [HttpGet]
        public PartialViewResult Create()
        {
            ViewBag.Images = BLL.Data.GetMyImagesIds(_currentUserId.Value);



            return PartialView();
        }

        [HttpGet]
        public PartialViewResult CreateAdmin()
        {
            ViewBag.Images = BLL.Data.GetMyImagesIds(_currentUserId.Value);



            return PartialView();
        }
        [HttpPost]
        public JsonResult Create(BLL.DTO.PostDTO model)
        {
            var result = new JsonResultResponse { Success = true };
            try
            {

                model.UserId = _currentUserId.Value;

                BLL.Data.CreatePost(model);


                

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
            return Json(result);
        }




        [HttpPost]
        public JsonResult UploadImages()
        {
            var result = new JsonResultResponse { Success = true };
            try
            {
                if (Request.Files != null && Request.Files.Count != 0 && Request.Files[0].ContentLength > 0)
                {
                    var file = Request.Files[0];

                    var userId = _currentUserId.Value;

                    result.Result = BLL.Data.AddImage(userId, new BLL.DTO.ImageWrapper(file));
                }
                else
                    throw new Exception("Не найдено файлов");

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Result = ex.Message;
            }
            return Json(result);

        }

        public ActionResult GetImage(long Id)
        {
            var avatar = BLL.Data.GetImage(Id);

            if (avatar.Content == null)
                return HttpNotFound();

            return File(avatar.Content, avatar.Mime);
        }

        [HttpPost]
        public JsonResult DelImage(long id)
        {
            var result = new JsonResultResponse { Success = true };
            try
            {
                BLL.Data.DelImage(id);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Result = ex.Message;
            }
            return Json(result);

        }

        public PartialViewResult GetPosts(int page, bool my = false)
        {
            var model = new Models.PostModel { FormMode = my ? PostFormMode.my : PostFormMode.all };
            model.Posts = BLL.Data.GetPosts(page, my ? _currentUserId : null,_currentUserId);
            model.NextExist = BLL.Data.GetPosts(page + 1, my ? _currentUserId : null).Any();
            
            return PartialView("_MorePostView", model);

        }
        public PartialViewResult GetPost(long id)
        {
            var model = BLL.Data.GetPostById(id);
            return PartialView("_PostView", model);
        }

        public JsonResult GetComments(long id)
        {
            var model = BLL.Data.GetPostById(id);
            return Json(new { model.Comments }, JsonRequestBehavior.AllowGet);
            
        }

        [HttpPost]
        public JsonResult AddComment(long postId, string commentText)
        {
            var result = new JsonResultResponse { Success = true };
            try
            {
                var userId = _currentUserId.Value;
                var comId = BLL.Data.CreateComment(new BLL.DTO.CommentDTO { UserID = userId, CommentText = commentText, PostID = postId, Date = DateTime.Now });
                result.Result = BLL.Data.GetComment(comId);
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Result = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult AddLike(long postId)
        {
            var result = new JsonResultResponse { Success = true };
            try
            {
                var userId = _currentUserId.Value;
                BLL.Data.CreateLike(new BLL.DTO.LikesDTO { UserId = userId, PostId = postId, Date = DateTime.Now });
                result.Result = BLL.Data.GetLikesCount(postId);
                
            }
            catch(Exception ex)
            {
                result.Success = false;
                result.Result = ex.Message;
            }
            return Json(result);

        }

        public JsonResult GetLike(long postId)
        {
            var result = new JsonResultResponse { Success = true };
            if (BLL.Data.IsLikeDone(postId,_currentUserId) == true)
            {
                result.Result = true;
            }
            else result.Result = false;
            return Json(result);
        }

    }
}