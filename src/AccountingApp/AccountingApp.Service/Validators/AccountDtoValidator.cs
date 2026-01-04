using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingApp.Core.DTOS;
using AccountingApp.Core.Enums;
using FluentValidation;

namespace AccountingApp.Service.Validators
{
    public class AccountDtoValidator : AbstractValidator<AccountDto>
    {
        public AccountDtoValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Hesap kodu boş olamaz.")
                .MaximumLength(20).WithMessage("Hesap kodu en fazla 20 karakter olabilir.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Hesap adı boş olamaz.")
                .MaximumLength(200).WithMessage("Hesap adı en fazla 200 karakter olabilir.");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Hesap tipi boş olamaz.")
                .Must(BeValidAccountType).WithMessage("Geçersiz hesap tipi. 'Asset', 'Liability', 'Equity', 'Revenue' veya 'Expense' olmalıdır.");
        }

        private bool BeValidAccountType(string type)
        {
            return Enum.TryParse<AccountType>(type, true, out _);
        }
    }
}
