using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountingApp.Core.DTOS;
using AccountingApp.Core.Enums;
using FluentValidation;

namespace AccountingApp.Services.Validators
{
    public class InvoiceDtoValidator : AbstractValidator<InvoiceDto>
    {
        public InvoiceDtoValidator()
        {
            RuleFor(x => x.InvoiceNumber)
                .NotEmpty().WithMessage("Fatura numarası boş olamaz.")
                .MaximumLength(50).WithMessage("Fatura numarası en fazla 50 karakter olabilir.");


            RuleFor(x => x.CustomerId)
                .GreaterThan(0).WithMessage("Geçerli bir Müşteri ID'si belirtilmelidir.");

            RuleFor(x => x.InvoiceDate)
                .NotEmpty().WithMessage("Fatura tarihi boş olamaz.");

            RuleFor(x => x.TotalAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Toplam tutar 0 veya daha büyük olmalıdır.");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Fatura tipi boş olamaz.")
                .Must(BeValidInvoiceType).WithMessage("Geçersiz fatura tipi. 'Sales' veya 'Purchase' olmalıdır.");

            RuleFor(x => x.Status)
                 .Must(BeValidInvoiceStatus).When(x => !string.IsNullOrEmpty(x.Status))
                 .WithMessage("Geçersiz fatura statüsü. 'Draft', 'Approved' veya 'Cancelled' olmalıdır.");

        }

        private bool BeValidInvoiceType(string type)
        {
            return Enum.TryParse<InvoiceType>(type, true, out _);
        }

        private bool BeValidInvoiceStatus(string status)
        {
            return Enum.TryParse<InvoiceStatus>(status, true, out _);
        }
    }
}
