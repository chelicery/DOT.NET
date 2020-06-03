using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DOT.NET.Models
{
    public class Kategoria
    {
        public int KategoriaId { get; set; }
        [Required(ErrorMessage = "Wprowadz nazwę kategorii")]
        [StringLength(100)]
        public string NazwaKategorii { get; set; }
        [Required(ErrorMessage = "Wprowadz opis kategorii")]
        public string OpisKategorii { get; set; }
        public string NazwaPlikuIkony { get; set; }
        public virtual ICollection<Przedmiot> Przedmioty
        { get; set; }
    }
}