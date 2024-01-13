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
    public class AddressModelController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AddressModels
        public ActionResult Index()
        {
            return View(db.AddressModels.ToList());
        }

        // GET: AddressModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddressModel addressModels = db.AddressModels.Find(id);
            if (addressModels == null)
            {
                return HttpNotFound();
            }
            return View(addressModels);
        }

        // GET: AddressModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AddressModels/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ZipCode,City,StreetAndBuildingNumber,ApartmentNumber")] AddressModel addressModels)
        {
            if (ModelState.IsValid)
            {
                db.AddressModels.Add(addressModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(addressModels);
        }

        // GET: AddressModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddressModel addressModels = db.AddressModels.Find(id);
            if (addressModels == null)
            {
                return HttpNotFound();
            }
            return View(addressModels);
        }

        // POST: AddressModels/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ZipCode,City,StreetAndBuildingNumber,ApartmentNumber")] AddressModel addressModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(addressModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(addressModels);
        }

        // GET: AddressModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AddressModel addressModels = db.AddressModels.Find(id);
            if (addressModels == null)
            {
                return HttpNotFound();
            }
            return View(addressModels);
        }

        // POST: AddressModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AddressModel addressModels = db.AddressModels.Find(id);
            db.AddressModels.Remove(addressModels);
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
