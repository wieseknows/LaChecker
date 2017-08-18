using System.Web.Mvc;
using WebAPI.Models.DbModels;
using WebAPI.Models.DbFactories;

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
                return View(user);
            }
        }
    }
}
