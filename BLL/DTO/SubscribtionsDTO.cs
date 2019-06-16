using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class SubscribtionsDTO
    {
        public long ID { get; set; }
        public long UserID { get; set; }
        public long FollowingUserID { get; set; }
        public DateTime Date { get; set; }
        public bool isActive { get; set; }
    }
}
