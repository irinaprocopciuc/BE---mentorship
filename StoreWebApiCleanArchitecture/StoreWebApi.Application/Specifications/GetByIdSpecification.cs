using StoreWebApi.Application.Common.Specifications;
using StoreWebApi.Domain.Entities;

namespace StoreWebApi.Application.Specifications
{
    public class GetByIdSpecification<T> : BaseSpecification<T> where T: BaseEntity
    {
        public GetByIdSpecification(int id)
        {
            Query.Where(x => x.Id == id);
        }
    }
}
