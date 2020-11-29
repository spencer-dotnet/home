using Home.Shared.DAL.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Home.Shared.Services
{
    public class ExpenseService
    {
        private readonly IMongoCollection<Expense> _expenses;

        private readonly IConfiguration _config;

        public ExpenseService(IConfiguration config)
        {
            _config = config;

            string connectionString = "";

            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            //connectionString = _config["FamilyMongoDatabaseSettings:ConnectionString"];
            connectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING");


            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("family");

            _expenses = database.GetCollection<Expense>("expense");
        }

        public List<Expense> Get() =>
            _expenses.Find(expense => true).ToList();

        public Expense Get(string id) =>
            _expenses.Find<Expense>(expense => expense.Id == id).FirstOrDefault();

        public Expense Create(Expense expense)
        {
            _expenses.InsertOne(expense);
            return expense;
        }

        public IEnumerable<Expense> CreateMany(IEnumerable<Expense> expensesToAdd)
        {
            _expenses.InsertMany(expensesToAdd);
            return expensesToAdd;
        }

        public void Update(string id, Expense expenseIn) =>
            _expenses.ReplaceOne(expense => expense.Id == id, expenseIn);

        public void Remove(Expense expenseIn) =>
            _expenses.DeleteOne(e => e.Id == expenseIn.Id);

        public void Remove(string id) =>
            _expenses.DeleteOne(e => e.Id == id);
    }
}
