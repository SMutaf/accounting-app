using AccountingApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Core.Interfaces
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {
        Task<Invoice> GetByNumberAsync(string invoiceNumber);
        Task<IEnumerable<Invoice>> GetByCustomerAsync(int customerId);
    }
}
