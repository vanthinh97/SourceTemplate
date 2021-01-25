using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShoppingWeb.Models;

namespace MyShoppingWeb.Controllers
{
    public class ProductsController : Controller
    {
        MyShoppingWebDbContext db = new MyShoppingWebDbContext();
        // GET: Products
        public ActionResult Index(string kw, int? cateid, string sortby, int? page )
        {
            var showCate = db.Categories.ToList();
            ViewBag.ShowCate = showCate;

            ViewBag.Kw = kw;
            ViewBag.Cateid = cateid;
            ViewBag.SortBy = sortby;

            IEnumerable<Products> result = db.Products.OrderByDescending(x=>x.Id);
            result = !string.IsNullOrEmpty(kw) ? getKw(kw, result) : result;
            result = cateid != null ? getCate(cateid, result) : result;
            result = !string.IsNullOrEmpty(sortby) ? getSortby(sortby, result) : result;

            var rs = result.Select(s => new ProductVM
            {
                Id = s.Id,
                ProductName = s.ProductName,
                ImgUrl = "/data/" + s.Id + "/" + s.ImgUrl,
                UnitPrice = s.UnitPrice.ToString("N2") 
            });
            // 30
            return View(rs.ToList());
        }
        public IEnumerable<Products> getCate(int? cateid, IEnumerable<Products> p)
        {
            p = p.Where(x => x.CategoryId == cateid);
            return p;
        }
        public IEnumerable<Products> getKw(string kw, IEnumerable<Products> p)
        {
            p = p.Where(x => x.ProductName.Contains(kw));
            return p;
        }
        public IEnumerable<Products> getSortby(string sortby, IEnumerable<Products> p)
        {
            switch (sortby)
            {
                case "name_asc":
                    p = p.OrderBy(x => x.ProductName);
                    break;
                case "name_desc":
                    p = p.OrderByDescending(x => x.ProductName);
                    break;
                default:
                    break;
            }
            return p;
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            Products p = db.Products.Find(id);
            return View(p);
        }

        [ChildActionOnly]
        public PartialViewResult ProCategory()
        {
            var model = new ProCategoryDao().ListAll();
            return PartialView("_ProCategory", model);
        }

        [HttpGet]
        public ActionResult SortProCategory(int id)
        {
            var SortPro = db.Products.Where(x => x.CategoryId == id).ToList();
            ViewBag.NewProducts = SortPro;
            return View("~/Views/Home/Index.cshtml");
        }
    }
}