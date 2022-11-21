using MediatR;
using StoreWebApi.Application.Products.Queries.GetAllProducts;
using StoreWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Products.Queries
{
    public record GetAllProductsQuery: IRequest<GetAllProductsResponse>
    {
    }
}
