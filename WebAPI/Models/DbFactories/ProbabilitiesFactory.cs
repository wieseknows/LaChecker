using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using WebAPI.Helpers;
using WebAPI.Models.DbModels;

namespace WebAPI.Models.DbFactories {
    public class ProbabilitiesFactory : Factory<Probabilities> {
        private CachedFactory cachedFactory = new CachedFactory();

        private Probabilities CalculateProbabilities(string word) {
            Probabilities result = new Probabilities();
            Dictionary<string, double> dict = new Dictionary<string, double>();
            foreach (var language in Settings.Languages) {
                Regex regex = new Regex(language.AlphabetPattern);
                double languageProbability = regex.IsMatch(word) ? 1.0 : 0.0;
                for (int i = 0; i < word.Length - 1; i++) {
                    var curBigram = String.Format("{0}{1}", word[i], word[i + 1]);
                    if (language.Bigrams.ContainsKey(curBigram)) {
                        languageProbability += ((double)(language.Bigrams[curBigram]) / (double)(language.TotalBigramsCount));
                    }
                }
                dict.Add(language.Name, languageProbability);
            }
            double totalSum = 0.0;
            foreach (var item in dict) {
                totalSum += item.Value;
            }
            for (int i = 0; i < dict.Count; i++) {
                dict[Settings.Languages[i].Name] = Math.Round((dict[Settings.Languages[i].Name] / totalSum) * 100, 4);
            }

            result.English = dict["English"].ToString();
            result.Spanish = dict["Spanish"].ToString();
            result.Portuguese = dict["Portuguese"].ToString();
            result.Russian = dict["Russian"].ToString();
            result.Bulgarian = dict["Bulgarian"].ToString();
            result.Id = base.Insert(result);

            return result;
        }

        public Probabilities GetProbabilitiesForWord(string word) {
            Cached cached = cachedFactory.GetBy(word, "Word");

            if (cached.ProbabilitiesId != null) {
                return base.GetBy(cached.ProbabilitiesId, "Id");
            } else {
                Probabilities calculatedProbabilities = CalculateProbabilities(word);
                cached.Word = word;
                cached.ProbabilitiesId = calculatedProbabilities.Id;
                cachedFactory.Insert(cached);

                return calculatedProbabilities;
            }
        }
    }
}