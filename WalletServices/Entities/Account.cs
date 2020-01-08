using System.Collections.Generic;

namespace WalletServices.Entities
{
    /// <summary>
    /// Account model
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Account's unique Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Account's name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Account's balance
        /// </summary>
        public double Sum { get; set; }
        /// <summary>
        /// The List of Incomes
        /// </summary>
        public List<Income> Incomes { get; set; }
        /// <summary>
        /// The List of Expenses
        /// </summary>
        public List<Expense> Expenses { get; set; }

        public Account()
        {
            Incomes = new List<Income>();
            Expenses = new List<Expense>();
        }
    }
}
