using AccountingApp.Core.Entities;
using AccountingApp.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Core.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<IEnumerable<Account>> GetByTypeAsync(AccountType type);
        Task<Account> GetByCodeAsync(string code);
    }
}
