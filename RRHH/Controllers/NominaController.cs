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
    public class NominaController : Controller
    {
        private RRHHEntities db = new RRHHEntities();

        // GET: Nomina
        public ActionResult Index(String Mes, String Ano)
        {
            var provider = from s in db.NOMINA select s;
            if (!String.IsNullOrEmpty(Mes))
            {
                int mes = Convert.ToInt32(Mes);
                //DateTime dt = DateTime.Parse(Mes);
                provider = provider.Where(j => j.MES == mes);
            }
            else if(!String.IsNullOrEmpty(Ano))
            {
                int ano = Convert.ToInt32(Ano);
                //DateTime dt = DateTime.Parse(Mes);
                provider = provider.Where(j => j.ANIO == ano);
            }
            return View(provider.ToList());
        }

        // GET: Nomina/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NOMINA nOMINA = db.NOMINA.Find(id);
            if (nOMINA == null)
            {
                return HttpNotFound();
            }
            return View(nOMINA);
        }

        // GET: Nomina/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Nomina/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_NOMINA,ANIO,MES,MONTO_TOTAL")] NOMINA nOMINA)
        {
            if (ModelState.IsValid)
            {
                Nullable<decimal> total = (from result in db.EMPLEADO where result.ESTATUS == "A" select result.SALARIO).Sum();
                if (ModelState.IsValid)
                {
                    db.NOMINA.Add(nOMINA);
                    nOMINA.MONTO_TOTAL = total;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(nOMINA);
        }

        // GET: Nomina/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NOMINA nOMINA = db.NOMINA.Find(id);
            if (nOMINA == null)
            {
                return HttpNotFound();
            }
            return View(nOMINA);
        }

        // POST: Nomina/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_NOMINA,ANIO,MES,MONTO_TOTAL")] NOMINA nOMINA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nOMINA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nOMINA);
        }

        // GET: Nomina/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NOMINA nOMINA = db.NOMINA.Find(id);
            if (nOMINA == null)
            {
                return HttpNotFound();
            }
            return View(nOMINA);
        }

        // POST: Nomina/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NOMINA nOMINA = db.NOMINA.Find(id);
            db.NOMINA.Remove(nOMINA);
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
