using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Common.Specifications
{
    public interface IQueryableWithSpec<T> where T: class
    {

        IQueryableWithSpec<T> Include(Expression<Func<T, object>> includeExpression);
        IQueryableWithSpec<T> Where(Expression<Func<T, bool>> criteria);

        IQueryable<T> AsQueriable();
        IQueryable<T> AsNoTracking();
    }
}
