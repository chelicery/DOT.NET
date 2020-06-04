using DOT.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOT.NET.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Kategoria> Kategorie { get; set; }
        public IEnumerable<Przedmiot> Nowosci { get; set; }
        public IEnumerable<Przedmiot> Bestsellery { get; set; }
    }
}