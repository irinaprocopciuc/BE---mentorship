using StoreWebApi.Application.Common.Specifications;
using StoreWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Carts.Commands.AddProductInCart
{
    class GetProductsByIdsSpec: BaseSpecification<Product>
    {
        public GetProductsByIdsSpec(IReadOnlyList<int> productsIds)
        {
            Query.Where(p => productsIds.Contains(p.Id));
        }
    }
}
