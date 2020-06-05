using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DOT.NET.Models
{
    public class Przedmiot
    {
        public int PrzedmiotId { get; set; }
        public int KategoriaId { get; set; }
        [Required(ErrorMessage = "Wprowadz nazwę przedmiotu")]
        [StringLength(100)]
        public string Nazwa { get; set; }
        [Required(ErrorMessage = "Wprowadz nazwę producenta")]
        [StringLength(50)]
        public string Producent { get; set; }
        public DateTime DataDodania { get; set; }
        [StringLength(50)]
        public string NazwaPlikuObrazka { get; set; }
        public string Opis { get; set; }
        public decimal Cena { get; set; }
        public bool Bestseller { get; set; }
        public bool Ukryty { get; set;   }
        public string OpisSkrocony { get; set; }
        public virtual Kategoria Kategoria { get; set; }
    }
}