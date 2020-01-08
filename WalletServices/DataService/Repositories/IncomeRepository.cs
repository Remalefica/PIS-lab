using System;
using System.Collections.Generic;
using System.Linq;
using WalletServices.Entities;

namespace WalletServices.DataService
{
    public class IncomeRepository : IRepository<Income>
    {
        private ApplicationContext _context;

        public IncomeRepository(ApplicationContext context)
        {
            _context = context ?? throw new NullReferenceException(nameof(context));
        }

        public void Create(Income item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Income cannot be null");

            _context.Add(item);
            _context.SaveChanges();
        }

        public void Delete(int? id)
        {
            if (id == null || id <= 0)
                throw new ArgumentException("Income id cannot be <=0 or null", nameof(id));

            var incomeToRemove = GetById(id);
            if (incomeToRemove == null)
                throw new Exception("Income not found");

            _context.Remove(incomeToRemove);

            _context.SaveChanges();
        }

        public IEnumerable<Income> GetAll()
        {
            return _context.Incomes;
        }

        public Income GetById(int? id)
        {
            if (id == null || id <= 0)
                throw new ArgumentException("Account id cannot be <=0 or null", nameof(id));

            return _context.Incomes.FirstOrDefault(a => a.Id == id);
        }
    }
}