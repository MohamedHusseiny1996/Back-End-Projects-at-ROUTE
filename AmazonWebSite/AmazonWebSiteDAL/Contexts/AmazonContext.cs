using AmazonWebSiteDAL.Entities;
using Department_project_DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonWebSiteDAL.Contexts
{
    public class AmazonContext:IdentityDbContext<UserApplication>
    {
        public AmazonContext(DbContextOptions options):base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseLazyLoadingProxies(true);
        }
        public DbSet<Items> Items { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
