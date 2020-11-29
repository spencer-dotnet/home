using Home.Shared.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Home.Client
{
    public class ExpenseStateContainer
    {
        public List<Expense> Expenses { get; private set; }

        public Expense ExpenseToEdit { get; private set; }

        public event Action OnChange;

        public void SetExpenses(List<Expense> value)
        {
            Expenses = value;
            NotifyStateChanged();
        }

        public void SetExpenseToEdit(Expense value)
        {
            ExpenseToEdit = value;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();

    }
}
