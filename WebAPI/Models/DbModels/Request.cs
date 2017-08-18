using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using WebAPI.Helpers;
using WebAPI.Models.DbFactories;

namespace WebAPI.Models.DbModels {
    public class Request {
        private RequestFactory requestFactory = new RequestFactory();
        private ProbabilitiesFactory probabilitiesFactory = new ProbabilitiesFactory();

        public string Id { get; set; }

        [Required]
        [MinLength(4)]
        public string Word { get; set; }

        public string UserId { get; set; }
        public string Time { get; set; }
        public string ProbabilitiesId { get; set; }

        public void Identify(string word, string userId) {
            this.Word = word;
            this.UserId = userId;
            this.Time = DateTime.Now.ToString(Settings.SQLiteDateTimeFormat);
            Probabilities probabilities = probabilitiesFactory.GetProbabilitiesForWord(word);
            this.ProbabilitiesId = probabilities.Id;

            requestFactory.Insert(this);
        }
    }


}