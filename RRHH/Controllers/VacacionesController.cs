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
    public class VacacionesController : Controller
    {
        private RRHHEntities db = new RRHHEntities();

        // GET: Vacaciones
        public ActionResult Index()
        {
            var vACACIONES = db.VACACIONES.Include(v => v.EMPLEADO);
            return View(vACACIONES.ToList());
        }

        // GET: Vacaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VACACIONES vACACIONES = db.VACACIONES.Find(id);
            if (vACACIONES == null)
            {
                return HttpNotFound();
            }
            return View(vACACIONES);
        }

        // GET: Vacaciones/Create
        public ActionResult Create()
        {
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADO, "ID_EMPLEADO", "CODIGO_EMPLEADO");
            return View();
        }

        // POST: Vacaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_VACACIONES,ID_EMPLEADO,DESDE,HASTA,CORRESPONDIENTE,COMENTARIO")] VACACIONES vACACIONES)
        {
            if (ModelState.IsValid)
            {
                db.VACACIONES.Add(vACACIONES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADO, "ID_EMPLEADO", "CODIGO_EMPLEADO", vACACIONES.ID_EMPLEADO);
            return View(vACACIONES);
        }

        // GET: Vacaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VACACIONES vACACIONES = db.VACACIONES.Find(id);
            if (vACACIONES == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADO, "ID_EMPLEADO", "CODIGO_EMPLEADO", vACACIONES.ID_EMPLEADO);
            return View(vACACIONES);
        }

        // POST: Vacaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_VACACIONES,ID_EMPLEADO,DESDE,HASTA,CORRESPONDIENTE,COMENTARIO")] VACACIONES vACACIONES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vACACIONES).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADO, "ID_EMPLEADO", "CODIGO_EMPLEADO", vACACIONES.ID_EMPLEADO);
            return View(vACACIONES);
        }

        // GET: Vacaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VACACIONES vACACIONES = db.VACACIONES.Find(id);
            if (vACACIONES == null)
            {
                return HttpNotFound();
            }
            return View(vACACIONES);
        }

        // POST: Vacaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VACACIONES vACACIONES = db.VACACIONES.Find(id);
            db.VACACIONES.Remove(vACACIONES);
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
