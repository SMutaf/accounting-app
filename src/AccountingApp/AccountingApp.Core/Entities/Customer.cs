using AccountingApp.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using AccountingApp.Core.Enums;

namespace AccountingApp.Core.Entities
{
    public class Customer : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string TaxNumber { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public CustomerType Type { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }

    }
}
