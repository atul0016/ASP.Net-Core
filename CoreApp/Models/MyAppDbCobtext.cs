using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApp.Models
{
    /// <summary>
    /// The Data Access Class
    /// 1. Define DbSet<T> properties
    /// 2. Establish Connection with DB and Create Tables if not exists
    /// </summary>
    public class MyAppDbContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Emplopyees { get; set; }

        /// <summary>
        /// The ctor is injected by DbContextOptions<T> class
        /// This will read connection string from Application StartUp class
        /// and create an instance of DbContext to map to Database
        /// </summary>
        /// <param name="options"></param>
        public MyAppDbContext(DbContextOptions<MyAppDbContext> options) : base(options)
        {
        }
        /// <summary>
        /// Used for Model Configuration w.r.t. DB Table 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
