using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyShoppingWeb.Models;

namespace MyShoppingWeb.Models
{
    public class ProCategoryDao
    {
        MyShoppingWebDbContext db = null;
        public ProCategoryDao()
        {
            db = new MyShoppingWebDbContext();
        }

        public List<Categories> ListAll()
        {
            var rs = db.Categories.OrderByDescending(x=>x.Position).ToList();
            return rs;
        }
    }
}