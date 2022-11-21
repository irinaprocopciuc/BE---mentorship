using MediatR;
using StoreWebApi.Application.Products.Queries.GetById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Products.Queries
{
    public record GetByIdQuery(int productId) : IRequest<GetByIdResponse>
    {
    }
}
