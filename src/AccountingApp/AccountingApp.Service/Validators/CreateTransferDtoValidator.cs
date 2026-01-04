using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingApp.Core.DTOS;
using FluentValidation;

namespace AccountingApp.Services.Validators
{
    public class CreateTransferDtoValidator : AbstractValidator<CreateTransferDto>
    {
        public CreateTransferDtoValidator()
        {
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage("Tutar 0'dan büyük olmalıdır.");
            RuleFor(x => x.Description).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Date).NotEmpty();
            RuleFor(x => x.SourceAccountId).GreaterThan(0).WithMessage("Kaynak hesap seçilmelidir.");
            RuleFor(x => x.TargetAccountId).GreaterThan(0).WithMessage("Hedef hesap seçilmelidir.");

            RuleFor(x => x).Must(x => x.SourceAccountId != x.TargetAccountId)
                .WithMessage("Kaynak ve Hedef hesap aynı olamaz.");
        }
    }
}