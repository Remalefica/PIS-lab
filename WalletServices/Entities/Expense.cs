using System;

namespace WalletServices.Entities
{
    public class Expense
    {
        public int Id { get; set; }
        public Account Account { get; set; }
        public ExpenseTypeName ExpenseType { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public double Sum { get; set; }
    }
}
