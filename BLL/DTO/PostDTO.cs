using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Spatial;

namespace BLL.DTO
{
    public partial class PostDTO
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public long UserId { get; set; }
        //public DbGeography Location { get; set; }
       // public string LocationName { get; set; }
        public DateTime PublicateDate { get; set; }
        public bool News {get;set;}
        public bool IsLiked { get; set; }

        public virtual List<CommentDTO> Comments { get; set; }
        public virtual List<ImageDTO> Images { get; set; }
        public virtual List<LikesDTO> Likes { get; set; }
        public virtual List<PostTagsDTO> PostTags { get; set; }
        public virtual UserDTO User { get; set; }

    }
}
