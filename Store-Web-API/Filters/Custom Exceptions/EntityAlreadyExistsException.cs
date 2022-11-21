using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Web_API.Filters.Custom_Exceptions
{
    public class EntityAlreadyExistsException: Exception
    {
        public EntityAlreadyExistsException()
        {
        }

        public EntityAlreadyExistsException(string message)
            : base(message)
        {
        }

        public EntityAlreadyExistsException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
