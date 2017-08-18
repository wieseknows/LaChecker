using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebAPI.Helpers;
using WebAPI.Models.DbFactories;
using WebAPI.Models.DbModels;

namespace WebAPI.Controllers
{
    //[EnableCors(origins: "http://localhost:30118", headers: "*", methods: "*")]
    public class IdentificationController : ApiController
    {
        private ProbabilitiesFactory probabilitiesFactory = new ProbabilitiesFactory();
        private RequestFactory requestFactory = new RequestFactory();

        // GET: api/Identification
        public IEnumerable<string> Get()
        {
            return new string[] { "controller", "Identification" };
        }

        [HttpPost]
        public Probabilities Post([FromBody]IdentificationModel model) {
            if (Settings.Languages == null) {
                Settings.GetStatisticsOfLanguages();
            }

            Request request = new Request();
            request.Identify(model.word, model.userId.ToString());
            Probabilities probabilities = probabilitiesFactory.GetBy(request.ProbabilitiesId, "Id");
            return probabilities;
        }
    }

    public class IdentificationModel {
        public string word { get; set; }
        public int userId { get; set; }
    }
}
