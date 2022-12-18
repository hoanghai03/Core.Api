﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Model
{
    public class LoginModel
    {
        [Required(ErrorMessage = "UserName is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Password { get; set; }
    }
}
