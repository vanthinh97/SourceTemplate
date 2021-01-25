using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShoppingWeb.Models;
using Newtonsoft.Json;

namespace MyShoppingWeb.Controllers
{
    public class CheckoutController : Controller
    {
        MyShoppingWebDbContext db = new MyShoppingWebDbContext();
        // GET: Checkout
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Checkout(string data, 
            string CustomerName, string CustomerEmail,
            string CustomerAddress,
            string CustomerPhone, string OrderNote
            )
        {
            if ((CustomerName ?? CustomerEmail ?? CustomerAddress) == "")
            {
                ModelState.AddModelError("", "Check your information again");
                return View();
            }

            var cart_items = JsonConvert.DeserializeObject<List<CartItem>>(data);
            if (cart_items == null || cart_items.Count == 0)
            {
                return Content("Ko có sp trong giỏ hàng");
            }


            Orders orders = new Orders()
            {
                CustomerName = CustomerName,
                CustomerEmail = CustomerEmail,
                CustomerAddress = CustomerAddress,
                CustomerPhone = CustomerPhone,
                // OrderNote = OrderNote
                StatusId = 1
            };
            db.Orders.Add(orders);
          
            foreach (var item in cart_items)
            {
                Products p = db.Products.Find(item.productid);
                OrderDetails od = new OrderDetails()
                {
                    OrderId = orders.Id,
                    ProductId = item.productid,
                    UnitPrice = p.UnitPrice,
                    Quantity = item.quantity
                };
                db.OrderDetails.Add(od);
            }
            db.SaveChanges();
            return Content("OK");
        }
    }
}