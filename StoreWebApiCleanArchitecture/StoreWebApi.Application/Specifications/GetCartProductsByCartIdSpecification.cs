using StoreWebApi.Application.Common.Specifications;
using StoreWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Specifications
{
    public class GetCartProductsByCartIdSpecification: BaseSpecification<CartProduct>
    {
        public GetCartProductsByCartIdSpecification(int cartId)
        {
             Query.Where(x => x.CartId == cartId);
        }
    }
}
