using StoreWebApi.Application.Common.Specifications;
using StoreWebApi.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Common.Interfaces
{
    public interface IStoreRepository
    {
        Task<T> FirstOrDefault<T>(IQuerySpecification<T> spec, bool isNotTrackable = false) where T : BaseEntity;
        Task<T> AddAsync<T>(T entity) where T : BaseEntity;
        Task<T> UpdateAsync<T>(T newEntity) where T : BaseEntity;
        Task RemoveAsync<T>(int entityId) where T : BaseEntity;

        Task<IReadOnlyCollection<T>> ExecuteQuery<T>(IQuerySpecification<T> spec) where T : BaseEntity;
    }
}
