using AccountingApp.Core.Entities;
using AccountingApp.Core.Enums;
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
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AccountingAppDbContext context) : base(context)
        {
        }

        public async Task<Customer> GetByCodeAsync(string code)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Code == code);
        }

        public async Task<IEnumerable<Customer>> GetByTypeAsync(CustomerType type)
        {
            return await _context.Customers.Where(c => c.Type == type).ToListAsync();
        }
    }
}
