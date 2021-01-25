using MyShoppingWeb.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MyShoppingWeb.Models;

namespace MyShoppingWeb.Controllers
{
    public class AccountController : Controller
    {
        MyShoppingWebDbContext db = new MyShoppingWebDbContext();
        // GET: Account
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
              //  int userId = int.Parse(User.Identity.Name);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(string Email, string Password)
        {
            var user = db.TblUsers.Where(x => x.Email.Equals(Email)).SingleOrDefault();
            if (user == null)
            {
                ModelState.AddModelError("", "Tài khoản không tồn tại");
            }
            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
            {
                if (user.Password.Equals(MySecurity.EncryptPassword(Password)))
                {
                    Response.Cookies["UserId"].Value = user.Id.ToString();
                    FormsAuthentication.SetAuthCookie(user.Id.ToString(), true);

                   
                    var c = db.UserRoles.Where(x => x.UserId == user.Id).FirstOrDefault();
                    if (c!= null && (c.RoleId == 1 || c.RoleId == 2)) //1 laf admin
                    {
                        return RedirectToAction("Index","DashBoard", new { area = "Admin" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult LoginCkout(string Email, string Password)
        {
            var user = db.TblUsers.Where(x => x.Email.Equals(Email)).SingleOrDefault();
            if (user == null)
            {
                ModelState.AddModelError("", "Tài khoản không tồn tại");
            }
            if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
            {
                if (user.Password.Equals(MySecurity.EncryptPassword(Password)))
                {
                    Response.Cookies["UserId"].Value = user.Id.ToString();
                    FormsAuthentication.SetAuthCookie(user.Id.ToString(), true);


                    var c = db.UserRoles.Where(x => x.UserId == user.Id).FirstOrDefault();
                    if (c != null && (c.RoleId == 1 || c.RoleId == 2)) //1 laf admin
                    {
                        return RedirectToAction("Index", "DashBoard", new { area = "Admin" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng");
                }
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Register()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Register(string Email, string Password, string FullName, string Address)
        {

            TblUsers t = new TblUsers()
            {
                Email = Email,
                Password = MySecurity.EncryptPassword(Password),
                FullName = FullName,
                Address = Address,
                IsActive = true
            };
            db.TblUsers.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
            //return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Abandon();
            Response.Cookies["UserId"].Expires = DateTime.Now.AddDays(-10);
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}