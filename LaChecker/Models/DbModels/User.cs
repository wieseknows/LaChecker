using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaChecker.Models {
    public class User {
        public string Id { get; set; }

        [Required]
        public string Nickname { get; set; }

        [Required]
        public string Password { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string LastLogIn { get; set; }

    }
}