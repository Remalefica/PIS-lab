using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WalletServices.BudgetManagementService;
using WalletServices.Entities;

namespace Wallet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountApiController : ControllerBase
    {
        private readonly IWalletService _dataService;

        public AccountApiController(IWalletService dataService)
        {
            _dataService = dataService;
        }

        /// <summary>
        /// Create the new account
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /AccountApi/Create
        ///     {
        ///        "name": "Item1",
        ///        "sum": 100
        ///     }
        ///
        /// </remarks>
        /// <param name="name">The name of account</param>
        /// <param name="sum">The account balance</param>
        /// <returns>The created Account</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]string name, [FromForm]int sum)
        {
            var account = new Account()
            {
                Sum = sum,
                Name = name
            };

            if (ModelState.IsValid)
            {
                _dataService.AddAccount(account);
            }

            return RedirectToAction("Index", "Accounts");
        }


        /// <summary>
        /// Delete the account
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /AccountApi/Delete
        ///     {
        ///        "id": 1
        ///     }
        ///
        /// </remarks>
        /// <returns>The removed Account</returns>
        [HttpPost("api/AccountApi/Delete")]
        public async Task<IActionResult> Delete([FromForm]int id)
        {
            _dataService.RemoveAccount(id);
            return RedirectToAction("Index", "Accounts");
        }
    }
}