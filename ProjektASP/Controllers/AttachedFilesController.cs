using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjektASP.Models;

namespace ProjektASP.Controllers
{
    public class AttachedFilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AttachedFiles
        public ActionResult Index()
        {
            var attachedFiles = db.AttachedFiles.Include(a => a.Product);
            return View(attachedFiles.ToList());
        }

        // GET: AttachedFiles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttachedFile attachedFile = db.AttachedFiles.Find(id);
            if (attachedFile == null)
            {
                return HttpNotFound();
            }
            return View(attachedFile);
        }

        // GET: AttachedFiles/Create
        public ActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name");
            return View();
        }

        // POST: AttachedFiles/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FileName,Description,ProductId")] AttachedFile attachedFile)
        {
            if (ModelState.IsValid)
            {
                db.AttachedFiles.Add(attachedFile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", attachedFile.ProductId);
            return View(attachedFile);
        }

        // GET: AttachedFiles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttachedFile attachedFile = db.AttachedFiles.Find(id);
            if (attachedFile == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", attachedFile.ProductId);
            return View(attachedFile);
        }

        // POST: AttachedFiles/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FileName,Description,ProductId")] AttachedFile attachedFile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attachedFile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductId = new SelectList(db.Products, "Id", "Name", attachedFile.ProductId);
            return View(attachedFile);
        }

        // GET: AttachedFiles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttachedFile attachedFile = db.AttachedFiles.Find(id);
            if (attachedFile == null)
            {
                return HttpNotFound();
            }
            return View(attachedFile);
        }

        // POST: AttachedFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AttachedFile attachedFile = db.AttachedFiles.Find(id);
            db.AttachedFiles.Remove(attachedFile);
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
