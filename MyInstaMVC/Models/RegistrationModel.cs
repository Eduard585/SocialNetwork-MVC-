using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace MyInstaMVC.Models
{
    public class RegistrationModel
    {
        [Required]
        [Display(Name = "Логин")]
        [EmailAddress(ErrorMessage = "Не тот адрес ты ввел")]
        public string LoginName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "No equal")]
        [DataType(DataType.Password)]
        public string RetryPassword { get; set; }
        [Required]
        public string NickName { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public System.DateTime BirthDate { get; set; }
        [Required]
        public bool SharedProfile { get; set; }

    }
}