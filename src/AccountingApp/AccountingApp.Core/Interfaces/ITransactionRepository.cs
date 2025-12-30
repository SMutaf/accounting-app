using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingApp.Core.Entities;
using AccountingApp.Core.DTOS;

namespace AccountingApp.Core.Interfaces
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<IEnumerable<Transaction>> GetByDateRangeAsync(DateTime start, DateTime end);
        Task<IEnumerable<Transaction>> GetByCustomerAsync(int customerId);

        Task<Transaction> GetByIdWithDetailsAsync(int id);

        Task<IEnumerable<AccountBalanceDto>> GetAccountBalancesAsync();
    }
}
