﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MyInstaMVC.Models
{
    public class LoginModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
    }

    public class CustomSerializeModel
    {
        public long UserId { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
        public List<string> RoleName { get; set; }
    }
}