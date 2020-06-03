namespace DOT.NET.Models
{
    internal class PozycjaZmowienia
    {
        public int PozycjaZamowieniaId { get; set; }
        public int ZamowienieId { get; set; }
        public int KursId { get; set; }
        public int Ilosc { get; set; }
        public decimal CenaZakupu { get; set; }
    
        public virtual Kurs kurs { get; set; }
        public virtual Zamowienie zamowienie { get; set; }

    }
}