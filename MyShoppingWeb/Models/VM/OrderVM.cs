using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyShoppingWeb.Models
{
    public class OrderVM
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
    }
}