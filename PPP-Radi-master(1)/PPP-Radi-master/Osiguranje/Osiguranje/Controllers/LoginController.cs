using Osiguranje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Osiguranje.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Authorize(Osiguranje.Models.Korisnik korisnik)
        {
            using (OsiguranjeEntities db = new OsiguranjeEntities())
            {
                string pass = Crypto.SHA1(korisnik.Password);

                var userDetails = db.Korisniks.Where(x => x.Username == korisnik.Username && x.Password == pass).FirstOrDefault();

                if (userDetails == null)
                {
                    korisnik.ErrorMsg = "Uneli ste pogresne podatke za prijavu!";
                    return View("Index", korisnik);
                }
                else
                {
                    Session["Username"] = korisnik.Username;
                    return RedirectToAction("Index", "Klijent");
                }
            }
        }

        public ActionResult LogOff()
        {
            Session.Abandon();

            return RedirectToAction("Index", "Login");
        }
    }
}