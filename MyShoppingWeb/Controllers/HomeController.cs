using MyShoppingWeb.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;
namespace MyShoppingWeb.Controllers
{
    public class HomeController : Controller
    {
        MyShoppingWebDbContext db = new MyShoppingWebDbContext();
        public ActionResult Index(string kw, int? cateid, string sortby, int? page)
        {
            int pageSize = 6;
            int pageNumber = page ?? 1;

            var showCate = db.Categories.ToList();
            ViewBag.ShowCate = showCate;

            ViewBag.Kw = kw;
            ViewBag.Cateid = cateid;
            ViewBag.SortBy = sortby;

            IEnumerable<Products> result = db.Products.OrderByDescending(x => x.Id);
            result = !string.IsNullOrEmpty(kw) ? getKw(kw, result) : result;
            result = cateid != null ? getCate(cateid, result) : result;
            result = !string.IsNullOrEmpty(sortby) ? getSortby(sortby, result) : result;

            var rs = result.Select(s => new ProductVM
            {
                Id = s.Id,
                ProductName = s.ProductName,
                ImgUrl = "/data/products/" + s.Id + "/" + s.ImgUrl,
                UnitPrice = s.UnitPrice.ToString("N2"),
                DiscountRatio = s.DiscountRatio.ToString(),
                salePrice = (s.UnitPrice - ((s.UnitPrice * s.DiscountRatio) / 100)).ToString(),
                CategoryName = s.Categories.CategoryName
            });

            return View(rs.ToPagedList(pageNumber, pageSize));
        }

        public IEnumerable<Products> getCate(int? cateid, IEnumerable<Products> p)
        {
            p = p.Where(x => x.CategoryId == cateid);
            return p;
        }
        public IEnumerable<Products> getKw(string kw, IEnumerable<Products> p)
        {
            kw = kw.ToLower();
            ViewBag.Kw = kw;
            p = p.Where(x => x.ProductName.ToLower().Contains(kw));
            return p;
        }
        public IEnumerable<Products> getSortby(string sortby, IEnumerable<Products> p)
        {
            switch (sortby)
            {
                case "name":
                    p = p.OrderBy(s => s.ProductName);
                    break;
                case "name_desc":
                    p = p.OrderByDescending(x => x.ProductName);
                    break;
                case "price":
                    p = p.OrderBy(x => x.UnitPrice);
                    break;
                case "price_desc":
                    p = p.OrderByDescending(x => x.UnitPrice);
                    break;
                case "best_sell":
                    string query = "select PD.*" +
                        "from Products as PD left join OrderDetails as OD" +
                        "on OD.ProductId = PD.Id" +
                        "group by PD.Id, PD.ProductName, PD.UnitPrice, PD.DiscountRatio, PD.DiscountExpiry, PD.IsActive, PD.CategoryId, PD.ImgUrl, PD.Description" +
                        "order by COUNT(OD.ProductId) desc";
                    p = db.Database.SqlQuery<Products>(query);
                    break;
                default:
                    p = p.OrderByDescending(x => x.Id);
                    break;
            }
            return p;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}