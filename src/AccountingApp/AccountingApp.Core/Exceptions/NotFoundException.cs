using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingApp.Core.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() : base("Aranan kaynak bulunamadı.")
        {
        }
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
