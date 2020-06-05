using DOT.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOT.NET.ViewModels
{
    public class EditPrzedmiotViewModel
    {
        public Przedmiot Przedmiot { get; set; }
        public IEnumerable<Kategoria> Kategorie { get; set; }
        public bool? Potwierdzenie { get; set; }
    }
}