﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyInstaMVC.Models
{
    public class JsonResultResponse
    {
        public bool Success { get; set; }
        public object Result { get; set; }
    }
}