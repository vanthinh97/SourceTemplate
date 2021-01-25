using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShoppingWeb.Models;

namespace MyShoppingWeb.Areas.Admin.Controllers
{
    public class AuthencationController : Controller
    {
        MyShoppingWebDbContext db = new MyShoppingWebDbContext();
        // GET: Admin/Authencation
        [HttpGet]
        public ActionResult Index()
        {
            var rs = db.UserRoles;
            return View(rs.ToList());
        }

        [HttpPost]
        public ActionResult AddRole(int RoleId, int UserId)
        {
            var check = db.UserRoles.Where(x => x.UserId == UserId).FirstOrDefault();
            if (check == null)
            {
                UserRoles u = new UserRoles()
                {
                    RoleId = RoleId,
                    UserId = UserId
                };
                db.UserRoles.Add(u);
                db.SaveChanges();
            }
            else
            {
                //show ra da co viewbag
            }
          
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var rs = db.UserRoles.Find(id);
            if (rs != null)
            {
                db.UserRoles.Remove(rs);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}