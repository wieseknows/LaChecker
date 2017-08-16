using LaChecker.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaChecker.Models {
    public class TopUserFactory : Factory<TopUser> {

        RequestFactory requestFactory = new RequestFactory();
        public List<TopUser> Top10Users() {
            string query = "select User.*, Count(Request.UserId) as TotalRequests " +
                           "from User join Request on User.Id = Request.UserId " +
                           "group by Request.UserId " +
                           "order by Count(Request.UserId) DESC " +
                           "limit 10";

            var topUsers = base.GetAll(fullSqlQuery: query);
            foreach (var topUser in topUsers) {
                topUser.AvgTimeBetweenRequests = requestFactory.AvgTimeBetweenRequests(topUser.Id);
            }

            return topUsers;

        }
    }
}