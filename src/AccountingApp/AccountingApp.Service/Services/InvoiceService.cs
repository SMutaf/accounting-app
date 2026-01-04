using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingApp.Core.DTOS;
using AccountingApp.Core.Entities;
using AccountingApp.Core.Enums;
using AccountingApp.Core.Exceptions;
using AccountingApp.Core.Interfaces;
using AccountingApp.Services.Interfaces;
using AutoMapper;

namespace AccountingApp.Services.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITransactionService _transactionService;

        public InvoiceService(IUnitOfWork unitOfWork, IMapper mapper, ITransactionService transactionService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _transactionService = transactionService;
        }

        public async Task<InvoiceDto> CreateAsync(InvoiceDto invoiceDto)
        {
            var invoice = _mapper.Map<Invoice>(invoiceDto);

            invoice.Status = InvoiceStatus.Draft;

            await _unitOfWork.Invoices.AddAsync(invoice);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<InvoiceDto>(invoice);
        }

        public async Task DeleteAsync(int id)
        {
            var invoice = await _unitOfWork.Invoices.GetByIdAsync(id);
            if (invoice == null)
            {
                throw new NotFoundException($"Invoice with ID {id} not found.");
            }

            if (invoice.Status == InvoiceStatus.Approved)
            {
                throw new BusinessException("Approved invoices cannot be deleted.");
            }


            await _unitOfWork.Invoices.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<InvoiceDto>> GetAllAsync()
        {
            var invoices = await _unitOfWork.Invoices.GetAllAsync();
            return _mapper.Map<IEnumerable<InvoiceDto>>(invoices);
        }

        public async Task<InvoiceDto> GetByIdAsync(int id)
        {
            var invoice = await _unitOfWork.Invoices.GetByIdAsync(id);
            if (invoice == null)
            {
                throw new NotFoundException($"Invoice with ID {id} not found.");
            }
            return _mapper.Map<InvoiceDto>(invoice);
        }

        public async Task UpdateAsync(int id, InvoiceDto invoiceDto)
        {
            var existingInvoice = await _unitOfWork.Invoices.GetByIdAsync(id);
            if (existingInvoice == null)
            {
                throw new NotFoundException($"Invoice with ID {id} not found.");
            }

            if (existingInvoice.Status == InvoiceStatus.Approved)
            {
                throw new BusinessException("Approved invoices cannot be updated.");
            }

            _mapper.Map(invoiceDto, existingInvoice);


            await _unitOfWork.Invoices.UpdateAsync(existingInvoice);
            await _unitOfWork.SaveChangesAsync();
        }


        public async Task ApproveAsync(int id)
        {
            var invoice = await _unitOfWork.Invoices.GetByIdAsync(id);
            if (invoice == null) { throw new NotFoundException($"Invoice with ID {id} not found."); }
            if (invoice.Status != InvoiceStatus.Draft) { throw new BusinessException("Only draft invoices can be approved."); }

            var createdTransactionDto = await _transactionService.CreateTransactionFromInvoiceAsync(invoice);

            invoice.Status = InvoiceStatus.Approved;

            await _unitOfWork.Invoices.UpdateAsync(invoice);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CancelAsync(int id)
        {
            var invoice = await _unitOfWork.Invoices.GetByIdAsync(id);
            if (invoice == null)
            {
                throw new NotFoundException($"Invoice with ID {id} not found.");
            }

            if (invoice.Status == InvoiceStatus.Cancelled)
            {
                throw new BusinessException("Invoice is already cancelled.");
            }

            invoice.Status = InvoiceStatus.Cancelled;

            await _unitOfWork.Invoices.UpdateAsync(invoice);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}