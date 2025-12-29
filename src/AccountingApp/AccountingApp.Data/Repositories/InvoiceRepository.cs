using AccountingApp.Core.Entities;
using AccountingApp.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingApp.Data.Context;

namespace AccountingApp.Data.Repositories
{
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(AccountingAppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Invoice>> GetByCustomerAsync(int customerId)
        {
            return await _context.Invoices
                                 .Where(i => i.CustomerId == customerId)
                                 .ToListAsync();
        }

        public async Task<Invoice> GetByNumberAsync(string invoiceNumber)
        {
            return await _context.Invoices
                                 .FirstOrDefaultAsync(i => i.InvoiceNumber == invoiceNumber);
        }
    }
}
