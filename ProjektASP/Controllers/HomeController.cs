using ProjektASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

            var productList = db.Products.ToList();

            return View(productList);
        }

        [HttpPost]
        public ActionResult Index(Category cat)
        {
            var productList = new Object();
            if (cat.Id == 0) 
                productList = db.Products.ToList();
            else 
                productList = db.Products.Where(s => s.Category.Id == cat.Id).ToList();

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

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}