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
    public class PermisoController : Controller
    {
        private RRHHEntities db = new RRHHEntities();

        // GET: Permiso
        public ActionResult Index()
        {
            var pERMISO = db.PERMISO.Include(p => p.EMPLEADO);
            return View(pERMISO.ToList());
        }

        public ActionResult PermisoPorEmpleado(String Nombre)
        {
            var provider = from s in db.PERMISO select s;
            if (!String.IsNullOrEmpty(Nombre))
            {
                provider = provider.Where(j => j.EMPLEADO.NOMBRE.Contains(Nombre)).Where(e => e.EMPLEADO.ESTATUS == "A");
            }
            else
            {
                provider = provider.Where(e => e.EMPLEADO.ESTATUS == "A");
            }
            return View(provider.ToList());
        }

        // GET: Permiso/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERMISO pERMISO = db.PERMISO.Find(id);
            if (pERMISO == null)
            {
                return HttpNotFound();
            }
            return View(pERMISO);
        }

        // GET: Permiso/Create
        public ActionResult Create()
        {
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADO, "ID_EMPLEADO", "CODIGO_EMPLEADO");
            return View();
        }

        // POST: Permiso/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PERMISO,ID_EMPLEADO,DESDE,HASTA,COMENTARIOS")] PERMISO pERMISO)
        {
            if (ModelState.IsValid)
            {
                db.PERMISO.Add(pERMISO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADO, "ID_EMPLEADO", "CODIGO_EMPLEADO", pERMISO.ID_EMPLEADO);
            return View(pERMISO);
        }

        // GET: Permiso/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERMISO pERMISO = db.PERMISO.Find(id);
            if (pERMISO == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADO, "ID_EMPLEADO", "CODIGO_EMPLEADO", pERMISO.ID_EMPLEADO);
            return View(pERMISO);
        }

        // POST: Permiso/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PERMISO,ID_EMPLEADO,DESDE,HASTA,COMENTARIOS")] PERMISO pERMISO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pERMISO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADO, "ID_EMPLEADO", "CODIGO_EMPLEADO", pERMISO.ID_EMPLEADO);
            return View(pERMISO);
        }

        // GET: Permiso/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PERMISO pERMISO = db.PERMISO.Find(id);
            if (pERMISO == null)
            {
                return HttpNotFound();
            }
            return View(pERMISO);
        }

        // POST: Permiso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PERMISO pERMISO = db.PERMISO.Find(id);
            db.PERMISO.Remove(pERMISO);
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
