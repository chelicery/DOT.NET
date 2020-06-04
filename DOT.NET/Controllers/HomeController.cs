using DOT.NET.DAL;
using DOT.NET.Models;
using DOT.NET.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOT.NET.Controllers
{
    public class HomeController : Controller
    {
        private PrzedmiotyContext db = new PrzedmiotyContext();
        // GET: Home
        public ActionResult Index()
        {

            var kategorie = db.Kategorie.ToList();
            var nowosci = db.Przedmioty.Where(a => !a.Ukryty).OrderByDescending(a => a.DataDodania).Take(3).ToList();
            var bestseller = db.Przedmioty.Where(a => !a.Ukryty && a.Bestseller).OrderBy(a => Guid.NewGuid()).Take(3).ToList();


            var viewmodel = new HomeViewModel()
            {
                Kategorie = kategorie,
                Nowosci = nowosci,
                Bestsellery = bestseller
            };
            return View(viewmodel);
        }

        public ActionResult StronyStatyczne(string nazwa)
        {
            return View(nazwa);
        }
    }
}