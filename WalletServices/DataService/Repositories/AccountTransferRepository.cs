using System;
using System.Collections.Generic;
using System.Linq;
using WalletServices.Entities;

namespace WalletServices.DataService
{
    public class AccountsTransferRepository : IRepository<AccountsTransfer>
    {
        private ApplicationContext _context;

        public AccountsTransferRepository(ApplicationContext context)
        {
            _context = context ?? throw new NullReferenceException(nameof(context));
        }

        public void Create(AccountsTransfer item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "AccountsTransfer cannot be null");

            _context.Add(item);
            _context.SaveChanges();
        }

        public void Delete(int? id)
        {
            if (id == null || id <= 0)
                throw new ArgumentException("AccountsTransfer id cannot be <=0 or null", nameof(id));

            var expenseToRemove = GetById(id);
            if (expenseToRemove == null)
                throw new Exception("AccountsTransfer not found");

            _context.Remove(expenseToRemove);

            _context.SaveChanges();
        }

        public IEnumerable<AccountsTransfer> GetAll()
        {
            return _context.AccountsTransfers;
        }

        public AccountsTransfer GetById(int? id)
        {
            if (id == null || id <= 0)
                throw new ArgumentException("AccountsTransfer id cannot be <=0 or null", nameof(id));

            return _context.AccountsTransfers.FirstOrDefault(a => a.Id == id);
        }
    }
}