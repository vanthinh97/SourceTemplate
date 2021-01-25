using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyShoppingWeb.Models
{
    public class MyShoppingWebDbContext : DbContext
    {
        public MyShoppingWebDbContext() : base("name=MyShoppingWebDbContext") { }

        public DbSet<Categories> Categories { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<OrderStatuses> OrderStatuses { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<TblUsers> TblUsers { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
      
    }
}