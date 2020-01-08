using System;
using System.Collections.Generic;
using System.Linq;
using WalletServices.DataService;
using WalletServices.Entities;

namespace WalletServices.BudgetManagementService
{
    public class WalletService : IWalletService
    {
        public WalletService(UnitOfWork context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "Conext cannot be null");
        }

        private readonly UnitOfWork _context;

        public void AddAccount(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account), "Account cannot be null");

            _context.Accounts.Create(account);
        }

        public void RemoveAccount(int? accountId)
        {
            _context.Accounts.Delete(accountId);
        }

        public Account GetAccountById(int? accountId)
        {
            return _context.Accounts.GetById(accountId);
        }

        public IEnumerable<Account> GetAccounts()
        {
            return _context.Accounts.GetAll();
        }

        public IEnumerable<Expense> GetExpenses()
        {
            return _context.Expenses.GetAll();
        }

        public IEnumerable<Income> GetIncomes()
        {
            return _context.Incomes.GetAll();
        }

        public IEnumerable<AccountsTransfer> GetAccountsTransfers()
        {
            return _context.AccountsTransfers.GetAll();
        }

        public void TransferSumToAnotherAccount(int sumToSend, string comment, int? senderAccountId, int? recieverAccountId)
        {
            if (sumToSend <= 0)
                throw new ArgumentException("Wrong sum argument", nameof(sumToSend));

            var senderAccount = GetAccountById(senderAccountId);
            if (senderAccount == null)
                throw new Exception("Account not found");

            var recieverAccount = GetAccountById(recieverAccountId);
            if (recieverAccount == null)
                throw new Exception("Account not found");

            if (senderAccount.Sum < sumToSend)
                throw new InvalidOperationException("Not enough money on the account");

            senderAccount.Sum -= sumToSend;
            recieverAccount.Sum += sumToSend;

            var accountsTransfer = new AccountsTransfer()
            {
                SenderAccount = senderAccount,
                RecieverAccount = recieverAccount,
                Sum = sumToSend,
                Comment = string.IsNullOrEmpty(comment) ? string.Empty : comment,
                CreatedAt = DateTime.Now
            };

            _context.AccountsTransfers.Create(accountsTransfer);
        }

        public void AddIncome(int? accountId, Income income)
        {
            if (income == null || income.Sum <= 0)
                throw new ArgumentException("Wrong income argument");

            Account account = GetAccountById(accountId);
            if (account == null)
                throw new Exception("Account not found");

            account.Sum += income.Sum;
            account.Incomes.Add(income);

            _context.Incomes.Create(income);
        }

        public void AddExpense(int? accountId, Expense expense)
        {
            if (expense == null || expense.Sum <= 0)
                throw new ArgumentException("Wrong expense argument");

            Account account = GetAccountById(accountId);
            if (account == null)
                throw new Exception("Account not found");

            if(expense.Sum > account.Sum)
                throw new ArgumentException("Wrong expense argument");

            account.Sum -= expense.Sum;
            account.Expenses.Add(expense);

            _context.Expenses.Create(expense);
        }

        public Dictionary<string, double> GetIncomeStatistics()
        {
            var incomeStatistics = new Dictionary<string, double>();

            double totalSum = 0;
            foreach (var account in _context.Accounts.GetAll())
            {
                foreach (var income in account.Incomes.Where(i => i.Sum > 0))
                {
                    totalSum += income.Sum;
                    if (!incomeStatistics.ContainsKey(income.IncomeType.ToString()))
                        incomeStatistics.Add(income.IncomeType.ToString(), income.Sum);
                    else
                        incomeStatistics[income.IncomeType.ToString()] += income.Sum;
                }
            }

            foreach (var key in incomeStatistics.Keys.ToList())
                incomeStatistics[key] *= 100 / totalSum;

            return incomeStatistics;
        }

        public Dictionary<string, double> GetExpenseStatistics()
        {
            var expenseStatistics = new Dictionary<string, double>();

            double totalSum = 0;
            foreach (var account in _context.Accounts.GetAll())
            {
                foreach (var income in account.Expenses.Where(i => i.Sum > 0))
                {
                    totalSum += income.Sum;
                    if (!expenseStatistics.ContainsKey(income.ExpenseType.ToString()))
                        expenseStatistics.Add(income.ExpenseType.ToString(), income.Sum);
                    else
                        expenseStatistics[income.ExpenseType.ToString()] += income.Sum;
                }

            }

            foreach (var key in expenseStatistics.Keys.ToList())
                expenseStatistics[key] *= 100 / totalSum;

            return expenseStatistics;
        }
    }
}
