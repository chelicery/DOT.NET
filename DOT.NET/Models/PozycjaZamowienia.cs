namespace DOT.NET.Models
{
    public class PozycjaZamowienia
    {
        public int PozycjaZamowieniaId { get; set; }
        public int ZamowienieId { get; set; }
        public int PrzedmiotId { get; set; }
        public int Ilosc { get; set; }
        public decimal CenaZakupu { get; set; }
    
        public virtual Przedmiot przedmiot { get; set; }
        public virtual Zamowienie zamowienie { get; set; }

    }
}