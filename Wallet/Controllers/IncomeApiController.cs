using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WalletServices.BudgetManagementService;
using WalletServices.Entities;

namespace Wallet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeApiController : ControllerBase
    {
        private readonly IWalletService _dataService;

        public IncomeApiController(IWalletService dataService)
        {
            _dataService = dataService;
        }

        /// <summary>
        /// Add new Income
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Income/Create
        ///     {
        ///        "incomeType" = [Salary],
        ///        "comment" = "comment",
        ///        "accountId" = 1,
        ///        "sum" = 1
        ///     }
        ///
        /// </remarks>
        /// <returns>The new Income</returns>
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm]int accountId, [FromForm]string incomeType, [FromForm]string comment, [FromForm]int sum)
        {
            var income = new Income()
            {
                IncomeType = Enum.Parse<IncomeTypeName>(incomeType),
                Comment = comment,
                CreatedAt = DateTime.Now,
                Sum = sum
            };

            if (ModelState.IsValid)
            {
                _dataService.AddIncome(accountId, income);
            }

            return RedirectToAction("Index", "Accounts");
        }
    }
}