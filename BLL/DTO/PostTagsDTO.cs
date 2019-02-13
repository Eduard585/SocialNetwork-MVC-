using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace BLL.DTO
{
   
    public partial class PostTagsDTO
    {
        public long PostId { get; set; }
       
        public string Tag { get; set; }
        
        public PostDTO Posts { get; set; }
    }
}
