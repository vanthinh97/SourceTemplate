using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyShoppingWeb.Models;


namespace MyShoppingWeb.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        MyShoppingWebDbContext db = new MyShoppingWebDbContext();
        // GET: Admin/Product
        public ActionResult Index()
        {
            var show = db.Products.OrderByDescending(x => x.Id);
            return View(show.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            var show = db.Categories.OrderBy(x=>x.Position);
            string date = DateTime.Today.ToString("yyyy-MM-dd");
            ViewBag.date = date;
            return View(show);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(UploadProduct UpPro, HttpPostedFileBase File)
        {
            string fileName = File.FileName;

            Products NewPro = new Products()
            {
                ProductName = UpPro.ProductName,
                UnitPrice = UpPro.UnitPrice,
                DiscountRatio = UpPro.DiscountRatio,
                DiscountExpiry = UpPro.DiscountExpiry,
                CategoryId = UpPro.CategoryId,
                Description = UpPro.Description,
                ImgUrl = fileName
            };
            db.Products.Add(NewPro);
            db.SaveChanges();

            string strFolder = Server.MapPath("~/data/products/" + NewPro.Id);

            if (!Directory.Exists(strFolder))
            {
                Directory.CreateDirectory(strFolder);
            }
            File.SaveAs(strFolder + @"\" + fileName);

            //ViewBag.Congra = "Add NewProduct successfully";

            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // GET: Admin2/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", products.CategoryId);
            return View(products);
        }

        // POST: Admin2/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductName,UnitPrice,DiscountRatio,DiscountExpiry,IsActive,CategoryId,ImgUrl,Description")] Products products)
        {
            if (ModelState.IsValid)
            {
                db.Entry(products).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "CategoryName", products.CategoryId);
            return View(products);
        }

        // GET: Admin2/Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = db.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        // POST: Admin2/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = db.Products.Find(id);
            db.Products.Remove(products);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}