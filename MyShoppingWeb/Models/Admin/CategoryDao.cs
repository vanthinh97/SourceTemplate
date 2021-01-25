using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyShoppingWeb.Models;

namespace MyShoppingWeb.Models
{
    public class CategoryDao
    {
        MyShoppingWebDbContext db = new MyShoppingWebDbContext();
        public CategoryDao()
        {
            db = new MyShoppingWebDbContext();
        }

        public List<Categories> ListAll()
        {
            return db.Categories.ToList();
        }
    }
}