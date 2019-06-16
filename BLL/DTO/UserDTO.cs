using System;
using System.Collections.Generic;


namespace BLL.DTO
{
    public partial class UserDTO
    {
        public long ID { get; set; }
        public string LoginName { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string NickName { get; set; }
        public DateTime RegDate { get; set; }
        public DateTime BirthDate { get; set; }
        public string Description { get; set; }
        public bool SharedProfile { get; set; }
        public string Gender { get; set; }
        public byte[] AvatarContent { get; set; }
        public string AvatarMime { get; set; }

        public virtual ICollection<RoleDTO> Roles { get; set; }
    }
}
