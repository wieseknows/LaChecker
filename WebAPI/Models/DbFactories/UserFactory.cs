using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using System.Reflection;
using WebAPI.Models.DbModels;

namespace WebAPI.Models.DbFactories {
    public class UserFactory : Factory<User> {
        public User GetByNickName(string nickname) {
            return base.GetBy(nickname, "Nickname");
        }
    }
}