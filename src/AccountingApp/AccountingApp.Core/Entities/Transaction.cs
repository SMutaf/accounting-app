using AccountingApp.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Core.Entities
{
    public class Transaction : BaseEntity
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public TransactionType Type { get; set; }
        public decimal TotalAmount { get; set; }
        public int? CustomerId { get; set; }
        public bool IsPosted { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<TransactionLine> Lines { get; set; }
    }
}
