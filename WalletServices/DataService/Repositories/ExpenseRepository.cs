using System;
using System.Collections.Generic;
using System.Linq;
using WalletServices.Entities;

namespace WalletServices.DataService
{
    public class ExpenseRepository : IRepository<Expense>
    {
        private ApplicationContext _context;

        public ExpenseRepository(ApplicationContext context)
        {
            _context = context ?? throw new NullReferenceException(nameof(context));
        }

        public void Create(Expense item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Expense cannot be null");

            _context.Add(item);
            _context.SaveChanges();
        }

        public void Delete(int? id)
        {
            if (id == null || id <= 0)
                throw new ArgumentException("Expense id cannot be <=0 or null", nameof(id));

            var expenseToRemove = GetById(id);
            if (expenseToRemove == null)
                throw new Exception("Expense not found");

            _context.Remove(expenseToRemove);

            _context.SaveChanges();
        }

        public IEnumerable<Expense> GetAll()
        {
            return _context.Expenses;
        }

        public Expense GetById(int? id)
        {
            if (id == null || id <= 0)
                throw new ArgumentException("Expense id cannot be <=0 or null", nameof(id));

            return _context.Expenses.FirstOrDefault(a => a.Id == id);
        }
    }
}