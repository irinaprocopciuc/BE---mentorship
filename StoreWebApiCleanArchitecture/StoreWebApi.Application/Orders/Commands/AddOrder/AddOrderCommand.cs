using MediatR;
using StoreWebAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreWebApi.Application.Orders.Commands.AddOrder
{
    public record AddOrderCommand(OrderDTO Order): IRequest
    {
    }
}
