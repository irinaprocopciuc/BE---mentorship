using MediatR;
using StoreWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Products.Commands.UpdateProducts
{
    public record UpdateProductCommand(Product product): IRequest
    {
    }
}
