using Osiguranje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Osiguranje.Controllers
{
    public class KlijentController : Controller
    {
        OsiguranjeEntities db = new OsiguranjeEntities();

        // GET: Klijent
        [Autentifikacija]
        public ActionResult Index()
        {
            return View(db.Klijents.ToList());
        }

        [HttpGet]
        [Autentifikacija]
        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [Autentifikacija]
        public ActionResult Create(Klijent k)
        {
            db.Klijents.Add(k);

            try
            {
                db.SaveChanges();
            }
            catch(Exception)
            {

            }

            return RedirectToAction("Details", "Klijent", new { id=k.JMBG});
        }

        [HttpGet]
        [Autentifikacija]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Klijent k = db.Klijents.Find(id);
            if(k==null)
            {
                return HttpNotFound();
            }

            return View(k);
        }

        [HttpPost]
        [Autentifikacija]
        public ActionResult Edit(Klijent k)
        {
            Klijent editovan = db.Klijents.Find(k.JMBG);
            editovan.Ime = k.Ime;
            editovan.Prezime = k.Prezime;
            editovan.Email = k.Email;
            editovan.BrojTelefona = k.BrojTelefona;
            editovan.DatumRodjenja = k.DatumRodjenja;

            if(ModelState.IsValid)
            {
                try
                {
                    db.SaveChanges();
                }
                catch(Exception)
                {

                }
            }

            return RedirectToAction("Details",new { id = editovan.JMBG });
        }

        [HttpGet]
        [Autentifikacija]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Klijent k = db.Klijents.Find(id);

            foreach (Polisa p in db.Polisas.Where(x => x.JMBG == id))
            {
                k.Polisas.Add(p);
                foreach (Imovina i in db.Imovinas.Where(y => y.IDPolisa == p.IDPolisa))
                {
                    p.Imovinas.Add(i);
                }

                foreach (Objekat o in db.Objekats.Where(c => c.IDPolisa == p.IDPolisa))
                {
                    p.Objekats.Add(o);
                }
            }

            if (k == null)
            {
                return HttpNotFound();
            }

            return View(k);
        }

        [Autentifikacija]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Klijent k = db.Klijents.Find(id);

            foreach (Polisa p in db.Polisas.Where(x => x.JMBG == id))
            {
                foreach (Imovina i in db.Imovinas.Where(y => y.IDPolisa == p.IDPolisa))
                {
                    db.Imovinas.Remove(i);
                }

                foreach (Objekat o in db.Objekats.Where(c => c.IDPolisa == p.IDPolisa))
                {
                    db.Objekats.Remove(o);
                }
                db.Polisas.Remove(p);
            }
            try {
                db.Klijents.Remove(k);
                db.SaveChanges();
            } catch (Exception) { }
            return RedirectToAction("Index","Klijent");
        }
    }
}