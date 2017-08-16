using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LaChecker.Helpers;
using System.Data.SQLite;
using System.Reflection;

namespace LaChecker.Models {
    public class UserFactory : Factory<User> {
        public User GetByNickName(string nickname) {
            return base.GetBy(nickname, "Nickname");
        }
    }
}