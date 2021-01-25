using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyShoppingWeb.Models
{
    public class ProductVM
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ImgUrl { get; set; }
        public string UnitPrice { get; set; }
        public string DiscountRatio { get; set; }
        public string salePrice { get; set; }
        public string CategoryName { get; set; }

    }
}