using System;

namespace WalletServices.Entities
{
    public class Income
    {
        public int Id { get; set; }
        public Account Account { get; set; }
        public IncomeTypeName IncomeType { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public double Sum { get; set; }
    }
}