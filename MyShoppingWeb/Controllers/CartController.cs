﻿using MyShoppingWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShoppingWeb.Controllers
{
    public class CartController : Controller
    {
        MyShoppingWebDbContext db = new MyShoppingWebDbContext();
        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Binding(string data)//string type
        {

           

            var cart_items = JsonConvert.DeserializeObject<List<CartItem>>(data);
            if (cart_items == null || cart_items.Count == 0)
            {
                return PartialView("_Empty");
            }


            List<CartVM> lstCart = new List<CartVM>();
            CartVM ca;
            double subTotal = 0;
            foreach (var item in cart_items)
            {
                Products p = db.Products.Find(item.productid);
                if (p != null)
                {
                 
                    double tien = (p.UnitPrice - ((p.UnitPrice * p.DiscountRatio.Value) / 100)) *item.quantity;

                    ca = new CartVM();
                    ca.ProductId = p.Id;
                    ca.ProductName = p.ProductName;
                    ca.UnitPrice = (p.UnitPrice - ((p.UnitPrice * p.DiscountRatio) / 100)).ToString();
                    ca.Quantity = item.quantity;
                    ca.ImgUrl = "/data/products/" + p.Id + "/" + p.ImgUrl;
                    ca.Total = tien.ToString();
                    //ca.AllTotal = (int.Parse(ca.Total) - int.Parse(ca.Total) * 0.1).ToString();
                    lstCart.Add(ca);

                    subTotal = subTotal + tien;

                }
            }
            double vat = 10;
            ViewBag.VAT = vat;
            ViewBag.SubTotal = subTotal.ToString("N2");

            double tongToanBo = subTotal + (subTotal * (vat / 100));
            ViewBag.GrandTotal = tongToanBo.ToString("N2");
            return PartialView("_Cart", lstCart);
        }
    }
}