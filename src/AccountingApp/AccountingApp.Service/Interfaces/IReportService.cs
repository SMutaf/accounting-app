using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingApp.Core.DTOS;


namespace AccountingApp.Services.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<AccountBalanceDto>> GetAccountBalancesAsync();

        Task<AccountBalanceDto> GetAccountBalanceByIdAsync(int accountId);
    }
}