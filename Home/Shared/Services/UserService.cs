using Home.Shared.DAL.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Home.Shared.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        private readonly IConfiguration _config;

        public UserService(IConfiguration config)
        {
            _config = config;

            string connectionString = "";

            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (env == "Development")
            {
                connectionString = _config["FamilyMongoDatabaseSettings:ConnectionString"];
            }
            else
            {
                connectionString = Environment.GetEnvironmentVariable("MongoConnectionString");
            }

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("family");

            _users = database.GetCollection<User>("users");
        }

        public List<User> Get() =>
            _users.Find(user => true).ToList();

        public User Get(string email) =>
            _users.Find<User>(user => user.EmailAddress == email).FirstOrDefault();

        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public void Update(string id, User userIn) =>
            _users.ReplaceOne(user => user.Id == id, userIn);

        public void Remove(User userIn) =>
            _users.DeleteOne(e => e.Id == userIn.Id);

        public void Remove(string id) =>
            _users.DeleteOne(e => e.Id == id);
    }
}
