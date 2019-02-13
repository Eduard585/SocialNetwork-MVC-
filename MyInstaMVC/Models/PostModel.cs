using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyInstaMVC.Models
{
    public class PostModel
    {
        public PostFormMode FormMode { get; set; }
        public bool NextExist { get; set; }
        public List<BLL.DTO.PostDTO> Posts { get; set; }
        public bool ShowAdminControl { get; set; }
        public bool IsLiked { get; set; }
    }

    public enum PostFormMode
    {
        my,
        all
    }
}