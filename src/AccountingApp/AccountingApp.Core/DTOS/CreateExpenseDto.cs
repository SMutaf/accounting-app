using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Core.DTOS
{
    public class CreateExpenseDto
    {
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public int SourceAccountId { get; set; }
        public int ExpenseAccountId { get; set; }
        public int? CustomerId { get; set; }
    }
}
