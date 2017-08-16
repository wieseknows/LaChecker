using LaChecker.Models;
using LaChecker.Models.DbFactories;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace LaChecker.Helpers {
    public static class Settings {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["LiteConnection"].ConnectionString;

        public static string SQLiteDateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        private static Dictionary<string, string> LangPatterns = new Dictionary<string, string>() {
            { "English", "[^a-zA-Z ]" },
            { "Spanish", "[^a-zA-Z ]" },
            { "Portuguese", "[^a-zA-Z ]" },
            { "Russian", "[^а-яА-Я ]" },
            { "Bulgarian", "[^а-яА-Я ]" }
        };

        public static List<Language> Languages;

        public static void GetStatisticsOfLanguages() {
            Languages = new List<Language>();

            foreach (var pattern in LangPatterns) {
                var allText = System.IO.File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "App_Data/Language_Samples/" + pattern.Key + ".txt");
                allText = new Regex(pattern.Value).Replace(allText, "");

                Dictionary<string, int> bigramsForCurrentLanguage = new Dictionary<string, int>();
                List<string> words = allText.Split(' ')
                                            .Select(word => word.ToLower())
                                            .Where(word => word.Length >= 4)
                                            .ToList();

                foreach (var word in words) {
                    for (int i = 0; i < word.Length - 1; i++) {
                        var bigram = String.Format("{0}{1}", word[i], word[i + 1]);
                        if (bigramsForCurrentLanguage.ContainsKey(bigram)) {
                            bigramsForCurrentLanguage[bigram]++;
                        } else {
                            bigramsForCurrentLanguage.Add(bigram, 0);
                        }
                    }
                }
                bigramsForCurrentLanguage = bigramsForCurrentLanguage.OrderByDescending(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
                Language language = new Language(pattern.Key, bigramsForCurrentLanguage, pattern.Value.Replace("^", ""));
                
                Languages.Add(language);
            }
        }
    }
}