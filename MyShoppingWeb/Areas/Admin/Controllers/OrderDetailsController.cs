using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyShoppingWeb.Models;

namespace MyShoppingWeb.Areas.Admin.Controllers
{
    public class OrderDetailsController : Controller
    {
        private MyShoppingWebDbContext db = new MyShoppingWebDbContext();

        // GET: Admin/OrderDetails
        public ActionResult Index()
        {
            var orderDetails = db.OrderDetails.Include(o => o.Orders).Include(o => o.Products);
            return View(orderDetails.ToList());
        }

        // GET: Admin/OrderDetails/Details/5
        public ActionResult Details(int? id)// orred id
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var orderDetails = db.OrderDetails.Where(x=>x.OrderId == id);
            if (orderDetails == null)
            {
                return HttpNotFound();
            }
            return View("Details", orderDetails);
        }

        
    }
}
