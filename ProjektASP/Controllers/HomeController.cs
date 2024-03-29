﻿using Newtonsoft.Json;
using ProjektASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjektASP.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            Category category = new Category();
            category.Id = 0;
            category.Name = "Brak";
            var categories = db.Categories.ToList();
            categories.Insert(0, category);
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");

            var productList = db.Products.Where(s => s.Avaliable == true ).ToList();

            return View(productList);
        }

        [HttpPost]
        public ActionResult Index(Category cat)
        {
            var productList = new Object();
            if (cat.Id == 0) 
                productList = db.Products.Where(s => s.Avaliable == true).ToList();
            else 
                productList = db.Products.Where(s => s.Category.Id == cat.Id).Where(s => s.Avaliable == true).ToList();

            Category category = new Category();
            category.Id = 0;
            category.Name = "Brak";

            var categories = db.Categories.ToList();
            categories.Insert(0, category);
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            return View(productList);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        [HttpPost]
        public ActionResult Details(int? id, int? pusty)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            var productid = Convert.ToString(product.Id);

            List<KeyValuePair<string, int>> itemsInCart;
            HttpCookie cookie = Request.Cookies["CartCookie"];


            if (cookie != null)
            {
                string existingCart = cookie.Value.ToString();
                itemsInCart = JsonConvert.DeserializeObject<List<KeyValuePair<string, int>>>(existingCart);
            }
            else
            {
                cookie = new HttpCookie("CartCookie");
                itemsInCart = new List<KeyValuePair<string, int>>();
            }

            bool productFound = false;
            for (int i = 0; i < itemsInCart.Count; i++)
            {
                if (itemsInCart[i].Key == productid)
                {
                    itemsInCart[i] = new KeyValuePair<string, int>(itemsInCart[i].Key, itemsInCart[i].Value + 1);
                    productFound = true;
                    break;
                }
            }

            if (!productFound)
            {
                itemsInCart.Add(new KeyValuePair<string, int>(productid, 1));
            }

            string cartData = JsonConvert.SerializeObject(itemsInCart);
            cookie.Value = cartData;
            Response.Cookies.Add(cookie);

            return View(product);
        }
    }
}