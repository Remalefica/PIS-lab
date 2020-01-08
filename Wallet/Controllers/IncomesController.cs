using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WalletServices.BudgetManagementService;
using WalletServices.Entities;

namespace Wallet.Controllers
{
    public class IncomesController : Controller
    {

        private readonly IWalletService _context;

        public IncomesController(IWalletService context)
        {
            _context = context;
        }

        public IActionResult Index(string searchString, string incomeType)
        {
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentFilterType"] = incomeType;

            IEnumerable<Income> incomes;

            incomes = _context.GetIncomes();

            if (incomeType != null)
            {
                incomes = _context.GetIncomes().Where(i => i.IncomeType.ToString() == incomeType);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                incomes = incomes.Where(a => a.Comment.Contains(searchString));
            }

            return View(incomes);
        }
    }
}