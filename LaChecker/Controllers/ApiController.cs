using LaChecker.Models;
using LaChecker.Models.DbFactories;
using System.Web.Mvc;

namespace LaChecker.Controllers
{
    public class ApiController : Controller
    {
        TopUserFactory topUserFactory = new TopUserFactory();
        ProbabilitiesFactory probabilitiesFactory = new ProbabilitiesFactory();

        [HttpPost]
        public JsonResult IdentifyWord(string word, string userId) {
            Request request = new Request();
            request.Identify(word, userId);
            Probabilities probabilities = probabilitiesFactory.GetBy(request.ProbabilitiesId, "Id");
            return Json(probabilities);
        }

        [HttpPost]
        public JsonResult GetTopUsers() {
            return Json(topUserFactory.Top10Users());
        }
    }
}