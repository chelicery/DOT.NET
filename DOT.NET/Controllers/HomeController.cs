using DOT.NET.DAL;
using DOT.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOT.NET.Controllers
{
    public class HomeController : Controller
    {
        private KursyContext db = new KursyContext();
        // GET: Home
        public ActionResult Index()
        {
            Kategoria kategoria = new Kategoria { NazwaKategorii = "asp.net mvc", NazwaPlikuIkony = "aspNetMvc.png", OpisKategorii = "opis kategorii" };
            db.Kategorie.Add(kategoria);
            db.SaveChanges();
            
            return View();
        }
    }
}