using LaChecker.Helpers;
using LaChecker.Models.DbFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;

namespace LaChecker.Models {
    public class Probabilities {
        public string Id { get; set; }
        public string English { get; set; }
        public string Spanish { get; set; }
        public string Portuguese { get; set; }
        public string Russian { get; set; }
        public string Bulgarian { get; set; }
    }
}