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
    public class LicenciaController : Controller
    {
        private RRHHEntities db = new RRHHEntities();

        // GET: Licencia
        public ActionResult Index()
        {
            var lICENCIA = db.LICENCIA.Include(l => l.EMPLEADO);
            return View(lICENCIA.ToList());
        }

        // GET: Licencia/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LICENCIA lICENCIA = db.LICENCIA.Find(id);
            if (lICENCIA == null)
            {
                return HttpNotFound();
            }
            return View(lICENCIA);
        }

        // GET: Licencia/Create
        public ActionResult Create()
        {
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADO, "ID_EMPLEADO", "CODIGO_EMPLEADO");
            return View();
        }

        // POST: Licencia/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_LICECIA,ID_EMPLEADO,DESDE,HASTA,MOTIVO,COMENTARIOS")] LICENCIA lICENCIA)
        {
            if (ModelState.IsValid)
            {
                db.LICENCIA.Add(lICENCIA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADO, "ID_EMPLEADO", "CODIGO_EMPLEADO", lICENCIA.ID_EMPLEADO);
            return View(lICENCIA);
        }

        // GET: Licencia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LICENCIA lICENCIA = db.LICENCIA.Find(id);
            if (lICENCIA == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADO, "ID_EMPLEADO", "CODIGO_EMPLEADO", lICENCIA.ID_EMPLEADO);
            return View(lICENCIA);
        }

        // POST: Licencia/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_LICECIA,ID_EMPLEADO,DESDE,HASTA,MOTIVO,COMENTARIOS")] LICENCIA lICENCIA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lICENCIA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_EMPLEADO = new SelectList(db.EMPLEADO, "ID_EMPLEADO", "CODIGO_EMPLEADO", lICENCIA.ID_EMPLEADO);
            return View(lICENCIA);
        }

        // GET: Licencia/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LICENCIA lICENCIA = db.LICENCIA.Find(id);
            if (lICENCIA == null)
            {
                return HttpNotFound();
            }
            return View(lICENCIA);
        }

        // POST: Licencia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LICENCIA lICENCIA = db.LICENCIA.Find(id);
            db.LICENCIA.Remove(lICENCIA);
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
