using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WalletServices.BudgetManagementService;
using WalletServices.Entities;

namespace Wallet.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IWalletService _context;

        public AccountsController(IWalletService context)
        {
            _context = context;
        }

        // GET: Accounts
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            IEnumerable<Account> accounts;

            accounts = _context.GetAccounts();

            if (!string.IsNullOrEmpty(searchString))
            {
                accounts = accounts.Where(a => a.Name.Contains(searchString));
            }

            ViewBag.Incomes = _context
                .GetIncomeStatistics()
                .Select(p => new { y = p.Value, indexLabel = p.Key });
            ViewBag.Expenses = _context.GetExpenseStatistics()
                .Select(p => new { y = p.Value, indexLabel = p.Key });

            return View(accounts);
        }

        public ActionResult Doughnut()
        {
            return View();
        }


    }
}
