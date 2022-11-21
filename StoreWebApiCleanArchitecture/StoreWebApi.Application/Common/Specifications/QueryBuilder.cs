using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Common.Specifications
{
    public class QueryBuilder<T> where T:class
    {
        private List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        private List<Expression<Func<T, bool>>> CriteriaList { get; } = new List<Expression<Func<T, bool>>>();

        public QueryBuilder<T> Include(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
            return this;
        }

        public QueryBuilder<T> Where(Expression<Func<T, bool>> criteria)
        {
            CriteriaList.Add(criteria);
            return this;
        }

        public IQueryable<T> BuildQuery(IQueryableWithSpec<T> set)
        {
            foreach (var criteria in CriteriaList)
            {
                set = set.Where(criteria);
            }

            foreach (var criteria in Includes)
            {
                set = set.Include(criteria);
            }
            return set.AsQueriable();
        }
    }
}
