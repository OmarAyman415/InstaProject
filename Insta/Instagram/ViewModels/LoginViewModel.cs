﻿using System.ComponentModel.DataAnnotations;

namespace Instagram.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}