using DOT.NET.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOT.NET.Controllers
{
    public class PrzedmiotyController : Controller
    {
        private PrzedmiotyContext db = new PrzedmiotyContext();
        // GET: Przedmioty
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Lista(string nazwaKategori)
        {
            var kategoria = db.Kategorie.Include("Przedmioty").Where(k=>k.NazwaKategorii.ToUpper() == nazwaKategori).Single();
            var przedmioty = kategoria.Przedmioty.ToList();
            return View(przedmioty);
        }
        public ActionResult Szczegoly(string id)
        {
            return View();
        }
        [ChildActionOnly]
        public ActionResult KategorieMenu()
        {
            var kategorie = db.Kategorie.ToList();
            return PartialView("_KategorieMenu",kategorie);
        }
    }
}