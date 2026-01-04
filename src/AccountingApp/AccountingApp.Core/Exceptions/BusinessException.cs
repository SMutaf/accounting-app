using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Core.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException() : base("İş kuralı ihlali oluştu.")
        {
        }

        public BusinessException(string message) : base(message)
        {
        }
    }
}
