using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShoppingWeb.Models
{
    public class UploadProduct
    {
        public string ProductName { get; set; }
        public double UnitPrice { get; set; }
        public Nullable<double> DiscountRatio { get; set; }
        public Nullable<System.DateTime> DiscountExpiry { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string ImgUrl { get; set; }
        public string Description { get; set; }
    }
}