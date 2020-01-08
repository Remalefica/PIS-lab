using System;

namespace WalletServices.DataService
{
    public class UnitOfWork : IDisposable
    {
        private ApplicationContext _context;

        private IncomeRepository incomeRepository;
        private ExpenseRepository expenseRepository;
        private AccountsTransferRepository accountTransferRepository;
        private AccountRepository accountRepository;

        public UnitOfWork(ApplicationContext context)
        {
            _context = context ?? throw new NullReferenceException(nameof(context));
        }

        public AccountRepository Accounts
        {
            get
            {
                return accountRepository ?? (accountRepository = new AccountRepository(_context));
            }
        }
        public IncomeRepository Incomes
        {
            get
            {
                return incomeRepository ?? (incomeRepository = new IncomeRepository(_context));
            }
        }

        public ExpenseRepository Expenses
        {
            get
            {
                return expenseRepository ?? (expenseRepository = new ExpenseRepository(_context));
            }
        }

        public AccountsTransferRepository AccountsTransfers
        {
            get
            {
                return accountTransferRepository ?? (accountTransferRepository = new AccountsTransferRepository(_context));
            }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
