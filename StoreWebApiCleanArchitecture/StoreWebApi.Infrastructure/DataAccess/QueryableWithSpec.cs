using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using StoreWebApi.Application.Common.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StoreWebApi.Infrastructure.DataAccess
{
    public class QueryableWithSpec<T> : IQueryableWithSpec<T> where T:class
    {
        private DbSet<T> dbSet;
        private List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        private List<Expression<Func<T, bool>>> CriteriaList { get; } = new List<Expression<Func<T, bool>>>();

        public QueryableWithSpec(DbSet<T> set)
        {
            dbSet = set;
        }

        public IQueryable<T> AsQueriable()
        {
            IQueryable<T> set = dbSet;
            foreach (var criteria in Includes)
            {
                set = set.Include(criteria);
            }

            foreach (var criteria in CriteriaList)
            {
                set = set.Where(criteria);
            }

            return set;
        }

        public IQueryable<T> AsNoTracking()
        {
            return dbSet.AsNoTracking();
        }

        public IQueryableWithSpec<T> Include(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
            return this;
        }

        public IQueryableWithSpec<T> Where(Expression<Func<T, bool>> criteria)
        {
            CriteriaList.Add(criteria);
            return this;
        }
    }
}
