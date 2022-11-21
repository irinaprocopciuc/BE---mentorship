using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Web_API.Filters.Custom_Exceptions
{
    [Serializable]
    public class NotFoundDomainException: Exception
    {
        public NotFoundDomainException()
        {
        }

        public NotFoundDomainException(string message)
            : base(message)
        {
        }

        public NotFoundDomainException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
