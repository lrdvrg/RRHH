using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RRHH.Models;

namespace RRHH.Controllers
{
    public class CargoController : Controller
    {
        private RRHHEntities db = new RRHHEntities();

        // GET: Cargo
        public ActionResult Index()
        {
            return View(db.CARGO.ToList());
        }

        // GET: Cargo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CARGO cARGO = db.CARGO.Find(id);
            if (cARGO == null)
            {
                return HttpNotFound();
            }
            return View(cARGO);
        }

        // GET: Cargo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cargo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_CARGO,CARGO1")] CARGO cARGO)
        {
            if (ModelState.IsValid)
            {
                db.CARGO.Add(cARGO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cARGO);
        }

        // GET: Cargo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CARGO cARGO = db.CARGO.Find(id);
            if (cARGO == null)
            {
                return HttpNotFound();
            }
            return View(cARGO);
        }

        // POST: Cargo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_CARGO,CARGO1")] CARGO cARGO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cARGO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cARGO);
        }

        // GET: Cargo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CARGO cARGO = db.CARGO.Find(id);
            if (cARGO == null)
            {
                return HttpNotFound();
            }
            return View(cARGO);
        }

        // POST: Cargo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CARGO cARGO = db.CARGO.Find(id);
            db.CARGO.Remove(cARGO);
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
