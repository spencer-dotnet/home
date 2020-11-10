using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Home.Shared.DAL.Models;
using Home.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.IO;

namespace Home.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly ExpenseService _expenseService;

        public ExpenseController (ExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet]
        public ActionResult<List<Expense>> Get() =>
            _expenseService.Get();

        [HttpGet("{id:length(24)}", Name = "GetExpense")]
        public ActionResult<Expense> Get(string id)
        {
            var expense = _expenseService.Get(id);

            if (expense == null)
            {
                return NotFound();
            }

            return expense;
        }

        [HttpPost]
        public ActionResult<Expense> Create(Expense expense)
        {
            _expenseService.Create(expense);

            return CreatedAtRoute("GetExpense", new { id = expense.Id.ToString() }, expense);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Expense expenseIn)
        {
            var expense = _expenseService.Get(id);

            if (expense == null)
            {
                return NotFound();
            }

            _expenseService.Update(id, expenseIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var expense = _expenseService.Get(id);

            if(expense == null)
            {
                return NotFound();
            }

            _expenseService.Remove(expense.Id);

            return NoContent();
        }
    }

}
