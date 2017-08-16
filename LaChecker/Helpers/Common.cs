using LaChecker.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LaChecker.Helpers {
    public static class Common {
        private static Random rand = new Random();

        public static DateTime GetRandomDateTime() {
            int year = 2017;
            int month = 8;
            int day = rand.Next(15, 31);
            int hour = rand.Next(0, 24);
            int minute = rand.Next(0, 60);
            int second = rand.Next(0, 60);
            return new DateTime(year, month, day, hour, minute, second);
        }

        private static string GetRandomString() {
            string latinAlphabet = "abcdefghijklmnopqrstuvwxyz";
            string cyrillicAlphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

            string input = (rand.Next(1, 11) > 5) ? latinAlphabet : cyrillicAlphabet;

            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < rand.Next(4, 15); i++) {
                ch = input[rand.Next(0, input.Length)];
                builder.Append(ch);
            }
            return builder.ToString();
        }


        public static void GenerateTestData() {
            UserFactory userFactory = new UserFactory();
            List<User> allUsers = userFactory.GetAll();
            foreach (var user in allUsers) {
                for (int i = 0; i < rand.Next(20, 30); i++) {
                    var randomWord = GetRandomString();
                    Request request = new Request();
                    request.Identify(randomWord, user.Id);
                }
            }
        }
    }
}