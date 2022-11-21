using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Products.Commands.RemoveProduct
{
    public record RemoveProductCommand(int productId): IRequest
    {
    }
}
