using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Core.DTOS
{
    public class CreateIncomeDto
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public int TargetAccountId { get; set; }
        public int RevenueAccountId { get; set; }

        public int? CustomerId { get; set; }
    }
}
