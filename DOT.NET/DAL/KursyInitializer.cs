using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DOT.NET.Models;

namespace DOT.NET.DAL
{
    public class KursyInitializer : DropCreateDatabaseAlways<KursyContext>
    {
        protected override void Seed(KursyContext context)
        {
            SeedKursyData(context);
            base.Seed(context);
        }

        private void SeedKursyData(KursyContext context)
        {
            var kategorie = new List<Kategoria>
            {
                new Kategoria() { KategoriaId = 1, NazwaKategorii="asp", NazwaPlikuIkony="asp.png", OpisKategorii="opis asp dot net"},
                new Kategoria() { KategoriaId = 2, NazwaKategorii = "java", NazwaPlikuIkony = "java.png", OpisKategorii = "opis java" },
                new Kategoria() { KategoriaId = 3, NazwaKategorii = "php", NazwaPlikuIkony = "php.png", OpisKategorii = "opis php" },
                new Kategoria() { KategoriaId = 4, NazwaKategorii = "html", NazwaPlikuIkony = "html.png", OpisKategorii = "opis html" },
                new Kategoria() { KategoriaId = 5, NazwaKategorii = "css", NazwaPlikuIkony = "css.png", OpisKategorii = "opis css" },
                new Kategoria() { KategoriaId = 6, NazwaKategorii = "xml", NazwaPlikuIkony = "xml.png", OpisKategorii = "opis xml" }

             };
            kategorie.ForEach(k => context.Kategorie.Add(k));
            context.SaveChanges();

            var kursy = new List<Kurs>
            {
                new Kurs() {AutorKursu = "Pszemek", TytulKursu= "asp dot net mvc", KategoriaId=1, CenaKursu=5, Bestseller=true, NazwaPlikuObrazka="asp.png" },
                new Kurs() {AutorKursu = "Janusz", TytulKursu= "asp dot net mvc2", KategoriaId=1, CenaKursu=12, Bestseller=true, NazwaPlikuObrazka="asp.png" },
                new Kurs() {AutorKursu = "Kamila", TytulKursu= "asp dot net mvc3", KategoriaId=1, CenaKursu=21, Bestseller=true, NazwaPlikuObrazka="asp.png" },
                new Kurs() { AutorKursu = "Karolina", TytulKursu = "asp dot net mvc4", KategoriaId = 1, CenaKursu = 30, Bestseller = true, NazwaPlikuObrazka = "asp.png" }
            };
            kursy.ForEach(k => context.Kursy.Add(k));
            context.SaveChanges();



        }
    }
}