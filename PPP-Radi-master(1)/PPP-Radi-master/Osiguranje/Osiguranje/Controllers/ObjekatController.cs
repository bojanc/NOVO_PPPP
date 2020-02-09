using Osiguranje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Osiguranje.Controllers
{
    public class ObjekatController : Controller
    {
        private OsiguranjeEntities db = new OsiguranjeEntities();

        [HttpGet]
        [Autentifikacija]
        public ActionResult Create(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Objekat o = new Objekat();

            o.IDPolisa = (int)id;
            ViewBag.IDPolisa = (int)id;
            return View(o);
        }

        [HttpPost]
        [Autentifikacija]
        public ActionResult Create(Objekat o, int id)
        {
            if(ModelState.IsValid)
            {
                o.IDPolisa = id;
                db.Objekats.Add(o);
                try
                {
                    db.SaveChanges();
                }
                catch(Exception)
                {

                }
            }

            return RedirectToAction("Details", "Polisa", new { id = o.IDPolisa });
        }

        [HttpGet]
        [Autentifikacija]
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Objekat o = db.Objekats.Find(id);
            if(o == null)
            {
                return HttpNotFound();
            }

            return View(o);
        }

        [HttpPost]
        [Autentifikacija]
        public ActionResult Edit(Objekat o)
        {
            Objekat editovan = new Objekat();
            if(ModelState.IsValid)
            {
                editovan = db.Objekats.Find(o.IDObjekat);
                editovan.Opis = o.Opis;
                editovan.Povrsina = o.Povrsina;
                editovan.Vrednost = o.Vrednost;
            }
            try
            {
                db.SaveChanges();
            }
            catch(Exception)
            {

            }

            return RedirectToAction("Details", "Polisa", new { id = editovan.IDPolisa });
        }

        [Autentifikacija]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Objekat o = db.Objekats.Find(id);
            int temp = o.IDPolisa;
            try
            {
                db.Objekats.Remove(o);
                db.SaveChanges();
            }
            catch (Exception)
            {

            }

            return RedirectToAction("Details", "Polisa", new { id = temp });
        }
    }
}