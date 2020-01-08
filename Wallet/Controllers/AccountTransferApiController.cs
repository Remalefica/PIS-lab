using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WalletServices.BudgetManagementService;
using WalletServices.Entities;

namespace Wallet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountTransferApiController : ControllerBase
    {
        private readonly IWalletService _dataService;

        public AccountTransferApiController(IWalletService dataService)
        {
            _dataService = dataService;
        }

        /// <summary>
        /// Transfer money between the Accounts
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /AccountsTransfer/Create
        ///     {
        ///        "sum":1,
        ///        "comment": "Comment",
        ///        "senderId": 1,
        ///        "recieverId": 2
        ///     }
        ///
        /// </remarks>
        /// <returns>The transfer between Accounts</returns>
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm]int senderId, [FromForm]int recieverId, [FromForm]string comment, 
            [FromForm]int sum)
        {
            if (ModelState.IsValid)
            {
                _dataService.TransferSumToAnotherAccount(
                    sumToSend:sum,
                    comment: comment,
                    senderAccountId: senderId,
                    recieverAccountId: recieverId);
            }

            return RedirectToAction("Index", "Accounts");
        }

        /// <summary>
        /// Transfer money between the Accounts
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /AccountsTransfer
        ///     {
        ///     
        ///     }
        ///
        /// </remarks>
        /// <returns>The transfer between Accounts</returns>
        [HttpGet]
        public async Task<IEnumerable<AccountsTransfer>> Get()
        {
            return _dataService.GetAccountsTransfers();
        }
    }
}