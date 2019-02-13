using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using BLL.DTO;

namespace MyInstaMVC.CustomAuth
{
    public class CustomMembershipUser : MembershipUser
    {
        #region User Properties  
        public long UserID { get; set; }
        public string NickName { get; set; }
        public ICollection<RoleDTO> Roles { get; set; }

        #endregion

        public CustomMembershipUser(UserDTO user) : base("CustomMembership", user.LoginName,user.ID, user.LoginName, string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
        {
            UserID = user.ID;
            NickName = user.NickName;
            Roles = user.Roles;
        }
    }
}