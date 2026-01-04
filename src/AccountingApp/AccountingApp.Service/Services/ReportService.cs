using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingApp.Core.DTOS;
using AccountingApp.Core.Interfaces;
using AccountingApp.Services.Interfaces;


namespace AccountingApp.Services.Services
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<AccountBalanceDto>> GetAccountBalancesAsync()
        {
            return await _unitOfWork.Transactions.GetAccountBalancesAsync();
        }

        public Task<AccountBalanceDto> GetAccountBalanceByIdAsync(int accountId)
        {
            throw new System.NotImplementedException();
        }
    }
}