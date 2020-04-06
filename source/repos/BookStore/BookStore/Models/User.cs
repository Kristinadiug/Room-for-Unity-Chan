using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace Shop.Data.Models
{
    public class User
    {
        public int Id { set; get; }
        [Required]
        [Remote("CheckUsername", "Validation")]
        public string Name { set; get; }
        [Range(18, 100, ErrorMessage ="You must be adult")]
        public int Age { set; get; }
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Invalid Email")]
        public string Email { set; get; }
        [Required]
        [Remote("CheckPassword", "Validation")]
        public string Password { set; get; }
        [Required]
        [Compare("Password", ErrorMessage = "Passwords are not same")]
        public string PasswordConfirm { set; get; }
      
        public String Role { set; get; }
    }
}
