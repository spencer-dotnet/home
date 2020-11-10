using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Home.Shared.DAL.Models
{
    public enum ExpenseCategory
    {
        Food,
        Rent,
        Utilities,
        Bills,
        Shopping,
        Transportation,
        Insurance,
        Medical,
        Clothing,
        Tithing,
        Charity,
        Other,

        Salary,
        Interest,
        Business,
        Gifts
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
