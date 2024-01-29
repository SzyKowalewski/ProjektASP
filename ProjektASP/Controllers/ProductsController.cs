using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

using ProjektASP.Models;

using Image = ProjektASP.Models.Image;

namespace ProjektASP.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        [Authorize(Roles = "Admin")]

        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        // GET: Products/Details/5
        [Authorize(Roles = "Admin")]
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

            //ViewBag.Image = product.ImageUrl;

            return View(product);
        }

        public ActionResult GetImage(string imagePath)
        {
            return File(imagePath, "image/png"); // Tutaj musisz dostosować typ MIME w zależności od formatu obrazu
        }
        [Authorize(Roles = "Admin")]
        // GET: Products/Create
        public ActionResult Create()
        {
            Category category = new Category();
            category.Id = 0;
            category.Name = "Brak";
            var categories = db.Categories.Where(c => c.isVisible).ToList();
            categories.Insert(0, category);
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            return View();
        }
        [Authorize(Roles = "Admin")]
        // POST: Products/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, List<HttpPostedFileBase> ImageFiles, List<HttpPostedFileBase> AttachedFiles, List<string> FileDescriptions)
        {
            //if (ModelState.IsValid)
            //{
            // Handle main image upload
            if (ImageFiles != null && ImageFiles.Count > 0)
            {
                product.Images = new List<Image>(); // Inicjalizacja kolekcji zdjęć

                foreach (var imageFile in ImageFiles)
                {
                    if (imageFile != null && imageFile.ContentLength > 0)
                    {
                        string imageFileName = Path.GetFileName(imageFile.FileName);
                        string imagePath = Path.Combine(Server.MapPath("~/ImageUrl"), imageFileName);
                        imageFile.SaveAs(imagePath);
                        string path = "/ImageUrl/" + imageFileName;

                        // Create Image record
                        var imageRecord = new Image
                        {
                            ImageName = imageFileName,
                            ImagePath = path,
                            ProductId = product.Id,
                        };

                        product.Images.Add(imageRecord);
                    }
                }
            }

            // Handle attached files
            if (AttachedFiles != null && AttachedFiles.Count == FileDescriptions.Count)
                {
                    for (int i = 0; i < AttachedFiles.Count; i++)
                    {
                        var attachedFile = AttachedFiles[i];
                        var fileDescription = FileDescriptions[i];

                        if (attachedFile != null && attachedFile.ContentLength > 0)
                        {
                            string attachedFileName = Path.GetFileName(attachedFile.FileName);
                            string attachedFilePath = Path.Combine(Server.MapPath("~/FileUrl"), attachedFileName);
                            attachedFile.SaveAs(attachedFilePath);

                            // Create AttachedFile record
                            var attachedFileRecord = new AttachedFile
                            {
                                FileName = attachedFileName,
                                Description = fileDescription,
                                ProductId = product.Id,
                                FilePath = attachedFilePath
                            };

                            db.AttachedFiles.Add(attachedFileRecord);
                        }
                    }
                db.Products.Add(product);

                db.SaveChanges();
                    return RedirectToAction("Index");
                }
            //}

            // Obsługa błędów, dodaj odpowiednie komunikaty do ModelState lub ViewBag

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        public ActionResult DownloadFile(int id)
        {
            var attachedFile = db.AttachedFiles.Find(id);

            if (attachedFile != null)
            {
                var fileBytes = System.IO.File.ReadAllBytes(attachedFile.FilePath); // Dostosuj do swojej struktury danych
                var fileName = attachedFile.FileName;

                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }

            return HttpNotFound();
        }

        public ActionResult Search(string searchString)
        {
            var products = db.Products.Include(p => p.Category);

            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => p.Name.Contains(searchString));
            }

            return View(products.ToList());
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
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
            Category category = new Category();
            category.Id = 0;
            category.Name = "Brak";
            var categories = db.Categories.ToList();
            categories.Insert(0, category);
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", product.CategoryId);
            return View(product);
        }
        [Authorize(Roles = "Admin")]
        // POST: Products/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CategoryId,Description,Price,Avaliable,ImageUrl")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            Category category = new Category();
            category.Id = 0;
            category.Name = "Brak";
            var categories = db.Categories.ToList();
            categories.Insert(0, category);
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name", product.CategoryId);

            return View(product);
        }
        [Authorize(Roles = "Admin")]
        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
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
        [Authorize(Roles = "Admin")]
        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
