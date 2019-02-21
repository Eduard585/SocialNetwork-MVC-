using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public partial class Love
    {
        public long ID { get; set; }
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public string Gender { get; set; }
    }
}
