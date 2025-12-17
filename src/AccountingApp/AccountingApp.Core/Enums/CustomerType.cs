using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Core.Enums
{
    public enum CustomerType
    {
        Customer = 1,   // only customer (sadece müşteri)
        Supplier = 2,    // only supplier (sadece tedarikçi)
        Both = 3         // both (ikiside)
    }
}
