using Osiguranje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Osiguranje.Controllers
{
    public class PretragaController : Controller
    {
        OsiguranjeEntities db = new OsiguranjeEntities();

        [Autentifikacija]
        public ActionResult JMBG(string id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<Klijent> q = db.Klijents.Where(x => x.JMBG == id).ToList();

            if (q.Count() < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else if (q.Count() == 1)
            {
                return RedirectToAction("Details", "Klijent", new { id = q.First().JMBG });
            }
            return View("~/Views/Pretraga/Klijent.cshtml", q);
        }

        [Autentifikacija]
        public ActionResult Ime(string id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<Klijent> q = db.Klijents.Where(x => x.Ime == id).ToList();

            if (q.Count() < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else if (q.Count() == 1)
            {
                return RedirectToAction("Details", "Klijent", new { id = q.First().JMBG });
            }
            return View("~/Views/Pretraga/Klijent.cshtml", q);
        }
        [Autentifikacija]
        public ActionResult Prezime(string id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<Klijent> q = db.Klijents.Where(x => x.Prezime == id).ToList();

            if (q.Count() < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else if (q.Count() == 1)
            {
                return RedirectToAction("Details", "Klijent", new { id = q.First().JMBG });
            }
            return View("~/Views/Pretraga/Klijent.cshtml", q);
        }

        [Autentifikacija]
        public ActionResult IDPolisa(Osiguranje.Models.Polisa p, string id)
        {
            if(id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int s = Convert.ToInt32(id);

            List<Polisa> q = db.Polisas.Where(x => x.IDPolisa == s).ToList();

            if(q.Count()<1)
            {
               return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else if(q.Count()==1)
            {
                return RedirectToAction("Details", "Polisa", new { id = q.First().IDPolisa });
            }

            return View("~/Views/Pretraga/Polisa.cshtml", q);
        }

        [Autentifikacija]
        public ActionResult DatOd(string id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var s = Convert.ToDateTime(id);

            List<Polisa> q = db.Polisas.Where(x => x.DatOd == s).ToList();

            if (q.Count() < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else if (q.Count() == 1)
            {
                return RedirectToAction("Details", "Polisa", new { id = q.First().IDPolisa });
            }

            return View("~/Views/Pretraga/Polisa.cshtml", q);
        }

        [Autentifikacija]
        public ActionResult DatDo(string id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var s = Convert.ToDateTime(id);

            List<Polisa> q = db.Polisas.Where(x => x.DatDo == s).ToList();

            if (q.Count() < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else if (q.Count() == 1)
            {
                return RedirectToAction("Details", "Polisa", new { id = q.First().IDPolisa });
            }

            return View("~/Views/Pretraga/Polisa.cshtml", q);
        }

        [Autentifikacija]
        public ActionResult Tarifa(string id)
        {
            if (id == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            List<Polisa> q = db.Polisas.Where(x => x.Tarifa == id).ToList();

            if (q.Count() < 1)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else if (q.Count() == 1)
            {
                return RedirectToAction("Details", "Polisa", new { id = q.First().IDPolisa });
            }

            return View("~/Views/Pretraga/Polisa.cshtml", q);
        }
    }
}