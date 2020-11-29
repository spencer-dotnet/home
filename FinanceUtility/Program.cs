using Home.Shared.DAL.Models;
using Microsoft.VisualBasic.FileIO;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;

namespace FinanceUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\spenc\Documents\Finances\Expenses.csv";

            string FileContents = File.ReadAllText(@"C:\Users\spenc\Documents\Finances\Expenses.csv");

            //Console.WriteLine(FileContents);

            List<Expense> expenses = new List<Expense>();

            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();

                    if(fields[0] == "Date")
                    {
                        continue;
                    }

                    string[] dateParts = fields[0].Split("/");
                    DateTime newDate = new DateTime(int.Parse(dateParts[2]), int.Parse(dateParts[0]), int.Parse(dateParts[1]));
                    string newDescription = fields[1];
                    decimal newAmount = decimal.Parse(fields[2]);

                    ExpenseType newType = (newAmount > 0) ? ExpenseType.Income :  ExpenseType.Expense;

                    string newPaymentMode = "Debit Card";
                    if (newType == ExpenseType.Expense)
                    {
                        if (newDescription.Contains("Venmo", StringComparison.OrdinalIgnoreCase))
                        {
                            newPaymentMode = "Venmo";
                        }
                    }
                    else
                    {
                        newPaymentMode = null;
                    }

                    Expense newExpense = new Expense()
                    {
                        Date = newDate,
                        Description = newDescription,
                        Amount = newAmount,
                        Type = newType,
                        PaymentMode = newPaymentMode,
                        Category = ExpenseCategory.Other
                    };

                    expenses.Add(newExpense);
                }
            }

            var mongo = new MongoClient("mongodb+srv://family_user:l3AnWI74AmUkQzzN@cluster0.t4it5.mongodb.net/family?retryWrites=true&w=majority");

            var db = mongo.GetDatabase("family");
            var collection = db.GetCollection<Expense>("expense");
            collection.InsertMany(expenses);



        }
    }
}
