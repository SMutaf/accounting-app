using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingApp.Core.DTOS;
using FluentValidation;

namespace AccountingApp.Services.Validators
{
    public class CreateExpenseDtoValidator : AbstractValidator<CreateExpenseDto>
    {
        public CreateExpenseDtoValidator()
        {
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Tutar 0'dan büyük olmalıdır.");
            RuleFor(x => x.Description).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.SourceAccountId).GreaterThan(0).WithMessage("Kasa/Banka hesabı seçilmelidir.");
            RuleFor(x => x.ExpenseAccountId).GreaterThan(0).WithMessage("Gider hesabı seçilmelidir.");
        }
    }
}