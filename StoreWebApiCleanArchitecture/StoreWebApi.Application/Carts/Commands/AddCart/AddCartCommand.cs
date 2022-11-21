using MediatR;
using StoreWebAPI.Domain.Entities;

namespace StoreWebApi.Application.Carts.Commands
{
    public record AddCartCommand(Cart Cart): IRequest
    {
    }
}
