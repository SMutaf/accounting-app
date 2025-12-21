using AccountingApp.Core.Entities;
using AccountingApp.Core.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Data.Context
{
    public class AccountingAppDbContext : DbContext
    {
        public AccountingAppDbContext(DbContextOptions<AccountingAppDbContext> options) : base(options)
        {
        }
        public DbSet<Account> Accounts { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Invoice> Invoices { get; set; }

        public DbSet<InvoiceItem> InvoiceItems { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<TransactionLine> TransactionLines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //Varsayılan hesaplar
            modelBuilder.Entity<Account>().HasData(
                new Account { Id = 1, Code = "100", Name = "Kasa", Type = AccountType.Asset, CreatedAt = DateTime.UtcNow },
                new Account { Id = 2, Code = "102", Name = "Bankalar", Type = AccountType.Asset, CreatedAt = DateTime.UtcNow },
                new Account { Id = 3, Code = "120", Name = "Alıcılar", Type = AccountType.Asset, CreatedAt = DateTime.UtcNow },
                new Account { Id = 4, Code = "320", Name = "Satıcılar", Type = AccountType.Liability, CreatedAt = DateTime.UtcNow },
                new Account { Id = 5, Code = "600", Name = "Yurtiçi Satışlar", Type = AccountType.Revenue, CreatedAt = DateTime.UtcNow },
                new Account { Id = 6, Code = "770", Name = "Genel Yönetim Giderleri", Type = AccountType.Expense, CreatedAt = DateTime.UtcNow }
            );

        }
    }
}
