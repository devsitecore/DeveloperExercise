// <copyright file="NoSqlDatabaseCacheStorage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AirportsFeedReader.Data
{
    using System;
    using System.Data;
    using System.Data.SQLite;
    using System.IO;
    using System.Reflection;
    using Foundation.Contracts;
    using Foundation.Model;

    public class NoSqlDatabaseCacheStorage : ICacheStorage
    {
        protected virtual string DataTableName
        {
            get
            {
                return "NoSqlCache";
            }
        }

        protected virtual string SQLiteConnectionString
        {
            get
            {
                return string.Format("Data Source={0};Version=3", this.SQLiteFilePath);
            }
        }

        protected virtual string SQLiteFilePath
        {
            get
            {
                var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["NoSqlCacheDatabaseConnectionString"]?.ConnectionString;
                connectionString = connectionString.Replace("~", ApplicationRootDirectory());
                return connectionString;
            }
        }

        public virtual void Initialize()
        {
            if (!File.Exists(this.SQLiteFilePath))
            {
                SQLiteConnection.CreateFile(this.SQLiteFilePath);

                using (var connection = new SQLiteConnection(this.SQLiteConnectionString))
                {
                    connection.Open();

                    var sql = "create table " + this.DataTableName + " (cachedate datetime, cachedata nvarchar(100))";

                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public virtual CacheResult<string> GetData()
        {
            this.Initialize();

            var result = new CacheResult<string>();
            result.CacheDate = DateTime.MinValue;
            result.Data = string.Empty;

            using (var connection = new SQLiteConnection(this.SQLiteConnectionString))
            {
                connection.Open();
                var sql = "select * from " + this.DataTableName;

                using (var command = new SQLiteCommand(sql, connection))
                {
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var stringDate = reader.GetString(0);
                            result.Data = reader.GetString(1);

                            DateTime date = DateTime.MinValue;

                            if (DateTime.TryParse(stringDate, out date))
                            {
                                result.CacheDate = date;
                            }
                        }
                    }
                }

                connection.Close();
            }

            return result;
        }

        public virtual void ClearData()
        {
            using (var connection = new SQLiteConnection(this.SQLiteConnectionString))
            {
                connection.Open();

                var sql = "delete from " + this.DataTableName;

                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }

        public virtual bool SaveDate(string data)
        {
            this.ClearData();

            using (var connection = new SQLiteConnection(this.SQLiteConnectionString))
            {
                connection.Open();

                data = data.Replace("'", "''");

                var sql = "insert into " + this.DataTableName + " (cachedate, cachedata) values ('" + DateTime.UtcNow + "', '" + data + "')";

                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

            return true;
        }

        private static string ApplicationRootDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);

            path = Path.GetDirectoryName(path);
            path = Path.GetDirectoryName(path);

            return path;
        }
    }
}
