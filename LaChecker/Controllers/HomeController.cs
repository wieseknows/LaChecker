using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebAPI.Models.DbFactories;
using WebAPI.Models.DbModels;
using System.Net.Http;
using LaChecker.ViewModels;

namespace LaChecker.Controllers
{
    public class HomeController : Controller {

        private UserFactory userFactory = new UserFactory();
        private RequestFactory requestFactory = new RequestFactory();
        private HttpClient httpClient = new HttpClient();

        public ActionResult Login() {
            if (Session["__User"] != null) {
                return RedirectToAction("Index", "Checker");
            }
            return View();
        }


        [HttpPost]
        public ActionResult Login(User user) {
            var response = httpClient.GetAsync("http://localhost:1833/api/user/" + user.Nickname).Result;
            var foundUser = response.Content.ReadAsAsync<User>().Result;

            if (foundUser == null) {
                return RedirectToAction("Register", user);
            }
            if (!foundUser.Password.Equals(user.Password)) {
                ViewBag.ErrorMessage = "Password is not correct";
                return View();
            // GOT USER WITH ALREADY UPDATED `LASTLOGIN` FIELD
            } else {
                Session["__User"] = foundUser;
                return RedirectToAction("Index", "Checker");
            }
        }


        public ActionResult Register(User user) {
            RegisterViewModel model = new RegisterViewModel { user = user, Password = null};
            return View(model);
        }


        [HttpPost]
        public ActionResult Register(RegisterViewModel model) {
            if (model.Password.Equals(model.user.Password)) {
                var response = httpClient.PostAsJsonAsync("http://localhost:1833/api/user", model.user).Result;
                User registeredUser = response.Content.ReadAsAsync<User>().Result;

                Session["__User"] = registeredUser;
                return RedirectToAction(actionName: "Index", controllerName: "Checker");
            } else {
                ViewBag.ErrorMessage = "Passwords don't match";
                return View();
            }
        }
    }
}
