using DOT.NET.App_Start;
using DOT.NET.DAL;
using DOT.NET.Models;
using DOT.NET.OwnHelpers;
using DOT.NET.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DOT.NET.Controllers
{
        [Authorize]
        public class ManageController : Controller
        {
           
            private PrzedmiotyContext db = new PrzedmiotyContext();
        
            public enum ManageMessageId
            {
                ChangePasswordSuccess,
                Error
            }


        //public ManageController(ApplicationUserManager userManager)
        //{
        //    UserManager = userManager;
        //}

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

            private IAuthenticationManager AuthenticationManager
            {
                get
                {
                    return HttpContext.GetOwinContext().Authentication;
                }
            }

            // GET: Manage
            public async Task<ActionResult> Index(ManageMessageId? message)
            {
                var name = User.Identity.Name;

                if (TempData["ViewData"] != null)
                {
                    ViewData = (ViewDataDictionary)TempData["ViewData"];
                }

                if (User.IsInRole("Admin"))
                    ViewBag.UserIsAdmin = true;
                else
                    ViewBag.UserIsAdmin = false;

                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user == null)
                {
                    return View("Error");
                }

                var model = new ManageCredentialsViewModel
                {
                    Message = message,
                    DaneUzytkownika = user.DaneUzytkownika
                };

                return View(model);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<ActionResult> ChangeProfile([Bind(Prefix = "DaneUzytkownika")]DaneUzytkownika daneUzytkownika)
            {
                if (ModelState.IsValid)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    user.DaneUzytkownika = daneUzytkownika;
                    var result = await UserManager.UpdateAsync(user);

                    AddErrors(result);
                }

                if (!ModelState.IsValid)
                {
                    TempData["ViewData"] = ViewData;
                    return RedirectToAction("Index");
                }

                return RedirectToAction("Index");
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<ActionResult> ChangePassword([Bind(Prefix = "ChangePasswordViewModel")]ChangePasswordViewModel model)
            {
                if (!ModelState.IsValid)
                {
                    TempData["ViewData"] = ViewData;
                    return RedirectToAction("Index");
                }

                var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                    if (user != null)
                    {
                        await SignInAsync(user, isPersistent: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
                }
                AddErrors(result);

                if (!ModelState.IsValid)
                {
                    TempData["ViewData"] = ViewData;
                    return RedirectToAction("Index");
                }

                var message = ManageMessageId.ChangePasswordSuccess;
                return RedirectToAction("Index", new { Message = message });
            }


            private void AddErrors(IdentityResult result)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("password-error", error);
                }
            }

            private async Task SignInAsync(ApplicationUser user, bool isPersistent)
            {
                AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);
                AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
            }

        public ActionResult ListaZamowien()
        {
            var name = User.Identity.Name;

            bool isAdmin = User.IsInRole("Admin");
            ViewBag.UserIsAdmin = isAdmin;

            IEnumerable<Zamowienie> zamowieniaUzytkownika;

            // Dla administratora zwracamy wszystkie zamowienia
            if (isAdmin)
            {
                zamowieniaUzytkownika = db.Zamowienia.Include("PozycjeZamowienia").OrderByDescending(o => o.DataDodania).ToArray();
            }
            else
            {
                var userId = User.Identity.GetUserId();
                zamowieniaUzytkownika = db.Zamowienia.Where(o => o.UserId == userId).Include("PozycjeZamowienia").OrderByDescending(o => o.DataDodania).ToArray();
            }

            return View(zamowieniaUzytkownika);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public StanZamowienia ZmianaStanuZamowienia(Zamowienie zamowienie)
        {
            Zamowienie zamowienieDoModyfikacji = db.Zamowienia.Find(zamowienie.ZamowienieId);
            zamowienieDoModyfikacji.StanZamowienia = zamowienie.StanZamowienia;
            db.SaveChanges();



            return zamowienie.StanZamowienia;
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DodajPrzedmiot(int? przedmiotId, bool? potwierdzenie)
        {
            Przedmiot przedmiot;
            if (przedmiotId.HasValue)
            {
                ViewBag.EditMode = true;
                przedmiot = db.Przedmioty.Find(przedmiotId);
            }
            else
            {
                ViewBag.EditMode = false;
                przedmiot = new Przedmiot(); 
            }

            var result = new EditPrzedmiotViewModel();
            result.Kategorie = db.Kategorie.ToList();
            result.Przedmiot = przedmiot;
            result.Potwierdzenie = potwierdzenie;

            return View(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult DodajPrzedmiot(EditPrzedmiotViewModel model, HttpPostedFileBase file)
        {
            if (model.Przedmiot.PrzedmiotId > 0)
            {
           
                db.Entry(model.Przedmiot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DodajPrzedmiot", new { potwierdzenie = true });
            }
            else
            {
                if (file != null && file.ContentLength > 0)
                {
                    if (ModelState.IsValid)
                    {
                        // Generowanie pliku
                        var fileExt = Path.GetExtension(file.FileName);
                        var filename = Guid.NewGuid() + fileExt;

                        var path = Path.Combine(Server.MapPath(AppConfig.ObrazkiFolderWzgledny), filename);
                        file.SaveAs(path);

                        model.Przedmiot.NazwaPlikuObrazka = filename;
                        model.Przedmiot.DataDodania = DateTime.Now;

                        db.Entry(model.Przedmiot).State = EntityState.Added;
                        db.SaveChanges();

                        return RedirectToAction("DodajPrzedmiot", new { potwierdzenie = true });
                    }
                    else
                    {
                        var kategorie = db.Kategorie.ToList();
                        model.Kategorie = kategorie;
                        return View(model);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Nie wskazano pliku");
                    var kategorie = db.Kategorie.ToList();
                    model.Kategorie = kategorie;
                    return View(model);
                }
            }

        }

        [Authorize(Roles = "Admin")]
        public ActionResult UkryjPrzedmiot(int przedmiotId)
        {
            var przedmiot = db.Przedmioty.Find(przedmiotId);
            przedmiot.Ukryty = true;
            db.SaveChanges();

            return RedirectToAction("DodajPrzedmiot", new { potwierdzenie = true });
        }

        [Authorize(Roles = "Admin")]
        public ActionResult PokazPrzedmiot(int przedmiotId)
        {
            var przedmiot = db.Przedmioty.Find(przedmiotId);
            przedmiot.Ukryty = false;
            db.SaveChanges();

            return RedirectToAction("DodajPrzedmiot", new { potwierdzenie = true });
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UsunPrzedmiotZBazy(int przedmiotId)
        {
            var przedmiot = db.Przedmioty.First(p => p.PrzedmiotId == przedmiotId);
            if (przedmiot != null)
            {
                db.Przedmioty.Remove(przedmiot);
                db.SaveChanges();
                return RedirectToAction("DodajPrzedmiot", new { potwierdzenie = true });
            }
            else
                return RedirectToAction("DodajPrzedmiot", new { potwierdzenie = false });


        }



    }
}