using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;

namespace LaChecker.Helpers {
    public abstract class Factory<T> where T : new() {

        string tableName = typeof(T).Name.ToString();

        public void UpdateAll(T obj) {
            string query = "UPDATE " + tableName + " SET ";

            List<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            
            string objId = properties[0].GetValue(obj).ToString();
            
            properties = properties.Slice(1, properties.Count);
            
            List<string> values = properties
                                  .Select(x => x.GetValue(obj).ToString())
                                  .ToList();

            List<string> names = properties
                        .Select(x => x.Name)
                        .ToList();

            for (int i = 0; i < properties.Count; i++) {
                query += names[i] + " = '" + values[i] + "'";
                if (i != names.Count - 1) {
                    query += ",";
                }
            }

            query += " WHERE Id = " + objId;

            using (var conn = new SQLiteConnection(Settings.ConnectionString)) {
                conn.Open();
                using (var cmd = new SQLiteCommand(conn)) {
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
            };
        }

        public void UpdateFields(T obj, List<string> fields, List<string> values) {
            string query = "UPDATE " + tableName + " SET ";

            List<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            string objId = properties[0].GetValue(obj).ToString();

            for (int i = 0; i < fields.Count; i++) {
                query += fields[i] + " = '" + values[i] + "'";
                if (i != fields.Count - 1) {
                    query += ",";
                }
            }

            query += " WHERE Id = " + objId;

            using (var conn = new SQLiteConnection(Settings.ConnectionString)) {
                conn.Open();
                using (var cmd = new SQLiteCommand(conn)) {
                    cmd.CommandText = query;
                    cmd.ExecuteNonQuery();
                }
            };
        }



        public string Insert(T obj) {
            string left = "INSERT INTO " + tableName + " (";
            string right = "VALUES (";

            List<PropertyInfo> properties = typeof(T).GetProperties().ToList();
            
            properties = properties.Slice(1, properties.Count);
            
            List<string> values = properties
                                  .Select(x => x.GetValue(obj)?.ToString())
                                  .ToList();

            List<string> names = properties
                        .Select(x => x.Name)
                        .ToList();

            for (int i = 0; i < properties.Count; i++) {
                left += "`" + names[i] + "`";
                right += "'" + values[i] + "'";

                var t = ",";
                if (i == names.Count - 1) {
                    t = ")";
                }

                left += t;
                right += t;
            }

            string id = String.Empty;
            var query = left + right + "; select last_insert_rowid();";
            using (var conn = new SQLiteConnection(Settings.ConnectionString)) {
                conn.Open();
                using (var cmd = new SQLiteCommand(conn)) {
                    cmd.CommandText = query;
                    id = Convert.ToString(cmd.ExecuteScalar());
                }
            };
            return id;
        }


        public List<T> GetAll(string whereClause = "", string fullSqlQuery = "") {
            List<T> result = new List<T>();
            using (SQLiteConnection conn = new SQLiteConnection(Settings.ConnectionString)) {
                conn.Open();
                using (var cmd = new SQLiteCommand(conn)) {
                    cmd.CommandText = "SELECT * FROM " + tableName + " " +  whereClause;
                    if (!String.IsNullOrEmpty(fullSqlQuery)) {
                        cmd.CommandText = fullSqlQuery;
                    }
                    using (var reader = cmd.ExecuteReader()) {
                        if (reader.HasRows) {
                            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                            while (reader.Read()) {
                                var obj = new T();
                                foreach (var property in properties) {
                                    var columnIndex = reader.GetOrdinal(property.Name);
                                    if (!reader.IsDBNull(columnIndex))
                                        property.SetValue(obj, Convert.ToString(reader.GetValue(columnIndex)));
                                }
                                result.Add(obj);
                            }
                        }
                    }
                }
            };
            return result;
        }



        public void DeleteById(int id) {
            using (SQLiteConnection conn = new SQLiteConnection(Settings.ConnectionString)) {
                conn.Open();
                using (var cmd = new SQLiteCommand(conn)) {
                    cmd.CommandText = "DELETE FROM " + tableName + " WHERE Id = " + id.ToString();
                    cmd.ExecuteNonQuery();
                }
            };
        }



        public T GetBy(string value, string field = "Id") {
            T obj = new T();
            using (SQLiteConnection conn = new SQLiteConnection(Settings.ConnectionString)) {
                conn.Open();
                using (var cmd = new SQLiteCommand(conn)) {
                    cmd.CommandText = "SELECT * FROM " + tableName
                        + " WHERE " + field + " = '" + value + "'";

                    using (var reader = cmd.ExecuteReader()) {
                        if (reader.HasRows) {
                            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                            while (reader.Read()) {
                                foreach (var property in properties) {
                                    var columnIndex = reader.GetOrdinal(property.Name);
                                    if (!reader.IsDBNull(columnIndex))
                                        property.SetValue(obj, Convert.ToString(reader.GetValue(columnIndex)));
                                }
                            }
                        }
                    }
                }
            };
            return obj;
        }
    }
}