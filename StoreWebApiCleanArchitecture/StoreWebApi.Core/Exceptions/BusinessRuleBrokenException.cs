using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWebApi.Domain.Exceptions
{
    class BusinessRuleBrokenException: Exception
    {
        public BusinessRuleBrokenException()
        {
        }

        public BusinessRuleBrokenException(string message)
            : base(message)
        {
        }

        public BusinessRuleBrokenException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
