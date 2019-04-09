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
    public class SalidaController : Controller
    {
        private RRHHEntities db = new RRHHEntities();

        // GET: Salida
        public ActionResult Index()
        {
            var sALIDA_EMPLEADO = db.SALIDA_EMPLEADO.Include(s => s.EMPLEADO);
            return View(sALIDA_EMPLEADO.ToList());
        }

        public ActionResult SalidaPorMes(String Mes)
        {
            var provider = from s in db.SALIDA_EMPLEADO select s;
            if (!String.IsNullOrEmpty(Mes))
            {
                int mes = Convert.ToInt32(Mes);
                //DateTime dt = DateTime.Parse(Mes);
                provider = provider.Where(j => j.FECHA_SALIDA.Value.Month == mes).Where(e => e.EMPLEADO.ESTATUS == "I");
            }
            else
            {
                provider = provider.Where(e => e.EMPLEADO.ESTATUS == "I");
            }
            return View(provider.ToList());
        }

        // GET: Salida/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SALIDA_EMPLEADO sALIDA_EMPLEADO = db.SALIDA_EMPLEADO.Find(id);
            if (sALIDA_EMPLEADO == null)
            {
                return HttpNotFound();
            }
            return View(sALIDA_EMPLEADO);
        }

        // GET: Salida/Create
        public ActionResult Create()
        {
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADO, "ID_EMPLEADO", "CODIGO_EMPLEADO");
            return View();
        }

        // POST: Salida/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_SALIDA_EMPLEADO,ID_EMPLEADO,TIPO_SALIDA,MOTIVO,FECHA_SALIDA")] SALIDA_EMPLEADO sALIDA_EMPLEADO)
        {
            if (ModelState.IsValid)
            {
                db.SALIDA_EMPLEADO.Add(sALIDA_EMPLEADO);

                var query = (from a in db.EMPLEADO
                             where a.ID_EMPLEADO == sALIDA_EMPLEADO.ID_EMPLEADO
                             select a).FirstOrDefault();

                query.ESTATUS = "I";

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADO, "ID_EMPLEADO", "CODIGO_EMPLEADO", sALIDA_EMPLEADO.ID_EMPLEADO);
            return View(sALIDA_EMPLEADO);
        }

        // GET: Salida/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SALIDA_EMPLEADO sALIDA_EMPLEADO = db.SALIDA_EMPLEADO.Find(id);
            if (sALIDA_EMPLEADO == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADO, "ID_EMPLEADO", "CODIGO_EMPLEADO", sALIDA_EMPLEADO.ID_EMPLEADO);
            return View(sALIDA_EMPLEADO);
        }

        // POST: Salida/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_SALIDA_EMPLEADO,ID_EMPLEADO,TIPO_SALIDA,MOTIVO,FECHA_SALIDA")] SALIDA_EMPLEADO sALIDA_EMPLEADO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sALIDA_EMPLEADO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADO, "ID_EMPLEADO", "CODIGO_EMPLEADO", sALIDA_EMPLEADO.ID_EMPLEADO);
            return View(sALIDA_EMPLEADO);
        }

        // GET: Salida/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SALIDA_EMPLEADO sALIDA_EMPLEADO = db.SALIDA_EMPLEADO.Find(id);
            if (sALIDA_EMPLEADO == null)
            {
                return HttpNotFound();
            }
            return View(sALIDA_EMPLEADO);
        }

        // POST: Salida/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SALIDA_EMPLEADO sALIDA_EMPLEADO = db.SALIDA_EMPLEADO.Find(id);
            db.SALIDA_EMPLEADO.Remove(sALIDA_EMPLEADO);
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
