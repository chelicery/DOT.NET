using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOT.NET.Models
{
    public class PozycjaKoszyka
    {
        public Przedmiot Przedmiot { get; set; }
        public int Ilosc { get; set; }
        public decimal Wartosc { get; set; }
    }
}