using Osiguranje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Osiguranje.Controllers
{
    public class PretragaController : Controller
    {
        OsiguranjeEntities db = new OsiguranjeEntities();

        [Autentifikacija]
        public ActionResult Prezime(string s)
        {
            string test = s;
            return View();
        }
    }
}