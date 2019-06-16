using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BLL.DTO;
using AutoMapper;
namespace BLL
{
    public static class Data
    {
        static Data()
        {
        }
        public static long CreateUpdateUser(UserDTO user)
        {
            try
            {
                using (var ctx = new DAL.InstaDbEntities())
                {
                    var dbUser = ctx.Users.FirstOrDefault(x => x.ID == user.ID) ?? ctx.Users.Add(new DAL.Users());
                    
                    if (ctx.Users.Any(x => x.LoginName == user.LoginName && x.ID != dbUser.ID))
                        throw new Exception($"User with loginName :{user.LoginName} exist");

                      dbUser.BirthDate = user.BirthDate;
                      dbUser.Description = user.Description;
                      dbUser.LoginName = user.LoginName;
                      dbUser.NickName = user.NickName;
                      dbUser.PasswordHash = user.PasswordHash;
                      dbUser.RegDate = user.RegDate;
                      dbUser.Salt = user.Salt;
                      dbUser.SharedProfile = user.SharedProfile;
                      dbUser.Gender = user.Gender;
                      dbUser.AvatarContent = user.AvatarContent;
                      dbUser.AvatarMime = user.AvatarMime;

                   // AutoMapper.Mapper.Map(user, dbUser);
                    ctx.SaveChanges();
                    return dbUser.ID;
                }
            }
            catch (Exception ex)
            {
                throw;

            }
        }

        public static long UpdateUser(UserDTO user)
        {
            try
            {
                using (var ctx = new DAL.InstaDbEntities())
                {
                    var dbUser = ctx.Users.FirstOrDefault(x => x.ID == user.ID) ?? ctx.Users.Add(new DAL.Users());

                    if (ctx.Users.Any(x => x.LoginName == user.LoginName && x.ID != dbUser.ID))
                        throw new Exception($"User with loginName :{user.LoginName} exist");

                    dbUser.BirthDate = user.BirthDate;
                    dbUser.Description = user.Description;
                    dbUser.LoginName = user.LoginName;
                    dbUser.NickName = user.NickName;
                    dbUser.PasswordHash = user.PasswordHash;
                    dbUser.RegDate = user.RegDate;
                    dbUser.Salt = user.Salt;
                    dbUser.SharedProfile = user.SharedProfile;
                    dbUser.Gender = user.Gender;
                                     
                    ctx.SaveChanges();
                    return dbUser.ID;
                }
            }
            catch (Exception ex)
            {
                throw;

            }
        }

        public static string UpdateSubscribing(SubscribtionsDTO subs)
        {
            using (var ctx = new DAL.InstaDbEntities())
            {
                var dbSub = ctx.Subscribtions.FirstOrDefault(x =>
                    x.UserID == subs.UserID && x.FollowingUserID == subs.FollowingUserID);               
                if (dbSub == null)
                {
                    dbSub = AutoMapper.Mapper.Map<DAL.Subscribtions>(subs);
                    ctx.Subscribtions.Add(dbSub);
                    
                    var dbSub2 = ctx.Subscribtions.FirstOrDefault(x =>
                        x.UserID == subs.FollowingUserID && x.FollowingUserID == subs.UserID);
                    if (dbSub2 != null)
                    {
                        ctx.Friends.Add(new DAL.Friends
                        {
                            UserId = subs.UserID,
                            FriendId = subs.FollowingUserID
                        });
                    }
                    ctx.SaveChanges();
                    return "SubCreated";
                }
                else
                {
                    ctx.Subscribtions.Remove(dbSub);
                    return "SubDeleted";
                }
            }

        }

        public static List<SubscribtionsDTO> GetSubscribtions(long currentUserId)
        {
            List<SubscribtionsDTO> subList = null;
            using (var ctx = new DAL.InstaDbEntities())
            {
                var dbSubs = ctx.Subscribtions.Select(x => x.UserID == currentUserId).ToList();
                AutoMapper.Mapper.Map(subList, dbSubs);
            }

            return subList;
        }
        
        public static UserDTO GetUser(long? id = null, string Login = null)
        {
            if (!id.HasValue && string.IsNullOrEmpty(Login))
                return null;
            try
            {
                using (var ctx = new DAL.InstaDbEntities() )
                {                    
                    var user = ctx.Users.Where(x => x.ID == id || x.LoginName == Login).Select(AutoMapper.Mapper.Map<DTO.UserDTO>).FirstOrDefault();
                    
                    if (user != null | user.Roles != null)
                    {
                        return user;
                    }
                    throw new Exception($"Not found User with ID:{id}");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static IEnumerable<UserDTO> GetUsers()
        {
            int take = 20;
            try
            {
                using (var ctx = new DAL.InstaDbEntities())
                {
                    IEnumerable<UserDTO> userList = null;
                    userList = ctx.Users.Select(AutoMapper.Mapper.Map<DTO.UserDTO>).Take(take).ToList();
                    return userList;
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        private static IEnumerable<UserDTO> GetUsersById(List<long> userList)
        {
            using (var ctx = new DAL.InstaDbEntities())
            {
                var users = ctx.Users.Select(AutoMapper.Mapper.Map<DTO.UserDTO>).Where(x => userList.Contains(x.ID))
                    .ToList();
                return users;
            }
        }

        public static IEnumerable<UserDTO> GetFriends(long? userId)
        {
            
                using (var ctx = new DAL.InstaDbEntities())
                {
                    List<long> friendsListIds = null;
                    friendsListIds = ctx.Friends.Where(x => x.UserId == userId).Select(x => x.UserId).ToList();
                    return GetUsersById(friendsListIds);
                }            
        }

        public static bool ValidateUser(string login, string password)
        {
            var user = BLL.Data.GetUser(Login: login);
            if (user != null)
            {
                var salt = Convert.FromBase64String(user.Salt);
                var passhash = BLL.Hash.GenerateSaltedHash(password, salt);

                var oldHash = Convert.FromBase64String(user.PasswordHash);

                return BLL.Hash.CompareByteArrays(passhash, oldHash);
            }
            return false;
        }

        public static void SetAvatar(long userId, ImageWrapper avatar)
        {
            using (var ctx = new DAL.InstaDbEntities())
            {
                var user = ctx.Users.FirstOrDefault(x => x.ID == userId);
                if (user == null)
                    throw new Exception("Error from SetAvatar");
                user.AvatarContent = avatar.Content;
                user.AvatarMime = avatar.Mime;
                ctx.SaveChanges();
            }

        }

        public static ImageWrapper GetAvatar(long UserId)
        {
            var res = new ImageWrapper();
            using (var ctx = new DAL.InstaDbEntities())
            {
                var user = ctx.Users.FirstOrDefault(x => x.ID == UserId);
                if (user == null)
                    throw new Exception("User not Found");
                res.Content = user.AvatarContent;
                res.Mime = user.AvatarMime;
            }
            return res;

        }

        public static long AddImage(long UserId, ImageWrapper ImageData)
        {
            using (var ctx = new DAL.InstaDbEntities())
            {
                var img = ctx.Images.Add(new DAL.Images
                {
                    UserID = UserId,
                    CreateDate = DateTime.Now,
                    ImageContent = ImageData.Content,
                    MimeType = ImageData.Mime
                });

                ctx.SaveChanges();

                return img.ID;
            }
        }

      
        
        public static ImageWrapper GetImage(long ImageId)
        {
            var res = new ImageWrapper();
            using (var ctx = new DAL.InstaDbEntities())
            {
                var img = ctx.Images.FirstOrDefault(x => x.ID == ImageId);
                if (img == null)
                    throw new Exception("Image not Found");
                res.Content = img.ImageContent;
                res.Mime = img.MimeType;
            }
            return res;
        }
        public static void DelImage(long ImageId)
        {
            using (var ctx = new DAL.InstaDbEntities())
            {
                var img = ctx.Images.FirstOrDefault(x => x.ID == ImageId);
                if (img == null)
                    throw new Exception("Нет такой фотки");
                ctx.Images.Remove(img);
                ctx.SaveChanges();
            }
        }
        public static List<long> GetMyImagesIds(long UserId)
        {
            var res = new List<long>();
            using (var ctx = new DAL.InstaDbEntities())
            {
                res.AddRange(ctx.Images.Where(x => x.UserID == UserId && !x.PostID.HasValue).Select(x => x.ID));
            }
            return res;
        }
        public static void CreatePost(PostDTO post)
        {
            using (var ctx = new DAL.InstaDbEntities())
            {
                var imgIds = post.Images.Select(x => x.Id).ToArray();
                var images = ctx.Images.Where(x => imgIds.Contains(x.ID)).ToList();
                if (images.Any(x => x.PostID.HasValue))
                    throw new Exception("Чужие картинки");
                var dbPost = AutoMapper.Mapper.Map<DAL.Posts>(post);
                dbPost.Images.Clear();
                images.ForEach((x) => dbPost.Images.Add(x));
                dbPost.Date = DateTime.Now;
                dbPost.PublicateDate = DateTime.Now;
                ctx.Posts.Add(dbPost);
                ctx.SaveChanges();
            }
        }

        public static PostDTO GetPostById(long PostId)
        {
            var res = (PostDTO)null;
            using (var ctx = new DAL.InstaDbEntities())
            {
                res = ctx.Posts.Where(x => x.ID == PostId).Select(AutoMapper.Mapper.Map<PostDTO>).FirstOrDefault();
                res.User = GetUser(res.UserId);
                if (res == null)
                    throw new Exception("Нет такого поста");
                
            }
            return res;
        }

        public static List<PostDTO> GetPosts(int page = 0, long? userId = null, long? currentUserId = null)//следует убрать второе значение userId и поменять  getPOsts в My
        {
            int take = 4;
            int skip = take * page;
            var res = (List<PostDTO>)null;    
            
            using (var ctx = new DAL.InstaDbEntities())
            {
                var temp = ctx.Posts.Where(x => !userId.HasValue || userId.HasValue && x.UserID == userId.Value);
                
                res = temp.OrderByDescending(x => x.PublicateDate).ThenByDescending(x => x.Date).Skip(skip).Take(take).Select(AutoMapper.Mapper.Map<PostDTO>).ToList();
                for(int i = 0; i<res.Count; i++)
                {
                    res[i].User = GetUser(res[i].UserId);
                    if (res[i].Likes.FirstOrDefault(x => x.UserId == currentUserId) != null)
                    {
                        res[i].IsLiked = true;
                    }
                    else res[i].IsLiked = false;
                }
                
            }
            return res;
        }
        
        private static void LikePosts()
        {

        }
        public static void PublishPost(long id)
        {
            using (var ctx = new DAL.InstaDbEntities())
            {
                var post = ctx.Posts.FirstOrDefault(x => x.ID == id);
                if (post != null)
                {
                    post.PublicateDate = DateTime.Now;
                    ctx.SaveChanges();
                }
            }
        }

        public static long CreateComment(CommentDTO com)
        {
            using (var ctx = new DAL.InstaDbEntities())
            {
                var dbCom = AutoMapper.Mapper.Map<DAL.Comments>(com);
                ctx.Comments.Add(dbCom);
                ctx.SaveChanges();
                return dbCom.ID;
            }
        }
        public static CommentDTO GetComment(long id)
        {
            using (var ctx = new DAL.InstaDbEntities())
            {
                var ct = ctx.Comments
                    .Where(x => x.ID == id)
                    .Select(AutoMapper.Mapper.Map<CommentDTO>)
                    .FirstOrDefault();
                return ct;
            }
        }

        public static long CreateLike(LikesDTO like)
        {
            try
            {
                using (var ctx = new DAL.InstaDbEntities())
                {
                    var foundLike = ctx.Likes.FirstOrDefault(u => u.PostID == like.PostId && u.UserID == like.UserId);
                    var dbLike = AutoMapper.Mapper.Map<DAL.Likes>(like);
                    if (foundLike == null)
                    {

                        ctx.Likes.Add(dbLike);
                        ctx.SaveChanges();
                        return dbLike.ID;
                    }
                    else
                    {
                        ctx.Likes.Remove(foundLike);
                        ctx.SaveChanges();
                        return 0;
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public static int GetLikesCount(long postId)
        {
            using (var ctx = new DAL.InstaDbEntities())
            {
                var ct = ctx.Likes
                    .Where(x => x.PostID == postId)
                    .Select(AutoMapper.Mapper.Map<LikesDTO>);
                return ct.Count();
            }
        }

        public static bool IsLikeDone(long postId, long? currentUserId)
        {
            using (var ctx = new DAL.InstaDbEntities())
            {
                if (ctx.Likes.FirstOrDefault(x => x.PostID == postId && x.UserID == currentUserId) != null) return true;
                else return false;
            }
        }

        public static List<Love> GetLoves(int qty = 8)
        {
            var res = (List<Love>)null;

            using (var ctx = new DAL.InstaDbEntities())
            {
                var temp = ctx.Love.Where(x => x.ID >=0);
                res = temp.OrderByDescending(x => x.Date).Select(AutoMapper.Mapper.Map<Love>).ToList();
            }
            return res;
        }

        public static Love CreateLove(Love love)
        {
                   
            //Чтобы не генерировать рандомно числа, про сто каждый раз к номеру прибавляем единицу. Меньше будет проблем
            try
            {


                using (var ctx = new DAL.InstaDbEntities())
                {
                    if(ctx.Love.Any(x=>x.ID == love.ID))
                        throw new Exception($"Вы уже получили свою валентинку!");
                    var gender = ctx.Users.Where(x => x.ID == love.ID).Select(x => x.Gender).FirstOrDefault();
                    
                    var number = ctx.Love.Where(x=>x.Gender == gender).OrderByDescending(x => x.Date).FirstOrDefault().Number;
                    number++;
                    var dbLove = AutoMapper.Mapper.Map<DAL.Love>(love);
                    dbLove.Gender = gender;
                    dbLove.Number = number;
                    dbLove.NickName = ctx.Users.Where(x => x.ID == love.ID).Select(x => x.NickName).FirstOrDefault();

                    ctx.Love.Add(dbLove);
                    ctx.SaveChanges();
                    var retL = AutoMapper.Mapper.Map<DTO.Love>(dbLove);//Требуется доработка. Можно не создавать доп. переменные а записывать все в love
                    return retL;
                }
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public static List<ChatDTO> GetChatMessages(long RecipientId, int page = 0)
        {
            int take = 15;
            int skip = take * page;          
            var res = (List<ChatDTO>)null;
            using(var ctx = new DAL.InstaDbEntities())
            {
                var temp = ctx.User_Chat.Where(x => x.Date != null && x.RecipientId == RecipientId);
                res = temp.OrderByDescending(x => x.Date).Skip(skip).Take(take).Select(AutoMapper.Mapper.Map<ChatDTO>).ToList();
            }          
            return res;
        }

        public static void CreateMessage(ChatDTO chat)
        {
           
                using (var ctx = new DAL.InstaDbEntities())
                {
                    var dbChat = AutoMapper.Mapper.Map<DAL.User_Chat>(chat);
                    ctx.User_Chat.Add(dbChat);
                    try
                    {
                        ctx.SaveChanges();
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                    {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting
                            // the current instance as InnerException
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }

                }
            
           


        }

    }
}
