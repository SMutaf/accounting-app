using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingApp.Core.DTOS;
using AccountingApp.Core.Entities;
using AccountingApp.Core.Exceptions;
using AccountingApp.Core.Interfaces;
using AccountingApp.Services.Interfaces;
using AutoMapper;


namespace AccountingApp.Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AccountDto> CreateAccountAsync(AccountDto accountDto)
        {
            var account = _mapper.Map<Account>(accountDto);

            await _unitOfWork.Accounts.AddAsync(account);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<AccountDto>(account);
        }

        public async Task DeleteAccountAsync(int id)
        {
            var account = await _unitOfWork.Accounts.GetByIdAsync(id);
            if (account == null)
            {
                throw new NotFoundException("Silinecek hesap bulunamadı.");
            }

            await _unitOfWork.Accounts.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<AccountDto> GetAccountByIdAsync(int id)
        {
            var account = await _unitOfWork.Accounts.GetByIdAsync(id);
            if (account == null)
            {
                throw new NotFoundException("Hesap bulunamadı.");
            }

            return _mapper.Map<AccountDto>(account);
        }

        public async Task<IEnumerable<AccountDto>> GetAllAccountsAsync()
        {
            var accounts = await _unitOfWork.Accounts.GetAllAsync();

            return _mapper.Map<IEnumerable<AccountDto>>(accounts);
        }

        public async Task UpdateAccountAsync(int id, AccountDto accountDto)
        {
            var existingAccount = await _unitOfWork.Accounts.GetByIdAsync(id);
            if (existingAccount == null)
            {
                throw new NotFoundException("Güncellenecek hesap bulunamadı.");
            }

            _mapper.Map(accountDto, existingAccount);

            await _unitOfWork.Accounts.UpdateAsync(existingAccount);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
