using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingApp.Core.DTOS;


namespace AccountingApp.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<AccountDto>> GetAllAccountsAsync();
        Task<AccountDto> GetAccountByIdAsync(int id);
        Task<AccountDto> CreateAccountAsync(AccountDto accountDto);
        Task UpdateAccountAsync(int id, AccountDto accountDto);
        Task DeleteAccountAsync(int id);
    }
}