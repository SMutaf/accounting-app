using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAccountRepository Accounts { get; }
        ITransactionRepository Transactions { get; }
        ICustomerRepository Customers { get; }
        IInvoiceRepository Invoices { get; }

        Task<int> SaveChangesAsync();
    }
}
