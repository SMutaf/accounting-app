using AccountingApp.Core.Entities;
using AccountingApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Core.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> GetByCodeAsync(string code);
        Task<IEnumerable<Customer>> GetByTypeAsync(CustomerType type);
    }
}
