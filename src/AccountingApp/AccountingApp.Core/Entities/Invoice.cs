using AccountingApp.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace AccountingApp.Core.Entities
{
    public class Invoice : BaseEntity
    {
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int CustomerId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public InvoiceType Type { get; set; }
        public InvoiceStatus Status { get; set; }
        public int? TransactionId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Transaction Transaction { get; set; }
        public virtual ICollection<InvoiceItem> Items { get; set; }
    }
}
