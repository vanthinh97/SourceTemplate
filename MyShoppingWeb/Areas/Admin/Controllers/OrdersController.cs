
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
    public class OrdersController : Controller
    {
        private MyShoppingWebDbContext db = new MyShoppingWebDbContext();

        // GET: Admin/Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.OrderStatuses);
            return View(orders.ToList());
        }

        // GET: Admin/Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var order = db.Orders.Find(id);
            var orderDetail = db.OrderDetails.Where(x => x.OrderId == id);
            //var rs = order.Select(s => new OrderVM
            //{
            //    Id = s.Id,
            //    CustomerName = s.CustomerName,
            //    CustomerEmail = s.CustomerEmail,
            //    CustomerPhone = s.CustomerPhone,
            //    CustomerAddress = s.CustomerAddress,
            //    OrderDetails = orderDetail.ToList()
            //});

            OrderVM rs2 = new OrderVM()
            {
                Id = order.Id,
                CustomerName = order.CustomerName,
                CustomerEmail = order.CustomerEmail,
                CustomerPhone = order.CustomerPhone,
                CustomerAddress = order.CustomerAddress,
                OrderDetails = orderDetail.ToList()
            };

            if (rs2 == null)
            {
                return HttpNotFound();
            }

            return View("Details", rs2);

            //var findOrder = db.Orders.Find(id);
            //ViewBag.Name = findOrder.CustomerName;
            //ViewBag.Email = findOrder.CustomerEmail;
            //ViewBag.Address = findOrder.CustomerAddress;
            //ViewBag.Phone = findOrder.CustomerPhone;

            //var orderDetails = db.OrderDetails.Where(x => x.OrderId == id);
            //if (orderDetails == null)
            //{
            //    return HttpNotFound();
            //}
            //return View("Details", orderDetails.ToList());
        }

        // GET: Admin/Orders/Create
        public ActionResult Create()
        {
            ViewBag.StatusId = new SelectList(db.OrderStatuses, "Id", "StatusName");
            return View();
        }

        // POST: Admin/Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CustomerName,CustomerEmail,CustomerAddress,CustomerPhone,StatusId")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(orders);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StatusId = new SelectList(db.OrderStatuses, "Id", "StatusName", orders.StatusId);
            return View(orders);
        }

        // GET: Admin/Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            ViewBag.StatusId = new SelectList(db.OrderStatuses, "Id", "StatusName", orders.StatusId);
            return View(orders);
        }

        // POST: Admin/Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CustomerName,CustomerEmail,CustomerAddress,CustomerPhone,StatusId")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StatusId = new SelectList(db.OrderStatuses, "Id", "StatusName", orders.StatusId);
            return View(orders);
        }

        // GET: Admin/Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // POST: Admin/Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orders orders = db.Orders.Find(id);
            db.Orders.Remove(orders);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
