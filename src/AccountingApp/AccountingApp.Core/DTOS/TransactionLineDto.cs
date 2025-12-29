using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Core.DTOS
{
    public class TransactionLineDto
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string AccountName { get; set; }
        public string AccountCode { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public string Notes { get; set; }
    }
}
