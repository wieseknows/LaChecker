using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models {
    public class Language {
        public string Name { get; set; }
        public Dictionary<string, int> Bigrams { get; set; }
        public int TotalBigramsCount { get; set; }

        public string AlphabetPattern { get; set; }

        public Language(string name, Dictionary<string, int> bigrams, string alphabetPattern) {
            this.Name = name;
            this.Bigrams = bigrams;
            this.AlphabetPattern = alphabetPattern;
            foreach (var bigram in Bigrams) {
                TotalBigramsCount += bigram.Value;
            }
        }
    }
}