using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreWebApi.Application.Common.Interfaces;
using StoreWebApi.Infrastructure.DataAccess;

namespace StoreWebApi.Infrastructure
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var defaultConnectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<IStoreRepository, WebStoreDBContext>(options =>
               options.UseSqlServer(defaultConnectionString));


            return services;
        }
    }
}
