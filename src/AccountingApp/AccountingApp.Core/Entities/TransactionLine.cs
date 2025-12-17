using AccountingApp.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Core.Entities
{
    public class TransactionLine : BaseEntity
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public string Notes { get; set; }

        public virtual Transaction Transaction { get; set; }
        public virtual Account Account { get; set; }
    }
}
