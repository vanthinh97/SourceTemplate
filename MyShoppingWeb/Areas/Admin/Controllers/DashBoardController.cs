using MyShoppingWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShoppingWeb.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin,Manager")]
    public class DashBoardController : Controller
    {
        MyShoppingWebDbContext db = new MyShoppingWebDbContext();
        // GET: Admin/DashBoard
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                int userId = int.Parse(User.Identity.Name);
                TblUsers u = db.TblUsers.Find(userId);
                ViewBag.Name = u.FullName;
                return View();
            }
            return View();
        }
    }
}