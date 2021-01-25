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
    public class TblUsersController : Controller
    {
        private MyShoppingWebDbContext db = new MyShoppingWebDbContext();

        // GET: Admin/TblUsers
        public ActionResult Index()
        {
            return View(db.TblUsers.ToList());
        }

        // GET: Admin/TblUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblUsers tblUsers = db.TblUsers.Find(id);
            if (tblUsers == null)
            {
                return HttpNotFound();
            }
            return View(tblUsers);
        }

        // GET: Admin/TblUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/TblUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Email,Password,FullName,Address,IsActive")] TblUsers tblUsers)
        {
            if (ModelState.IsValid)
            {
                db.TblUsers.Add(tblUsers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblUsers);
        }

        // GET: Admin/TblUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblUsers tblUsers = db.TblUsers.Find(id);
            if (tblUsers == null)
            {
                return HttpNotFound();
            }
            return View(tblUsers);
        }

        // POST: Admin/TblUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Email,Password,FullName,Address,IsActive")] TblUsers tblUsers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblUsers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblUsers);
        }

        // GET: Admin/TblUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblUsers tblUsers = db.TblUsers.Find(id);
            if (tblUsers == null)
            {
                return HttpNotFound();
            }
            return View(tblUsers);
        }

        // POST: Admin/TblUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TblUsers tblUsers = db.TblUsers.Find(id);
            db.TblUsers.Remove(tblUsers);
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
