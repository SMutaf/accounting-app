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
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TransactionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TransactionDto>> GetAllAsync()
        {
            var transactions = await _unitOfWork.Transactions.GetAllAsync();

            return _mapper.Map<IEnumerable<TransactionDto>>(transactions);
        }

        public async Task<TransactionDto> GetByIdAsync(int id)
        {

            var transaction = await _unitOfWork.Transactions.GetByIdWithDetailsAsync(id);

            if (transaction == null)
            {
                throw new NotFoundException($"Transaction with ID {id} not found.");
            }
            return _mapper.Map<TransactionDto>(transaction);
        }

        public async Task DeleteTransactionAsync(int id)
        {
            var transaction = await _unitOfWork.Transactions.GetByIdAsync(id);
            if (transaction == null)
            {
                throw new NotFoundException($"Transaction with ID {id} not found.");
            }

            await _unitOfWork.Transactions.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<TransactionDto> CreateTransactionFromInvoiceAsync(Invoice invoice)
        {
            if (invoice.TransactionId.HasValue)
            {
                throw new BusinessException($"Invoice {invoice.InvoiceNumber} already has a transaction (ID: {invoice.TransactionId}).");
            }

            var customerAccountId = await GetOrCreateCustomerAccountIdAsync(invoice.CustomerId);

            var salesAccount = await _unitOfWork.Accounts.GetByCodeAsync("600");
            if (salesAccount == null)
                throw new BusinessException("Sistemde '600 - Yurtiçi Satışlar' hesabı tanımlı değil!");

            var taxAccount = await _unitOfWork.Accounts.GetByCodeAsync("391");
            if (taxAccount == null && invoice.TaxAmount > 0)
                throw new BusinessException("Sistemde '391 - Hesaplanan KDV' hesabı tanımlı değil!");

            int salesAccountId = salesAccount.Id;
            int taxAccountId = taxAccount.Id;

            var transaction = new Transaction
            {
                Date = invoice.InvoiceDate,
                Description = $"Invoice {invoice.InvoiceNumber} posting.",
                Type = Core.Enums.TransactionType.Income,
                TotalAmount = invoice.TotalAmount,
                CustomerId = invoice.CustomerId,
                IsPosted = true,
                Lines = new List<TransactionLine>()
            };

            transaction.Lines.Add(new TransactionLine
            {
                AccountId = customerAccountId,
                DebitAmount = invoice.TotalAmount,
                CreditAmount = 0,
                Notes = $"Invoice {invoice.InvoiceNumber}"
            });

            transaction.Lines.Add(new TransactionLine
            {
                AccountId = salesAccountId,
                DebitAmount = 0,
                CreditAmount = invoice.SubTotal,
                Notes = $"Invoice {invoice.InvoiceNumber}"
            });

            if (invoice.TaxAmount > 0)
            {
                transaction.Lines.Add(new TransactionLine
                {
                    AccountId = taxAccountId,
                    DebitAmount = 0,
                    CreditAmount = invoice.TaxAmount,
                    Notes = $"Invoice {invoice.InvoiceNumber} VAT"
                });
            }

            decimal totalDebit = transaction.Lines.Sum(l => l.DebitAmount);
            decimal totalCredit = transaction.Lines.Sum(l => l.CreditAmount);
            if (totalDebit != totalCredit)
            {
                throw new BusinessException("Transaction debit and credit totals do not match.");
            }

            await _unitOfWork.Transactions.AddAsync(transaction);

            return _mapper.Map<TransactionDto>(transaction);
        }

        private async Task<int> GetOrCreateCustomerAccountIdAsync(int customerId)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(customerId);
            if (customer == null) throw new NotFoundException("Müşteri bulunamadı.");

            var parentAccountCode = "120";

            var allAccounts = await _unitOfWork.Accounts.GetAllAsync();
            var parentAccount = allAccounts.FirstOrDefault(x => x.Code == parentAccountCode);

            if (parentAccount == null)
            {
                throw new InvalidOperationException("Sistemde '120 Alıcılar' ana hesabı bulunamadı. Lütfen Hesap Planını kontrol edin.");
            }

            string targetAccountCode = $"{parentAccountCode}.{customer.Code}";

            var existingAccount = allAccounts.FirstOrDefault(x => x.Code == targetAccountCode);

            if (existingAccount != null)
            {
                return existingAccount.Id;
            }

            var newAccount = new Account
            {
                Code = targetAccountCode,
                Name = customer.Name,
                Type = AccountType.Asset,
                ParentAccountId = parentAccount.Id,
                CreatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Accounts.AddAsync(newAccount);
            await _unitOfWork.SaveChangesAsync();

            return newAccount.Id;
        }

        public async Task<TransactionDto> CreateIncomeAsync(CreateIncomeDto dto)
        {
            var targetAccount = await _unitOfWork.Accounts.GetByIdAsync(dto.TargetAccountId);
            var revenueAccount = await _unitOfWork.Accounts.GetByIdAsync(dto.RevenueAccountId);

            if (targetAccount == null) throw new NotFoundException("Kasa/Banka hesabı bulunamadı.");
            if (revenueAccount == null) throw new NotFoundException("Gelir hesabı bulunamadı.");

            var transaction = new Transaction
            {
                Date = dto.Date,
                Description = dto.Description,
                Type = Core.Enums.TransactionType.Income,
                TotalAmount = dto.Amount,
                CustomerId = dto.CustomerId,
                IsPosted = true,
                CreatedAt = DateTime.UtcNow,
                Lines = new List<TransactionLine>()
            };


            transaction.Lines.Add(new TransactionLine
            {
                AccountId = dto.TargetAccountId,
                DebitAmount = dto.Amount,
                CreditAmount = 0,
                Notes = "Tahsilat / Gelir Girişi",
                CreatedAt = DateTime.UtcNow
            });


            transaction.Lines.Add(new TransactionLine
            {
                AccountId = dto.RevenueAccountId,
                DebitAmount = 0,
                CreditAmount = dto.Amount,
                Notes = "Gelir Kaydı",
                CreatedAt = DateTime.UtcNow
            });

            await _unitOfWork.Transactions.AddAsync(transaction);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<TransactionDto>(transaction);
        }

        public async Task<TransactionDto> CreateExpenseAsync(CreateExpenseDto dto)
        {
            var sourceAccount = await _unitOfWork.Accounts.GetByIdAsync(dto.SourceAccountId);
            var expenseAccount = await _unitOfWork.Accounts.GetByIdAsync(dto.ExpenseAccountId);

            if (sourceAccount == null) throw new NotFoundException("Kasa/Banka hesabı bulunamadı.");
            if (expenseAccount == null) throw new NotFoundException("Gider hesabı bulunamadı.");

            var transaction = new Transaction
            {
                Date = dto.Date,
                Description = dto.Description,
                Type = Core.Enums.TransactionType.Expense,
                TotalAmount = dto.Amount,
                CustomerId = dto.CustomerId,
                IsPosted = true,
                CreatedAt = DateTime.UtcNow,
                Lines = new List<TransactionLine>()
            };


            transaction.Lines.Add(new TransactionLine
            {
                AccountId = dto.ExpenseAccountId,
                DebitAmount = dto.Amount,
                CreditAmount = 0,
                Notes = "Gider Tahakkuku",
                CreatedAt = DateTime.UtcNow
            });

            transaction.Lines.Add(new TransactionLine
            {
                AccountId = dto.SourceAccountId,
                DebitAmount = 0,
                CreditAmount = dto.Amount,
                Notes = "Ödeme Çıkışı",
                CreatedAt = DateTime.UtcNow
            });

            await _unitOfWork.Transactions.AddAsync(transaction);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<TransactionDto>(transaction);
        }

        public async Task<TransactionDto> CreateTransferAsync(CreateTransferDto dto)
        {
            var sourceAccount = await _unitOfWork.Accounts.GetByIdAsync(dto.SourceAccountId);
            var targetAccount = await _unitOfWork.Accounts.GetByIdAsync(dto.TargetAccountId);

            if (sourceAccount == null) throw new NotFoundException("Kaynak hesap bulunamadı.");
            if (targetAccount == null) throw new NotFoundException("Hedef hesap bulunamadı.");

            var transaction = new Transaction
            {
                Date = dto.Date,
                Description = dto.Description,
                Type = Core.Enums.TransactionType.Transfer,
                TotalAmount = dto.Amount,
                IsPosted = true,
                CreatedAt = DateTime.UtcNow,
                Lines = new List<TransactionLine>()
            };


            transaction.Lines.Add(new TransactionLine
            {
                AccountId = dto.SourceAccountId,
                DebitAmount = 0,
                CreditAmount = dto.Amount,
                Notes = $"Transfer Çıkışı -> {targetAccount.Name}",
                CreatedAt = DateTime.UtcNow
            });

            transaction.Lines.Add(new TransactionLine
            {
                AccountId = dto.TargetAccountId,
                DebitAmount = dto.Amount,
                CreditAmount = 0,
                Notes = $"Transfer Girişi <- {sourceAccount.Name}",
                CreatedAt = DateTime.UtcNow
            });

            await _unitOfWork.Transactions.AddAsync(transaction);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<TransactionDto>(transaction);
        }



    }
}