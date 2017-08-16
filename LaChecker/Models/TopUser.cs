using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaChecker.Models {
    public class TopUser : User {
        public string TotalRequests { get; set; }

        public string AvgTimeBetweenRequests { get; set; }
    }
}