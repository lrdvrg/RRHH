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
    public class EmpleadoController : Controller
    {
        private RRHHEntities db = new RRHHEntities();

        // GET: Empleado
        public ActionResult Index()
        {
            var eMPLEADO = db.EMPLEADO.Include(e => e.CARGO).Include(e => e.DEPARTAMENTO);
            return View(eMPLEADO.ToList());
        }

        public ActionResult EmpleadosActivos(String Nombre, String Departamento)
        {
            var provider = from s in db.EMPLEADO select s;
            if (!String.IsNullOrEmpty(Nombre))
            {
                provider = provider.Where(j => j.NOMBRE.Contains(Nombre)).Where(e => e.ESTATUS == "A");
            } else if(!String.IsNullOrEmpty(Departamento))
            {
                provider = provider.Where(x => x.DEPARTAMENTO.NOMBRE.Contains(Departamento)).Where(e => e.ESTATUS == "A");
            } else
            {
                provider = provider.Where(e => e.ESTATUS == "A");
            }
            return View(provider.ToList());
        }

        public ActionResult EmpleadosPorMes(String Mes)
        {
            var provider = from s in db.EMPLEADO select s;
            if (!String.IsNullOrEmpty(Mes))
            {
                int mes = Convert.ToInt32(Mes);
                //DateTime dt = DateTime.Parse(Mes);
                provider = provider.Where(j =>j.FECHA_INGRESO.Value.Month == mes).Where(e => e.ESTATUS == "A");
            }
            else
            {
                provider = provider.Where(e => e.ESTATUS == "A");
            }
            return View(provider.ToList());
        }

        public ActionResult EmpleadosInactivos()
        {
            var eMPLEADO = db.EMPLEADO.Where(e => e.ESTATUS == "I");
            return View(eMPLEADO.ToList());
        }


        // GET: Empleado/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLEADO eMPLEADO = db.EMPLEADO.Find(id);
            if (eMPLEADO == null)
            {
                return HttpNotFound();
            }
            return View(eMPLEADO);
        }

        // GET: Empleado/Create
        public ActionResult Create()
        {
            ViewBag.ID_CARGO = new SelectList(db.CARGO, "ID_CARGO", "CARGO1");
            ViewBag.ID_DEPARTAMENTO = new SelectList(db.DEPARTAMENTO, "ID_DEPARTAMENTO", "NOMBRE");
            return View();
        }

        // POST: Empleado/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_EMPLEADO,CODIGO_EMPLEADO,NOMBRE,APELLIDO,TELÉFONO,ID_DEPARTAMENTO,ID_CARGO,FECHA_INGRESO,SALARIO,ESTATUS")] EMPLEADO eMPLEADO)
        {
            if (ModelState.IsValid)
            {
                db.EMPLEADO.Add(eMPLEADO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_CARGO = new SelectList(db.CARGO, "ID_CARGO", "CARGO1", eMPLEADO.ID_CARGO);
            ViewBag.ID_DEPARTAMENTO = new SelectList(db.DEPARTAMENTO, "ID_DEPARTAMENTO", "NOMBRE", eMPLEADO.ID_DEPARTAMENTO);
            return View(eMPLEADO);
        }

        // GET: Empleado/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLEADO eMPLEADO = db.EMPLEADO.Find(id);
            if (eMPLEADO == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_CARGO = new SelectList(db.CARGO, "ID_CARGO", "CARGO1", eMPLEADO.ID_CARGO);
            ViewBag.ID_DEPARTAMENTO = new SelectList(db.DEPARTAMENTO, "ID_DEPARTAMENTO", "NOMBRE", eMPLEADO.ID_DEPARTAMENTO);
            return View(eMPLEADO);
        }

        // POST: Empleado/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_EMPLEADO,CODIGO_EMPLEADO,NOMBRE,APELLIDO,TELÉFONO,ID_DEPARTAMENTO,ID_CARGO,FECHA_INGRESO,SALARIO,ESTATUS")] EMPLEADO eMPLEADO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eMPLEADO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_CARGO = new SelectList(db.CARGO, "ID_CARGO", "CARGO1", eMPLEADO.ID_CARGO);
            ViewBag.ID_DEPARTAMENTO = new SelectList(db.DEPARTAMENTO, "ID_DEPARTAMENTO", "NOMBRE", eMPLEADO.ID_DEPARTAMENTO);
            return View(eMPLEADO);
        }

        // GET: Empleado/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLEADO eMPLEADO = db.EMPLEADO.Find(id);
            if (eMPLEADO == null)
            {
                return HttpNotFound();
            }
            return View(eMPLEADO);
        }

        // POST: Empleado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EMPLEADO eMPLEADO = db.EMPLEADO.Find(id);
            db.EMPLEADO.Remove(eMPLEADO);
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
