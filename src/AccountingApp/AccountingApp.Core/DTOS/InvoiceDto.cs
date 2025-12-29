using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Core.DTOS
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int CustomerId { get; set; }

        public string? CustomerName { get; set; }

        public decimal SubTotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Type { get; set; }

        public string? Status { get; set; }

        public List<InvoiceItemDto> Items { get; set; } = new List<InvoiceItemDto>();
    }
}
