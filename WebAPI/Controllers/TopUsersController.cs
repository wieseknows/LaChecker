using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;
using WebAPI.Models.DbFactories;

namespace WebAPI.Controllers
{
    public class TopUsersController : ApiController {

        TopUserFactory topUserFactory = new TopUserFactory();
        public List<TopUser> Get() {
            return topUserFactory.Top10Users();
        }
    }
}
