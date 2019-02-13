using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public partial class CommentDTO
    {
        public long ID { get; set; }
        public DateTime Date { get; set; }
        public string DateString { get; set; }
        public long UserID { get; set; }
        public long PostID { get; set; }
        public string CommentText { get; set; }

        public string UserNickname { get; set; }
    }
}
