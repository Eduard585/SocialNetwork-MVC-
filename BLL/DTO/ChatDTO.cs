﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public partial class ChatDTO
    {
        public long UserId { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }

        public virtual UserDTO User { get; set; }
    }
}
