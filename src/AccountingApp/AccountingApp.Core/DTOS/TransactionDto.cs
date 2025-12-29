using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Core.DTOS
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public decimal TotalAmount { get; set; }
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public bool IsPosted { get; set; }
        public ICollection<TransactionLineDto> Lines { get; set; }
    }
}
