using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;


namespace BLL.DTO
{
   public partial class ImageWrapper
    {
        public ImageWrapper(HttpPostedFileBase file = null)
        {
            if (file != null)
            {
                using (var binaryReader = new BinaryReader(file.InputStream))
                {
                    Content = binaryReader.ReadBytes(file.ContentLength);
                    Mime = file.ContentType;//добавлен коммент
                }
            }
        }

        public byte[] Content { get; set; }
        public string Mime { get; set; }
    }
}
