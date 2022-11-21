using MediatR;
using StoreWebApi.Domain.Entities;
using StoreWebAPI.Domain.Entities;
using System.Collections.Generic;

namespace StoreWebApi.Application.Carts.Commands.AddProductInCart
{
    public record AddProductInCartCommand(List<CartProductDTO> Products, int cartId): IRequest
    {
    }
}
