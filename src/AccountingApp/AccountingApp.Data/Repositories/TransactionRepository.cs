using AccountingApp.Core.DTOS;
using AccountingApp.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingApp.Core.Entities;

using AccountingApp.Data.Context;

namespace AccountingApp.Data.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(AccountingAppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Transaction>> GetByCustomerAsync(int customerId)
        {
            return await _context.Transactions
                                 .Where(t => t.CustomerId == customerId)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Transaction>> GetByDateRangeAsync(DateTime start, DateTime end)
        {
            return await _context.Transactions
                                 .Where(t => t.Date >= start && t.Date <= end)
                                 .ToListAsync();
        }

        public async Task<Transaction> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Transactions
                .Include(t => t.Lines)
                    .ThenInclude(l => l.Account)
                .Include(t => t.Customer)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<AccountBalanceDto>> GetAccountBalancesAsync()
        {
            var result = await _context.TransactionLines
                .Include(l => l.Account)
                .GroupBy(l => new { l.AccountId, l.Account.Code, l.Account.Name, l.Account.Type })
                .Select(g => new AccountBalanceDto
                {
                    AccountId = g.Key.AccountId,
                    AccountCode = g.Key.Code,
                    AccountName = g.Key.Name,

                    TotalDebit = g.Sum(l => l.DebitAmount),
                    TotalCredit = g.Sum(l => l.CreditAmount)
                })
                .ToListAsync();

            return result;
        }
    }
}
