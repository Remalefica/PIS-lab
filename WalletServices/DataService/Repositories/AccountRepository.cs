using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WalletServices.Entities;

namespace WalletServices.DataService
{
    public class AccountRepository : IRepository<Account>
    {
        private ApplicationContext _context;

        public AccountRepository(ApplicationContext context)
        {
            _context = context ?? throw new NullReferenceException(nameof(context));
        }

        public void Create(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account), "Account cannot be null");

            _context.Add(account);
            _context.SaveChanges();
        }

        public void Delete(int? accountId)
        {
            if (accountId == null || accountId <= 0)
                throw new ArgumentException("Account id cannot be <=0 or null", nameof(accountId));

            var accountToRemove = GetById(accountId);
            if (accountToRemove == null)
                throw new Exception("Account not found");

            _context.Remove(accountToRemove);

            _context.SaveChanges();
        }

        public IEnumerable<Account> GetAll()
        {
            return _context.Accounts
                .Include(a => a.Incomes)
                .Include(a => a.Expenses);
        }

        public Account GetById(int? accountId)
        {
            if (accountId == null || accountId <= 0)
                throw new ArgumentException("Account id cannot be <=0 or null", nameof(accountId));

            return _context.Accounts.FirstOrDefault(a => a.Id == accountId);
        }
    }
}