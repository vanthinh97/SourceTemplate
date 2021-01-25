using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShoppingWeb.Models
{
    public class CartVM
    {
        public int ProductId { get; set; }
        public string ImgUrl { get; set; }
        public string ProductName { get; set; }
        public string UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string Total { get; set; }

        public string TotalDiscount { get; set; }
        public string AllTotal { get; set; }
    }

}