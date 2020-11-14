using Home.Shared.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Home.Client
{
    public class ExpenseStateContainer
    {
        public List<Expense> Expenses { get; set; }

        public event Action OnChange;

        public void SetExpenses(List<Expense> value)
        {
            Expenses = value;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

    }
}
