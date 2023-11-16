using Core.Entities;
using Core.Entities.OrderAggregation;
using Microsoft.EntityFrameworkCore;
using Repository.Data.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreContext).Assembly);//(it is called refliction of store context) any class applying ientity type configuration for any dbset in store context.. get it here
        }

        public DbSet<Product> products { get; set; }
        public DbSet<ProductBrand> brands { get; set; }
        public DbSet<ProductType> types { get; set; }
		public DbSet<Order> orders { get; set; }
		public DbSet<OrderItem> orderItems { get; set; }
		public DbSet<DeliveryMethod> deliveryMethods { get; set; }
	}
}
