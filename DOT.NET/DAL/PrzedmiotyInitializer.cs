using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DOT.NET.Models;
using DOT.NET.Migrations;
using System.Data.Entity.Migrations;

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
                new Przedmiot() {  Producent = "Huong San", Nazwa= "Opona samochodowa", KategoriaId=2, Cena=500, Bestseller=true, NazwaPlikuObrazka="opona.png" },
                new Przedmiot() { Producent = "Biedronka", Nazwa= "Obraz na płótnie malowany", KategoriaId=4, Cena=12000, Bestseller=true, NazwaPlikuObrazka="obraz.png" },
                new Przedmiot() {Producent = "Hp", Nazwa= "Drukarka", KategoriaId=5, Cena=21, Bestseller=true, NazwaPlikuObrazka="drukarka.png" },
                new Przedmiot() { Producent = "Adidas", Nazwa = "Piłka okrągła", KategoriaId = 6, Cena = 120, Bestseller = false, NazwaPlikuObrazka = "pilka.png" },
                new Przedmiot() { Producent = "Mymusic", Nazwa = "Płyta muzyczna", KategoriaId = 3, Cena = 30, Bestseller = false, NazwaPlikuObrazka = "plyta.png" },
                new Przedmiot() { Producent = "Biotanic", Nazwa = "taczka bez koła", KategoriaId = 1, Cena = 300, Bestseller = true, NazwaPlikuObrazka = "taczka.png" }

            };
            przedmioty.ForEach(k => context.Przedmioty.AddOrUpdate(k));
            context.SaveChanges();



        }
    }
}