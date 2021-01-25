namespace MyShoppingWeb.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using MyShoppingWeb.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MyShoppingWeb.Models.MyShoppingWebDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MyShoppingWeb.Models.MyShoppingWebDbContext context)
        {
            //var products = new List<Products>
            //{

            //};
            //products.ForEach(s => context.Students.Add(s));
            //context.SaveChanges();
        }
    }
}
