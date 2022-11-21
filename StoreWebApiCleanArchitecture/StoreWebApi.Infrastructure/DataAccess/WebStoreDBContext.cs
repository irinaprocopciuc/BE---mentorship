using Microsoft.EntityFrameworkCore;
using StoreWebApi.Application.Common.Interfaces;
using StoreWebApi.Application.Common.Specifications;
using StoreWebApi.Domain.Entities;
using StoreWebApi.Infrastructure.DataAccess.Configurations;
using StoreWebAPI.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreWebApi.Infrastructure.DataAccess
{
    public class WebStoreDBContext : DbContext, IStoreRepository
    {
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }

        public WebStoreDBContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new CartProductConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public async Task<T> FirstOrDefault<T>(IQuerySpecification<T> spec, bool isNotTrackable = true) where T : BaseEntity
        {
            IQueryableWithSpec<T> queryableSpec = new QueryableWithSpec<T>(this.Set<T>());
            if(isNotTrackable)
            {
                queryableSpec.AsNoTracking();
            }
            return await spec.ApplyToSet(queryableSpec).FirstOrDefaultAsync();
        }

        public async Task<T> AddAsync<T>(T newEntity) where T : BaseEntity
        {
            await PersistEntity(newEntity, EntityState.Added);
            return newEntity;
        }

        public async Task<T> UpdateAsync<T>(T entity) where T : BaseEntity
        {
            await PersistEntity(entity, EntityState.Modified);
            return entity;
        }

        public async Task RemoveAsync<T>(int entityId) where T : BaseEntity
        {
            T existingItem = this.Set<T>().Find(entityId);
            await PersistEntity(existingItem, EntityState.Deleted);
        }

        public async Task<IReadOnlyCollection<T>> ExecuteQuery<T>(IQuerySpecification<T> spec) where T : BaseEntity
        {
            IQueryableWithSpec<T> queryableSpec = new QueryableWithSpec<T>(this.Set<T>());
            return await spec.ApplyToSet(queryableSpec).ToListAsync();
        }

        private async Task PersistEntity<T>(T entity, EntityState state)
        {
            var entry = this.Entry(entity);
            entry.State = state;
            await this.SaveChangesAsync();
        }
    }
}
