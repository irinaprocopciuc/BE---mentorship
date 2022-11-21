using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Common.Exceptions
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
