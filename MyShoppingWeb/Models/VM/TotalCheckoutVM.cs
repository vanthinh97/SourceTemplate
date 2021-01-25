using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShoppingWeb.Models
{
    public class TotalCheckoutVM
    {
        public string TotalDiscount { get; set; }
        public string AllTotal { get; set; }
        public List<ProductVM> lstProduct { get; set; }
    }
}