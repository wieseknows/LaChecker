using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAPI.Helpers {
    public static class Extensions {
        public static List<T> Slice<T>(this List<T> list, int start, int finish) {
            var result = new List<T>();
            for (int i = start; i < finish; i++) {
                result.Add(list[i]);
            }
            return result;
        }

        public static string CustomSubstring(this string str, int start, int finish) {
            string result = "";
            for (int i = start; i < finish; i++) {
                result += str[i];
            }
            return result;
        }

        public static int GetAverage(this List<DateTime> datetimes) {
            // MORE than 2 datetimes in total
            if (datetimes.Count >= 2) {
                List<double> differences = new List<double>();
                for (int i = 1; i < datetimes.Count; i++) {
                    differences.Add((datetimes[i] - datetimes[i - 1]).TotalSeconds);
                }

                double total = differences.Take(differences.Count).Sum();

                var avg = Math.Round(total / differences.Count, 2);
                return Convert.ToInt32(avg);
            // ONLY 2 requests 
            } else if (datetimes.Count == 2) {
                var avg = (datetimes[1] - datetimes[0]).TotalSeconds;
                return Convert.ToInt32(avg);
            // Any other case (1 or 0 requests)
            } else {
                return 0;
            }
        }
    }
}