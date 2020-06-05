using DOT.NET.DAL;
using DOT.NET.OwnHelpers;
using DOT.NET.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOT.NET.Controllers
{
    public class KoszykController : Controller
    {
        private KoszykMenager koszykMenager;
        private IsessionMenager sessionMenager;
        private PrzedmiotyContext db = new PrzedmiotyContext();

        public KoszykController()
        {
            sessionMenager = new SessionMenager();
            koszykMenager = new KoszykMenager(sessionMenager, db);
        }
        // GET: Koszyk
        public ActionResult Index()
        {
            var pozycjeKoszyka = koszykMenager.PobierzKoszyk();
            var cenaCalkowita = koszykMenager.PobierzWartoscKoszyka();

            KoszykViewModel koszykVM = new KoszykViewModel()
            {
                PozycjeKoszyka = pozycjeKoszyka,
                CenaCalkowita = cenaCalkowita
            };
            return View(koszykVM);
        }
        public ActionResult DodajDoKoszyka(int id)
        {
            koszykMenager.DodajDoKoszyka(id);
            return RedirectToAction("Index");
        }

        public int PobierzIloscElementowKoszyka()
        {
            return koszykMenager.PobierzIloscPozycjiKoszyka();
        }


        public ActionResult UsunZKoszyka(int pId = 0)
        {
            koszykMenager.UsunZKoszyka(pId);

            return RedirectToAction("Index");

        }

        public ActionResult Zaplac()
        {
            return RedirectToAction("/");
        }
    }

}