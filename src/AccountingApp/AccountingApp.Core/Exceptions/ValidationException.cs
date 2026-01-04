using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Core.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() : base("Bir veya daha fazla doğrulama hatası oluştu.")
        {
        }
        public ValidationException(string message) : base(message)
        {
        }
    }
}
