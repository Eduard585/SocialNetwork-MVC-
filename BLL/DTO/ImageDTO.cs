using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
   public partial class ImageDTO
    {
        public long Id { get; set; }
        public DateTime CreateDate { get; set; }
        public long? PostId { get; set; }
        public long UserId { get; set; }

        public PostDTO Post { get; set; }
    }
}
