using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StoreWebApi.Application.Carts.Commands;
using StoreWebApi.Application.Carts.Commands.AddCart;
using StoreWebApi.Application.Carts.Queries;
using StoreWebApi.Application.Orders.Commands;
using StoreWebApi.Application.Orders.Queries;
using StoreWebApi.Application.Products.Commands;
using StoreWebApi.Application.Products.Queries.GetAllProducts;
using StoreWebApi.Application.Products.Queries.GetById;
using System.Reflection;

namespace StoreWebApi.Application
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
