using Osiguranje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Osiguranje.Controllers
{
    public class PolisaController : Controller
    {

        OsiguranjeEntities db = new OsiguranjeEntities();

        [Autentifikacija]
        public ActionResult Details(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Polisa p = db.Polisas.Find(id);
            p.Premija = 0;

            foreach (Imovina i in db.Imovinas.Where(y => y.IDPolisa == p.IDPolisa))
            {
                p.Imovinas.Add(i);
                p.Premija += i.Vrednost * (Convert.ToDouble(p.Tarifa))/100;
            }

            foreach (Objekat o in db.Objekats.Where(c => c.IDPolisa == p.IDPolisa))
            {
                p.Objekats.Add(o);
                p.Premija += o.Vrednost * Convert.ToDouble(p.Tarifa)/100;
            }
            try { db.SaveChanges(); }catch (Exception) { }

            return View(p);
        }

        [HttpGet]
        [Autentifikacija]
        public ActionResult Create(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Polisa p = new Polisa();
            p.JMBG= (string)id;
            return View(p);
        }

        [HttpPost]
        [Autentifikacija]
        public ActionResult Create(Polisa p,string id)
        {
            if(ModelState.IsValid)
            {
                p.JMBG = id;
                db.Polisas.Add(p);
                try
                {
                    db.SaveChanges();
                }catch (Exception) { }


                return RedirectToAction("Details", "Polisa", new { id=p.IDPolisa });
            }
            return RedirectToAction("Details", new { id = p.JMBG });
        }

        [HttpGet]
        [Autentifikacija]
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Polisa p = db.Polisas.Where(z => z.IDPolisa == id).First();
            return View(p);
        }

        [HttpPost]
        [Autentifikacija]
        public ActionResult Edit(Polisa p)
        {
            
                Polisa editovano = db.Polisas.Find(p.IDPolisa);
                editovano.JMBG = p.JMBG;
                editovano.DatOd = p.DatOd;
                editovano.DatDo = p.DatDo;
                editovano.Tarifa = p.Tarifa;
                editovano.Premija = p.Premija;

                try
                {
                    db.SaveChanges();
                }catch (Exception) { }

                return RedirectToAction("Details", "Polisa", new { id = p.IDPolisa });
            
            
        }

        [Autentifikacija]
        public ActionResult Delete(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Polisa p = db.Polisas.Where(z => z.IDPolisa == id).First();
            string temp = p.JMBG;

            foreach (Objekat o in db.Objekats.Where(x=> x.IDPolisa == p.IDPolisa))
            {
                db.Objekats.Remove(o);
            }
            foreach (Imovina i in db.Imovinas.Where(c => c.IDPolisa == p.IDPolisa))
            {
                db.Imovinas.Remove(i);
            }
            db.Polisas.Remove(p);

            try{
                db.SaveChanges();
            }catch (Exception) { }

            return RedirectToAction("Details", "Klijent", new { id = temp });
        }
    }
}