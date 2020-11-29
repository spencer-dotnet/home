using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Home.Shared.DAL.Models
{
    public enum ExpenseCategory
    {
        Food = 0,
        Rent= 1,
        Utilities = 2,
        Bills =3,
        Shopping = 4,
        Transportation = 5,
        Insurance = 6,
        Medical = 7,
        School = 12,
        Clothing = 8,
        Tithing = 9,
        Charity = 10,
        Other = 11,

        Salary = 112,
        Interest = 113,
        Business = 114,
        Gifts = 115
    }

    public enum ExpenseType
    {
        Income,
        Expense
    }

    public class Expense
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public ExpenseCategory Category { get; set; }
        public string PaymentMode { get; set; }
        public ExpenseType Type { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }

        public override string ToString()
        {
            return $"{Id}, {Date}, {Category}, {PaymentMode}, {Type}, {Description}, {Amount}";
        }

    }
}
