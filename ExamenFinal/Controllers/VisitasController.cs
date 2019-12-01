using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExamenFinal.Models;

namespace ExamenFinal.Controllers
{
    public class VisitasController : Controller
    {
        private Tarea_FinalEntities1 db = new Tarea_FinalEntities1();

        // GET: Visitas
        public ActionResult Index()
        {
            var visitas = db.Visitas.Include(v => v.Area1).Include(v => v.Persona1);
            return View(visitas.ToList());
        }

        // GET: Visitas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visitas visitas = db.Visitas.Find(id);
            if (visitas == null)
            {
                return HttpNotFound();
            }
            return View(visitas);
        }

        // GET: Visitas/Create
        public ActionResult Create()
        {
            ViewBag.Area = new SelectList(db.Area, "idArea", "Nombre");
            ViewBag.Persona = new SelectList(db.Persona, "idPersona", "Nombre");
            return View();
        }

        // POST: Visitas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idVisitas,Fecha,HoraEntrada,HoraSalida,Motivo_Visita,Persona,Area")] Visitas visitas)
        {
            if (ModelState.IsValid)
            {
                db.Visitas.Add(visitas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Area = new SelectList(db.Area, "idArea", "Nombre", visitas.Area);
            ViewBag.Persona = new SelectList(db.Persona, "idPersona", "Nombre", visitas.Persona);
            return View(visitas);
        }

        // GET: Visitas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visitas visitas = db.Visitas.Find(id);
            if (visitas == null)
            {
                return HttpNotFound();
            }
            ViewBag.Area = new SelectList(db.Area, "idArea", "Nombre", visitas.Area);
            ViewBag.Persona = new SelectList(db.Persona, "idPersona", "Nombre", visitas.Persona);
            return View(visitas);
        }

        // POST: Visitas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idVisitas,Fecha,HoraEntrada,HoraSalida,Motivo_Visita,Persona,Area")] Visitas visitas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(visitas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Area = new SelectList(db.Area, "idArea", "Nombre", visitas.Area);
            ViewBag.Persona = new SelectList(db.Persona, "idPersona", "Nombre", visitas.Persona);
            return View(visitas);
        }

        // GET: Visitas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Visitas visitas = db.Visitas.Find(id);
            if (visitas == null)
            {
                return HttpNotFound();
            }
            return View(visitas);
        }

        // POST: Visitas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Visitas visitas = db.Visitas.Find(id);
            db.Visitas.Remove(visitas);
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
