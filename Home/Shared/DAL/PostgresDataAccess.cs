using Dapper;
using Home.Shared.DAL.Helpers;
using Home.Shared.DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Home.Shared.DAL
{
    public class PostgresDataAccess : IPostgresDataAccess
    {
        private readonly IConfiguration _config;

        public readonly string DATABASE_URL_NAME = "DATABASE_URL";

        public string _connectionString;

        public PostgresDataAccess(IConfiguration config)
        {
            _config = config;

            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (env == "Development")
            {
                _connectionString = _config["PostgresConnectionString"];
            }
            else
            {
                string herokuPostgresURL = Environment.GetEnvironmentVariable(DATABASE_URL_NAME);

                _connectionString = PostgresConnectionHelper.ParseConnectionURL(herokuPostgresURL);
            }

        }

        public async Task<List<T>> LoadData<T, U>(string sql, U parameters)
        {
            string connectionString = _connectionString;

            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                var data = await connection.QueryAsync<T>(sql, parameters);

                return data.ToList();
            }
        }

        // TODO: Load one data?

        public async Task<int> SaveData<T>(string sql, T parameters)
        {
            string connectionString = _connectionString;

            using (IDbConnection connection = new NpgsqlConnection(connectionString))
            {
                var rowsChanged = await connection.ExecuteAsync(sql, parameters);

                return rowsChanged;
            }
        }
    }
}
