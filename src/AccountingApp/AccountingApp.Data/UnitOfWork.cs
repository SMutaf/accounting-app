using AccountingApp.Core.Interfaces;
using AccountingApp.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingApp.Data.Context;

namespace AccountingApp.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AccountingAppDbContext _context;

        private IAccountRepository _accountRepository;
        private ICustomerRepository _customerRepository;
        private IInvoiceRepository _invoiceRepository;
        private ITransactionRepository _transactionRepository;

        public UnitOfWork(AccountingAppDbContext context)
        {
            _context = context;
        }

        public IAccountRepository Accounts => _accountRepository ??= new AccountRepository(_context);
        public ICustomerRepository Customers => _customerRepository ??= new CustomerRepository(_context);
        public IInvoiceRepository Invoices => _invoiceRepository ??= new InvoiceRepository(_context);
        public ITransactionRepository Transactions => _transactionRepository ??= new TransactionRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
