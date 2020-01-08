using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WalletServices.BudgetManagementService;
using WalletServices.Entities;

namespace Wallet.Controllers
{
    public class ExpensesController : Controller
    {

        private readonly IWalletService _context;

        public ExpensesController(IWalletService context)
        {
            _context = context;
        }

        public IActionResult Index(string searchString, string expenseType)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentFilterType"] = expenseType;

            IEnumerable<Expense> expenses;

            expenses = _context.GetExpenses();

            if (expenseType != null)
            {
                expenses = _context.GetExpenses().Where(i => i.ExpenseType.ToString() == expenseType);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                expenses = expenses.Where(a => a.Comment.Contains(searchString));
            }

            return View(expenses);
        }
    }
}