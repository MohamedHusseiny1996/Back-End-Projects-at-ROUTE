using Department_project_DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Department_project_DAL.Contexts
{
    public class CompanyContext : IdentityDbContext<UserApplication>
    {
        public CompanyContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
     
        public DbSet<Department> departments { get; set; }
        public DbSet<Employee> employees { get; set; }
       
    }
}
