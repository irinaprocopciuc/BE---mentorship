using StoreWebApi.Application.Common.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Specifications
{
    public class GetAllSpecification<T>: BaseSpecification<T> where T:class
    {
        public GetAllSpecification()
        {

        }
    }
}
