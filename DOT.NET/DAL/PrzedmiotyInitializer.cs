using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DOT.NET.Models;
using DOT.NET.Migrations;
using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DOT.NET.DAL
{
    public class PrzedmiotyInitializer : MigrateDatabaseToLatestVersion<PrzedmiotyContext, Configuration>
    {
      
        public static void SeedPrzedmiotyData(PrzedmiotyContext context)
        {
            var kategorie = new List<Kategoria>
            {
                new Kategoria() { KategoriaId = 1, NazwaKategorii="Ogród", NazwaPlikuIkony="ogrod.png", OpisKategorii="kategoria ogród"},
                new Kategoria() { KategoriaId = 2, NazwaKategorii = "Motoryzacja", NazwaPlikuIkony = "motoryzacja.png", OpisKategorii = "kategoria motoryzacja brum brum" },
                new Kategoria() { KategoriaId = 3, NazwaKategorii = "Kultura", NazwaPlikuIkony = "kultura.png", OpisKategorii = "kategoria kultura" },
                new Kategoria() { KategoriaId = 4, NazwaKategorii = "Sztuka", NazwaPlikuIkony = "sztuka.png", OpisKategorii = "kategoria sztuka" },
                new Kategoria() { KategoriaId = 5, NazwaKategorii = "Dom", NazwaPlikuIkony = "dom.png", OpisKategorii = "kategoria dom" },
                new Kategoria() { KategoriaId = 6, NazwaKategorii = "Hobby", NazwaPlikuIkony = "hobby.png", OpisKategorii = "kategoria hobby" }

             };
            kategorie.ForEach(k => context.Kategorie.AddOrUpdate(k));
            context.SaveChanges();

            var przedmioty = new List<Przedmiot>
            {
                new Przedmiot() {  Producent = "Nokian", Nazwa= "Opona samochodowa", KategoriaId=2, Cena=500, Bestseller=true, NazwaPlikuObrazka="opona.png" },
                new Przedmiot() { Producent = "Von Gogh", Nazwa= "Obraz na płótnie malowany", KategoriaId=4, Cena=12000, Bestseller=true, NazwaPlikuObrazka="obraz.png" },
                new Przedmiot() {Producent = "Hp", Nazwa= "Drukarka", KategoriaId=5, Cena=21, Bestseller=true, NazwaPlikuObrazka="drukarka.png" },
                new Przedmiot() { Producent = "Adidas", Nazwa = "Piłka okrągła", KategoriaId = 6, Cena = 120, Bestseller = false, NazwaPlikuObrazka = "pilka.png" },
                new Przedmiot() { Producent = "Gang Plebanii", Nazwa = "Płyta muzyczna", KategoriaId = 3, Cena = 30, Bestseller = false, NazwaPlikuObrazka = "plyta.png" },
                new Przedmiot() { Producent = "Biotanic", Nazwa = "Taczka bez koła", KategoriaId = 1, Cena = 300, Bestseller = true, NazwaPlikuObrazka = "taczka.png" },

                new Przedmiot() {  Producent = "Pirelli", Nazwa= "Opona samochodowa", KategoriaId=2, Cena=600, Bestseller=false, NazwaPlikuObrazka="opona.png" },
                new Przedmiot() { Producent = "Jan Matejko", Nazwa= "Inny Obraz na płótnie malowany", KategoriaId=4, Cena=2000, Bestseller=false, NazwaPlikuObrazka="obraz.png" },
                new Przedmiot() {Producent = "Oki", Nazwa= "Drukarka", KategoriaId=5, Cena=21, Bestseller=false, NazwaPlikuObrazka="drukarka.png" },
                new Przedmiot() { Producent = "Nike", Nazwa = "Piłka okrągła", KategoriaId = 6, Cena = 150, Bestseller = true, NazwaPlikuObrazka = "pilka.png" },
                new Przedmiot() { Producent = "Geneza", Nazwa = "Płyta muzyczna", KategoriaId = 3, Cena = 35, Bestseller = true, NazwaPlikuObrazka = "plyta.png" },
                new Przedmiot() { Producent = "Castorama", Nazwa = "Taczka bez koła", KategoriaId = 1, Cena = 200, Bestseller = true, NazwaPlikuObrazka = "taczka.png" }

            };
            przedmioty.ForEach(k => context.Przedmioty.AddOrUpdate(k));
            context.SaveChanges();



        }

        public static void SeedUzytkownicy(PrzedmiotyContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            const string name = "admin@admin.pl";
            const string password = "Admin123?";
            const string roleName = "Admin";

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser { UserName = name, Email = name, DaneUzytkownika = new DaneUzytkownika() };
                var result = userManager.Create(user, password);
            }

            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
        }
    }
}