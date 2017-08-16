using System;
using System.Collections.Generic;
using LaChecker.Helpers;
using System.Data.SQLite;

namespace LaChecker.Models {
    public class RequestFactory : Factory<Request> {

        public string AvgTimeBetweenRequests(string userId) {
            List<DateTime> requestsTime = new List<DateTime>();
            // Get all the requests done by selected user
            using (SQLiteConnection conn = new SQLiteConnection(Settings.ConnectionString)) {
                conn.Open();
                using (var cmd = new SQLiteCommand(conn)) {
                    cmd.CommandText = "SELECT Time FROM Request WHERE UserId = " + userId + " ORDER BY Time ASC";

                    using (var reader = cmd.ExecuteReader()) {
                        if (reader.HasRows) {
                            while (reader.Read()) {
                                requestsTime.Add(DateTime.Parse(reader.GetString(0)));
                            }
                        }
                    }
                }
            };
            // Get avg time as total seconds
            var avgInSeconds = requestsTime.GetAverage();
            // Generate TimeSpan from seconds we've got above
            TimeSpan avgInTimespan = new TimeSpan(0, 0, 0, avgInSeconds);

            // Generate result string
            string result = "";
            if (avgInTimespan.Days > 0) {
                result += avgInTimespan.Days + " days ";
            }
            if (avgInTimespan.Hours > 0) {
                result += avgInTimespan.Hours + " hrs ";
            }
            if (avgInTimespan.Minutes > 0) {
                result += avgInTimespan.Minutes + " min ";
            }
            if (avgInTimespan.Seconds > 0) {
                result += avgInTimespan.Seconds + " sec";
            }

            return result;
        }
          
    }
}











//public Dictionary<string, string> GetLanguageStatistics() {
//    var stats = new Dictionary<string, string>();
//    int counter = 0;
//    // Goin through all valid languages declared in Settings class
//    foreach (var language in Settings.Languages.Values) {
//        // Get all the requests for every single language
//        List<Request> requestsForLanguage = base.GetAll("WHERE Language='" + language + "'");
//        // Calculate total
//        var sum = 0.0;
//        foreach (var req in requestsForLanguage) {
//            sum += double.Parse(req.Confidence);
//        }
//        // Calculate average
//        double averageForLanguage = (sum / requestsForLanguage.Count) * 100;
//        // Check if NaN
//        if (Double.IsNaN(averageForLanguage)) {
//            averageForLanguage = 0.0;
//        } else {
//            averageForLanguage = Math.Round(averageForLanguage, 3);
//        }
//        stats.Add(language, averageForLanguage.ToString());
//        counter++;
//    }
//    return stats;
//}