using AccountingApp.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

using AccountingApp.Core.Enums;
namespace AccountingApp.Core.Entities
{
    public class Account : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public AccountType Type { get; set; }
        public int? ParentAccountId { get; set; }


        public virtual Account ParentAccount { get; set; }
        public virtual ICollection<Account> SubAccounts { get; set; }
        public virtual ICollection<TransactionLine> TransactionLines { get; set; }
    }
}
