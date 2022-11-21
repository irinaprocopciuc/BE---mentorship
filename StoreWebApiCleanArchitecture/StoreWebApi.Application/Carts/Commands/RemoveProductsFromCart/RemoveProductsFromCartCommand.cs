using MediatR;

namespace StoreWebApi.Application.Carts.Commands.RemoveProductsFromCart
{
    public record RemoveProductsFromCartCommand(int productId, int cartId): IRequest
    {
    }
}
