using StoreWebApi.Application.Common.Specifications;
using StoreWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Carts.Queries
{
    public class GetCartProdByCartIdAndProdIdSpecification: BaseSpecification<CartProduct>
    {

        public GetCartProdByCartIdAndProdIdSpecification(int cartId, int prodId)
        {
            Query.Where(x => x.CartId == cartId).Where(x => x.ProductId == prodId);
        }
    }
}
