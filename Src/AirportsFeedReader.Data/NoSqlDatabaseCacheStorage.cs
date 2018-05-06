// <copyright file="NoSqlDatabaseCacheStorage.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AirportsFeedReader.Data
{
    using System;
    using System.Data;
    using System.Data.SQLite;
    using System.IO;
    using Common.Extensions;
    using Foundation.Contracts;
    using Foundation.Model;

    public class NoSqlDatabaseCacheStorage : ICacheStorage
    {
        private readonly string noSqlConnectionStringName = "NoSqlCacheDatabaseConnectionString";
        private readonly string defaultCacheKey = "AirportsCacheData";
        private readonly string dataTableName = "NoSqlCache";

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
                var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[this.noSqlConnectionStringName]?.ConnectionString;
                return connectionString.ConvertToApplicationRootPath();
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

                    var sql = "create table " + this.dataTableName + " (cacheDate datetime, cacheData nvarchar(100), cacheKey varchar(50))";

                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public virtual CacheResult<string> GetData(string cacheKey = "")
        {
            this.Initialize();

            var result = new CacheResult<string>();
            result.CacheDate = DateTime.MinValue;
            result.Data = string.Empty;

            using (var connection = new SQLiteConnection(this.SQLiteConnectionString))
            {
                connection.Open();
                var sql = this.GetSelectSql(cacheKey);

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

        public virtual void ClearData(string cacheKey = "")
        {
            if (File.Exists(this.SQLiteFilePath))
            {
                using (var connection = new SQLiteConnection(this.SQLiteConnectionString))
                {
                    connection.Open();

                    var sql = string.Format("delete from {0} where cacheKey='{1}'", this.dataTableName, this.GetCacheKey(cacheKey));

                    using (var command = new SQLiteCommand(sql, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }
        }

        public virtual bool SaveDate(string data, string cacheKey = "")
        {
            this.ClearData(cacheKey);

            using (var connection = new SQLiteConnection(this.SQLiteConnectionString))
            {
                connection.Open();

                data = data.Replace("'", "''");

                var sql = string.Format("insert into {0} (cacheDate, cacheData, cacheKey) values ('{1}', '{2}', '{3}')", this.dataTableName, DateTime.UtcNow, data, this.GetCacheKey(cacheKey));

                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }

            return true;
        }

        protected virtual string GetSelectSql(string cacheKey)
        {
            return string.Format("select * from {0} where cacheKey='{1}'", this.dataTableName, this.GetCacheKey(cacheKey));
        }

        private string GetCacheKey(string cacheKey)
        {
            cacheKey = string.IsNullOrEmpty(cacheKey) ? this.defaultCacheKey : cacheKey;
            return cacheKey;
        }
    }
}
