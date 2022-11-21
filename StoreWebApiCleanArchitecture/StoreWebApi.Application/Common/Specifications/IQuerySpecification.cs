using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Common.Specifications
{
    public interface IQuerySpecification<T> where T:class
    {
        IQueryable<T> ApplyToSet(IQueryableWithSpec<T> set);
    }
}
