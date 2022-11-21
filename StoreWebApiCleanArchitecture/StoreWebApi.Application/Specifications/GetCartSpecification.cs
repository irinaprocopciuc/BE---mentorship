using StoreWebApi.Application.Common.Specifications;
using StoreWebApi.Domain.Entities;
using StoreWebAPI.Domain.Entities;

namespace StoreWebApi.Application.Carts.Queries.GetCart
{
    public class GetCartSpecification: BaseSpecification<Cart>
    {
        public GetCartSpecification(int id)
        {
            Query.Include(x => x.Products).Where(x => x.Id == id);
        }
    }
}
