using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WalletServices.BudgetManagementService;
using WalletServices.Entities;

namespace Wallet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseApiController : ControllerBase
    {
        private readonly IWalletService _dataService;

        public ExpenseApiController(IWalletService dataService)
        {
            _dataService = dataService;
        }

        /// <summary>
        /// Add new Expense
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Expense/Create
        ///     {
        ///        "expenseType" = [Food],
        ///        "comment" = "comment",
        ///        "accountId" = 1,
        ///        "sum" = 1
        ///     }
        ///
        /// </remarks>
        /// <returns>The new Expense</returns>
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm]int accountId, [FromForm]string expenseType, [FromForm]string comment, [FromForm]int sum)
        {
            var expense = new Expense()
            {
                ExpenseType = Enum.Parse<ExpenseTypeName>(expenseType),
                Comment = comment,
                CreatedAt = DateTime.Now,
                Sum = sum
            };

            if (ModelState.IsValid)
            {
                _dataService.AddExpense(accountId, expense);
            }

            return RedirectToAction("Index", "Accounts");
        }
    }
}