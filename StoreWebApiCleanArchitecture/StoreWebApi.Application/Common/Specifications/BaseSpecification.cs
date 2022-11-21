using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Common.Specifications
{
    public abstract class BaseSpecification<T>: IQuerySpecification<T> where T:class
    {
        protected QueryBuilder<T> Query;
        public BaseSpecification()
        {
            Query = new QueryBuilder<T>();
        }

        public IQueryable<T> ApplyToSet(IQueryableWithSpec<T> set)
        {
            return Query.BuildQuery(set);
        }

        public IQueryable<T> AsNoTracking(IQueryableWithSpec<T> set)
        {
            return set.AsNoTracking();
        }
    }
}
