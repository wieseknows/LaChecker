using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LaChecker.Helpers;
using LaChecker.Models;
using LaChecker.ViewModels;

namespace LaChecker.Controllers
{
    public class HomeController : Controller {

        private UserFactory userFactory = new UserFactory();
        private RequestFactory requestFactory = new RequestFactory();

        public ActionResult Login() {
            if (Settings.Languages == null) {
                Settings.GetStatisticsOfLanguages();
            }
            if (Session["__User"] != null) {
                return RedirectToAction("Index", "Checker");
            }
            return View();
        }


        [HttpPost]
        public ActionResult Login(User user) {
            User foundUser = userFactory.GetByNickName(user.Nickname);
            // User wasn't found
            if (foundUser.Id == null) {
                return RedirectToAction("Register", user);
            }
            // User was found but password is incorrect
            if (!foundUser.Password.Equals(user.Password)) {
                ViewBag.ErrorMessage = "Password is not correct";
                return View();
            // Everything is OK
            } else {
                // Update LastLogIn field
                userFactory.UpdateFields(foundUser, new List<string> { "LastLogIn" }, new List<string> { DateTime.Now.ToString(Settings.SQLiteDateTimeFormat) });
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
                // Update LastLogIn field
                model.user.LastLogIn = DateTime.Now.ToString(Settings.SQLiteDateTimeFormat);
                // Add to database
                model.user.Id = userFactory.Insert(model.user);
                Session["__User"] = model.user;
                return RedirectToAction(actionName: "Index", controllerName: "Checker");
            } else {
                ViewBag.ErrorMessage = "Passwords don't match";
                return View();
            }
        }
    }
}
