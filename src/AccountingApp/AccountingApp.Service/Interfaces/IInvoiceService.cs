using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingApp.Core.DTOS;


namespace AccountingApp.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<IEnumerable<InvoiceDto>> GetAllAsync();
        Task<InvoiceDto> GetByIdAsync(int id);
        Task<InvoiceDto> CreateAsync(InvoiceDto invoiceDto);
        Task UpdateAsync(int id, InvoiceDto invoiceDto);
        Task DeleteAsync(int id);

        Task ApproveAsync(int id);
        Task CancelAsync(int id);
    }
}