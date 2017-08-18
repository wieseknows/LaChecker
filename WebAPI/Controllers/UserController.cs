using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Helpers;
using WebAPI.Models.DbFactories;
using WebAPI.Models.DbModels;

namespace WebAPI.Controllers
{
    public class UserController : ApiController
    {
        UserFactory userFactory = new UserFactory();

        // Get all users
        // GET: api/User
        public IEnumerable<User> GetAllUsers() {
            return userFactory.GetAll();
        }

        // Login
        // GET: api/User/epifancev
        public User GetByNickname(string nickname) {
            var user = userFactory.GetByNickName(nickname);
            if (user.Id == null) {
                return null;
            } else {
                userFactory.UpdateFields(user, new List<string> { "LastLogIn" }, new List<string> { DateTime.Now.ToString(Settings.SQLiteDateTimeFormat) });
                return user;
            }
        }

        // Register
        // POST: api/User
        public User Post([FromBody]User user) {
            userFactory.UpdateFields(user, new List<string> { "LastLogIn" }, new List<string> { DateTime.Now.ToString(Settings.SQLiteDateTimeFormat) });

            string insertedUserId = userFactory.Insert(user);
            User insertedUser = userFactory.GetBy(insertedUserId);
            return insertedUser;
        }
    }
}
