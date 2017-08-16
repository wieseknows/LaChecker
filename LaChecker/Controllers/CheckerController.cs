using System.Web.Mvc;
using LaChecker.Models;
using LaChecker.ViewModels.Checker;

namespace LaChecker.Controllers
{
    public class CheckerController : Controller
    {
        private User user = new User();
        private TopUserFactory topUserFactory = new TopUserFactory();

        
        public ActionResult Index() {
            if ((Session["__User"]) == null) {
                return RedirectToAction(actionName: "Login", controllerName: "Home");
            } else {
                user = (Session["__User"] as User);
                Session["__User"] = null;
                return View(new IndexViewModel { currentUser = user, topUsers = topUserFactory.Top10Users() });
            }
        }
    }
}
