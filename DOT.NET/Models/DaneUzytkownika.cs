using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DOT.NET.Models
{
    public class DaneUzytkownika
    {
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string IAdres { get; set; }
        public string Miasto { get; set; }
        [RegularExpression(@"(\+\d{2})*[\d\s-]+]", ErrorMessage = "Błędny format numeru telefonu.")]
        public string Telefon { get; set; }
        [EmailAddress(ErrorMessage = "Błędny format adresu email")]
        public string Email { get; set; }

    }
}