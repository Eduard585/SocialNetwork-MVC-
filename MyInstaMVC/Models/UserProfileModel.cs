using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.DTO;

namespace MyInstaMVC.Models
{
    
    public class UserProfileModel
    {
        public UserModel User { get; set; }  
    }

    public class UserModel
    {
        public UserModel() { }
        public long Id { get; set; }
        public string LoginName { get; set; }
        public string NickName { get; set; }
        public string Description { get; set; }
        public bool SharedProfile { get; set; }
        
        public UserModel(BLL.DTO.UserDTO dbUser)
        {
            Description = dbUser.Description;
            LoginName = dbUser.LoginName;
            NickName = dbUser.NickName;
            SharedProfile = dbUser.SharedProfile;
            Id = dbUser.ID;
            

        }
    }
}