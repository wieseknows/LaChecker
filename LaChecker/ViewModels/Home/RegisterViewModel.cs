﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LaChecker.Models;

namespace LaChecker.ViewModels {
    public class RegisterViewModel {
        [Required]
        public string Password { get; set; }
        public User user { get; set; }
    }
}