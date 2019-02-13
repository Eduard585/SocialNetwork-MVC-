using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
   public class RoleDTO
    {
        public long ID { get; set; }
        public string RoleName { get; set; }
        public virtual ICollection<UserDTO> Users { get; set; }
    }
}
