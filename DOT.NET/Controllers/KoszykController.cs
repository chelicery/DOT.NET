using DOT.NET.App_Start;
using DOT.NET.DAL;
using DOT.NET.Models;
using DOT.NET.OwnHelpers;
using DOT.NET.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
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
      
        public async Task<ActionResult> Zaplac()
        {
            if (Request.IsAuthenticated) 
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                var zamowienie = new Zamowienie
                {
                    Imie = user.DaneUzytkownika.Imie,
                    Nazwisko = user.DaneUzytkownika.Nazwisko,
                    Adres = user.DaneUzytkownika.Adres,
                    Miasto = user.DaneUzytkownika.Miasto,
                    KodPocztowy = user.DaneUzytkownika.KodPocztowy,
                    Email = user.DaneUzytkownika.Email,
                    Telefon = user.DaneUzytkownika.Telefon
                };
                return View(zamowienie);
            }
            else
            {
                return RedirectToAction("Login","Account", new { returnurl = Url.Action("Zaplac", "Koszyk") });
            }
        }


        [HttpPost]
        public async Task<ActionResult> Zaplac(Zamowienie zamowienieSzczegoly)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();

                var newOrder = koszykMenager.UtworzZamowienie(zamowienieSzczegoly, userId);

                koszykMenager.PustyKoszyk();

                 return RedirectToAction("PotwierdzenieZamowienia");
            }
            else
                return View(zamowienieSzczegoly);
        }
        public ActionResult PotwierdzenieZamowienia()
        {
            var name = User.Identity.Name;
            return View();
        }
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
    }

}