using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;

namespace WebAPI.Models.DbModels {
    public class Probabilities {
        public string Id { get; set; }
        public string English { get; set; }
        public string Spanish { get; set; }
        public string Portuguese { get; set; }
        public string Russian { get; set; }
        public string Bulgarian { get; set; }
    }
}