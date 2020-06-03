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

            List<Kategoria> listaKategori = db.Kategorie.ToList();
            return View();
        }
    }
}