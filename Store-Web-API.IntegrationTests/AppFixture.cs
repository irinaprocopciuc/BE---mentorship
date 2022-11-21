using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Respawn;
using Respawn.Graph;
using Store_Web_API.Controllers;
using Store_Web_API.DataAccess;
using Store_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store_Web_API.IntegrationTests
{
    public class AppFixture : WebApplicationFactory<Program>
    {

        private static readonly Checkpoint Checkpoint = new Checkpoint { 
            TablesToIgnore = new[] { new Table("__EFMigrationsHistory") }
        };
        private string connectionString = "Data Source=NBKR004093; Initial Catalog=WebStoreIntegration1; Integrated Security=SSPI;TrustServerCertificate=True";
        private IServiceScopeFactory serviceFactory;

        public AppFixture()
        {
            var services = new ServiceCollection();
            services.AddDbContext<WebStoreDBContext>(c => c.UseSqlServer(connectionString));
            serviceFactory = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();
            EnsureDatabaseCreated();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<WebStoreDBContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }
                    
                services.AddDbContext<WebStoreDBContext>((options, context) =>
                {
                    //options.UseInMemoryDatabase("InMemoryTest");
                    context.UseSqlServer(connectionString);
                });

                //serviceFactory = services.BuildServiceProvider().GetRequiredService <IServiceScopeFactory>();
                //EnsureDatabaseCreated();
            });
        }

        public async Task ResetState()
        {
            await Checkpoint.Reset(connectionString);
        }

        public async Task<T> Add<T>(T entity) where T:class
        {
            var scope = serviceFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<WebStoreDBContext>();
            var res = context.Entry(entity);
            res.State = EntityState.Added;
            await context.SaveChangesAsync();
            await res.ReloadAsync();
            return res.Entity;
        }

        public async Task DeleteDB()
        {
            await serviceFactory.CreateScope().ServiceProvider.GetRequiredService<WebStoreDBContext>().Database.EnsureDeletedAsync();
        }

        private void EnsureDatabaseCreated()
        {
            using (var scope = serviceFactory.CreateScope())
            using (var appContext = scope.ServiceProvider.GetRequiredService<WebStoreDBContext>())
            {
                try
                {
                    //appContext.Products.Add(new Product { ProductId = 1, Name = "Test", Description = "Descr test", Stock = 23, Price = 34, Category = "makeup"});
                    //appContext.Products.Add(new Product { ProductId = 2, Name = "Test 2", Description = "Descr test 2", Stock = 41, Price = 100, Category = "makeup" });
                    //appContext.Products.Add(new Product { ProductId = 100, Name = "Test 2", Description = "Descr test 2", Stock = 41, Price = 100, Category = "makeup" });
                    //appContext.Carts.Add(new Cart { CartId = 1, Products = new List<CartProduct> { }, Discount = 20, Total = 0 });
                    //appContext.Carts.Add(new Cart { CartId = 2, Discount = 20, Total = 0 });
                    //appContext.CartProducts.Add(new CartProduct{ Id = 1 ,ProductId = 100 ,Name = "Test", Price = 34, Quantity = 2, CartId = 2 });
                    //appContext.SaveChanges();
                    appContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}
