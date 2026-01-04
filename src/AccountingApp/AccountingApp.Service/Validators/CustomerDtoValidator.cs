using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingApp.Core.DTOS;
using FluentValidation;

namespace AccountingApp.Services.Validators
{
    public class CustomerDtoValidator : AbstractValidator<CustomerDto>
    {
        public CustomerDtoValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Müşteri kodu boş olamaz.")
                .MaximumLength(20).WithMessage("Müşteri kodu en fazla 20 karakter olabilir.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Müşteri adı boş olamaz.")
                .MaximumLength(200).WithMessage("Müşteri adı en fazla 200 karakter olabilir.");

            RuleFor(x => x.Email)
                .EmailAddress().When(x => !string.IsNullOrEmpty(x.Email))
                .WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Müşteri tipi boş olamaz.")
                .Must(BeValidCustomerType).WithMessage("Geçersiz müşteri tipi. 'Customer', 'Supplier' veya 'Both' olmalıdır.");
        }

        private bool BeValidCustomerType(string type)
        {
            return Enum.TryParse<Core.Enums.CustomerType>(type, true, out _);
        }
    }
}