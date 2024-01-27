using Newtonsoft.Json;
using ProjektASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjektASP.Controllers
{
    public class BasketController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Basket
        public ActionResult BasketView()
        {
            List<Product> products = new List<Product>();

            HttpCookie cookie = Request.Cookies["CartCookie"];

            if (cookie != null)
            {
                string cartContent = cookie.Value.ToString();
                List<KeyValuePair<string, int>> cartItems = JsonConvert.DeserializeObject<List<KeyValuePair<string, int>>>(cartContent);

                foreach (var id2 in cartItems)
                {
                    int id = Convert.ToInt32(id2.Key);
                    var query1 = db.Products.Where(x => x.Id == id).FirstOrDefault();
                    Product prod = db.Products.Find(id);
                    if (prod.Avaliable == false && id2.Value > 0)
                    {
                        TempData["InsufficientStock"] = "Wybrany produkt" + prod.Name + "jest w tym momencie niedostępny. ";
                    }
                    Product prod2 = new Product { Id = prod.Id, Name = prod.Name, Price = (prod.Price) * id2.Value};
                    products.Add(prod2);
                }
            }
            else
            {
                ViewBag.Message = "Ciasteczko nie istnieje";
                return View(products); ;
            }


            return View(products);
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
        /*[HttpPost]
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

            return View(product);
        }*/

        public ActionResult Deleted(int? id)
        {
            List<Product> products = new List<Product>();

            HttpCookie cookie = Request.Cookies["CartCookie"];

            if (cookie != null)
            {
                string idc = Convert.ToString(id);
                string cartContent = cookie.Value.ToString();
                List<KeyValuePair<string, int>> cartItems = JsonConvert.DeserializeObject<List<KeyValuePair<string, int>>>(cartContent);

                for (int i = 0; i < cartItems.Count; i++)
                {
                    if (cartItems[i].Key == idc)
                    {
                        if (cartItems[i].Value > 1)
                        {
                            cartItems[i] = new KeyValuePair<string, int>(cartItems[i].Key, cartItems[i].Value - 1);
                        }
                        else
                        {
                            cartItems.RemoveAt(i);
                        }
                        break;
                    }
                }
                string cartData = JsonConvert.SerializeObject(cartItems);
                cookie.Value = cartData;
                Response.Cookies.Add(cookie);
            }
            else
            {
                ViewBag.Message = "Ciasteczko nie istnieje";
                return View(products); ;
            }
            return RedirectToAction("BasketView");
        }

        public ActionResult Order()
        {
            List<Product> products = new List<Product>();

            HttpCookie cookie = Request.Cookies["CartCookie"];

            if (cookie != null)
            {
                string cartContent = cookie.Value.ToString();
                List<KeyValuePair<string, int>> cartItems = JsonConvert.DeserializeObject<List<KeyValuePair<string, int>>>(cartContent);

                foreach (var id2 in cartItems)
                {
                    int id = Convert.ToInt32(id2.Key);
                    Product prod = db.Products.Find(id);
                    Product prod2 = new Product { Id = prod.Id, Name = prod.Name, Price = (prod.Price) * id2.Value };
                    for ( int i = 0; i < id2.Value; i++ )
                    {
                        products.Add(prod2);
                    }
                }
            }

            if (products.Count < 1)
            {
                return RedirectToAction("Index", "Home");
            }
            cookie = Request.Cookies["CartCookie"];
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookie);
            return View();
        }
    }
}