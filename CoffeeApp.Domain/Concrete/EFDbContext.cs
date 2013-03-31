using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using CoffeeApp.Domain.Entities;

namespace CoffeeApp.Domain.Concrete
{
    class EFDbContext : DbContext
    {
        public EFDbContext() : base("CoffeeStore") { }
        public DbSet<Coffee> Coffees { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<Order> Orders { get; set; }
        //public DbSet<OrderCart> OrderCarts { get; set; }
        //public DbSet<OrderCartLine> OrderCartLines { get; set; }
    }
}
