using Microsoft.EntityFrameworkCore;
using WalletServices.Entities;

namespace WalletServices.DataService
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<AccountsTransfer> AccountsTransfers { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Incomes)
                .WithOne(i => i.Account)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Account>()
                .HasMany(a => a.Expenses)
                .WithOne(i => i.Account)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AccountsTransfer>()
                .HasOne(at => at.RecieverAccount)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<AccountsTransfer>()
                .HasOne(at => at.SenderAccount)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
