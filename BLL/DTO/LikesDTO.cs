using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public partial class LikesDTO
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public long UserId { get; set; }
        public long PostId { get; set; }
        
        public UserDTO User { get; set; }
        
    }
}
