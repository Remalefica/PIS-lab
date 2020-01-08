using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WalletServices.Entities
{
    public class AccountsTransfer
    {
        public int Id { get; set; }

        public int SenderAccountId { get; set; }
        [ForeignKey("SenderAccountId")]
        public Account SenderAccount { get; set; }

        public int RecieverAccountId { get; set; }
        [ForeignKey("RecieverAccountId")]
        public Account RecieverAccount { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public double Sum { get; set; }
    }
}
