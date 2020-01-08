
using System.Collections.Generic;
using WalletServices.Entities;

namespace WalletServices.BudgetManagementService
{
    public interface IWalletService
    {
        void AddAccount(Account account);
        void RemoveAccount(int? accountId);
        Account GetAccountById(int? accountId);
        IEnumerable<Account> GetAccounts();
        IEnumerable<Expense> GetExpenses();
        IEnumerable<Income> GetIncomes();
        public IEnumerable<AccountsTransfer> GetAccountsTransfers();
        void TransferSumToAnotherAccount(int sumToSend, string comment, int? senderAccountId, int? recieverAccountId);
        void AddIncome(int? accountId, Income income);
        void AddExpense(int? accountId, Expense expense);
        public Dictionary<string, double> GetIncomeStatistics();
        public Dictionary<string, double> GetExpenseStatistics();
    }
}
