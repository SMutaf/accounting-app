using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingApp.Core.DTOS;
using AccountingApp.Core.Entities;


namespace AccountingApp.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDto>> GetAllAsync();
        Task<TransactionDto> GetByIdAsync(int id);
        Task DeleteTransactionAsync(int id);

        Task<TransactionDto> CreateTransactionFromInvoiceAsync(Invoice invoice);
        Task<TransactionDto> CreateIncomeAsync(CreateIncomeDto dto);
        Task<TransactionDto> CreateExpenseAsync(CreateExpenseDto dto);
        Task<TransactionDto> CreateTransferAsync(CreateTransferDto dto);

    }
}