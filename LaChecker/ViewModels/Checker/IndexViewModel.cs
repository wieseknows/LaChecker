using LaChecker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaChecker.ViewModels.Checker {
    public class IndexViewModel {
        public User currentUser{ get; set; }
        public List<TopUser> topUsers { get; set; }
    }
}