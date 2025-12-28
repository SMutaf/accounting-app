using AccountingApp.Core.Entities;
using AccountingApp.Core.Enums;
using AccountingApp.Core.Interfaces;
using AccountingApp.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AccountingApp.Data.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(AccountingDbContext context) : base(context)
        {
        }

        public async Task<Account> GetByCodeAsync(string code)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.Code == code);
        }

        public async Task<IEnumerable<Account>> GetByTypeAsync(AccountType type)
        {
            return await _context.Accounts.Where(a => a.Type == type).ToListAsync();
        }
    }
}
