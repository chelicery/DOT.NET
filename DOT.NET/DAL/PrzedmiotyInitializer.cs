using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DOT.NET.Models;

namespace DOT.NET.DAL
{
    public class PrzedmiotyInitializer : DropCreateDatabaseAlways<PrzedmiotyContext>
    {
        protected override void Seed(PrzedmiotyContext context)
        {
            SeedPrzedmiotyData(context);
            base.Seed(context);
        }

        private void SeedPrzedmiotyData(PrzedmiotyContext context)
        {
            var kategorie = new List<Kategoria>
            {
                new Kategoria() { KategoriaId = 1, NazwaKategorii="Ogród", NazwaPlikuIkony="asp.png", OpisKategorii="opis asp dot net"},
                new Kategoria() { KategoriaId = 2, NazwaKategorii = "Motoryzacja", NazwaPlikuIkony = "java.png", OpisKategorii = "opis java" },
                new Kategoria() { KategoriaId = 3, NazwaKategorii = "Kultura", NazwaPlikuIkony = "php.png", OpisKategorii = "opis php" },
                new Kategoria() { KategoriaId = 4, NazwaKategorii = "Sztuka", NazwaPlikuIkony = "html.png", OpisKategorii = "opis html" },
                new Kategoria() { KategoriaId = 5, NazwaKategorii = "Dom", NazwaPlikuIkony = "css.png", OpisKategorii = "opis css" },
                new Kategoria() { KategoriaId = 6, NazwaKategorii = "Hobby", NazwaPlikuIkony = "xml.png", OpisKategorii = "opis xml" }

             };
            kategorie.ForEach(k => context.Kategorie.Add(k));
            context.SaveChanges();

            var przedmioty = new List<Przedmiot>
            {
                new Przedmiot() {  Producent = "Pszemek", Nazwa= "wykałaczki", KategoriaId=1, Cena=5, Bestseller=true, NazwaPlikuObrazka="asp.png" },
                new Przedmiot() { Producent = "Janusz", Nazwa= "orzeszki", KategoriaId=1, Cena=12, Bestseller=true, NazwaPlikuObrazka="asp.png" },
                new Przedmiot() {Producent = "Kamila", Nazwa= "drukarka", KategoriaId=2, Cena=21, Bestseller=true, NazwaPlikuObrazka="asp.png" },
                new Przedmiot() { Producent = "Karolina", Nazwa = "czekotubka", KategoriaId = 2, Cena = 30, Bestseller = true, NazwaPlikuObrazka = "asp.png" }
            };
            przedmioty.ForEach(k => context.Przedmioty.Add(k));
            context.SaveChanges();



        }
    }
}