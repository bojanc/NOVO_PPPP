using Osiguranje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Osiguranje.Controllers
{
    public class ImovinaController : Controller
    {
        OsiguranjeEntities db = new OsiguranjeEntities();

        [HttpGet]
        [Autentifikacija]
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Imovina i = db.Imovinas.Find(id);
            return View(i);
        }

        [HttpPost]
        [Autentifikacija]
        public ActionResult Edit(Imovina i)
        {
            Imovina editovan = new Imovina();
            if(ModelState.IsValid)
            {
                editovan = db.Imovinas.Find(i.IDImovina);
                editovan.Opis = i.Opis;
                editovan.Vrednost = i.Vrednost;
            }
            try
            {
                db.SaveChanges();
            }
            catch(Exception)
            {

            }

            return RedirectToAction("Details", "Polisa", new { id = editovan.IDPolisa});
        }

        [HttpGet]
        [Autentifikacija]
        public ActionResult Create(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Imovina i = new Imovina();
            i.IDPolisa = (int)id;
            ViewBag.IDPolisa = (int)id;
            return View(i);
        }

        [HttpPost]
        [Autentifikacija]
        public ActionResult Create(Imovina i, int id)
        {
            if(ModelState.IsValid)
            {
                i.IDPolisa = id;
                db.Imovinas.Add(i);
                try
                {
                    db.SaveChanges();
                }
                catch(Exception)
                {

                }
            }

            return RedirectToAction("Details", "Polisa", new { id = i.IDPolisa });
        }

        [Autentifikacija]
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Imovina i = db.Imovinas.Find(id);
            int temp = i.IDPolisa;
            try
            {
                db.Imovinas.Remove(i);
                db.SaveChanges();
            }
            catch(Exception)
            {

            }

            return RedirectToAction("Details", "Polisa", new { id = temp });
        }
    }
}