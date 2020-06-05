using DOT.NET.DAL;
using DOT.NET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace DOT.NET.OwnHelpers
{
    public class KoszykMenager
    {
        private PrzedmiotyContext db;
        private IsessionMenager session;
        public KoszykMenager(IsessionMenager session, PrzedmiotyContext db)
        {
            this.session = session;
            this.db = db;
        }

        public List<PozycjaKoszyka> PobierzKoszyk()
        {
            List<PozycjaKoszyka> koszyk;

            if (session.Get<List<PozycjaKoszyka>>(Consts.KoszykSessionKlucz)==null)
            {
                koszyk = new List<PozycjaKoszyka>();
            }
            else
            {
                koszyk = session.Get<List<PozycjaKoszyka>>(Consts.KoszykSessionKlucz) as List<PozycjaKoszyka>;
            }

            return koszyk;
        }

        public void DodajDoKoszyka(int PrzedmiotId)
        {
            var koszyk = PobierzKoszyk();
            var pozycjaKoszyka = koszyk.Find(p => p.Przedmiot.PrzedmiotId == PrzedmiotId);

            if(pozycjaKoszyka != null)
            {
                pozycjaKoszyka.Ilosc++;
            }
            else
            {
                var PozycjaDoDodania = db.Przedmioty.Where(p => p.PrzedmiotId == PrzedmiotId).SingleOrDefault();

                if(PozycjaDoDodania != null)
                {
                    var nowaPozycjaKoszyka = new PozycjaKoszyka()
                    {
                        Przedmiot = PozycjaDoDodania,
                        Ilosc = 1,
                        Wartosc = PozycjaDoDodania.Cena
                    };
                    koszyk.Add(nowaPozycjaKoszyka);
                }
            }
            session.Set(Consts.KoszykSessionKlucz, koszyk); 


        }

        public int UsunZKoszyka(int przedmiotId) {
            var koszyk = PobierzKoszyk();
            var pozycjaKoszyka = koszyk.Find(p=>p.Przedmiot.PrzedmiotId == przedmiotId);

            if(pozycjaKoszyka != null)
            {
                if(pozycjaKoszyka.Ilosc > 1)
                {
                    pozycjaKoszyka.Ilosc--;
                    return pozycjaKoszyka.Ilosc;
                } 
                else
                {
                    koszyk.Remove(pozycjaKoszyka);
                }
            }
            return 0;

        }

        public decimal PobierzWartoscKoszyka()
        {
            var koszyk = PobierzKoszyk();
            return koszyk.Sum(p=>(p.Ilosc*p.Przedmiot.Cena));
        }

        public int PobierzIloscPozycjiKoszyka()
        {
            var koszyk = PobierzKoszyk();
            int ilosc = koszyk.Sum(p => p.Ilosc);
            return ilosc;
        }

        public Zamowienie UtworzZamowienie(Zamowienie noweZamowienie, string userId){
            var koszyk = PobierzKoszyk();
            noweZamowienie.DataDodania = DateTime.Now;
            //noweZamowienie.userId = userId;
            db.Zamowienia.Add(noweZamowienie);
            if(noweZamowienie.PozycjeZamowienia == null)            
                noweZamowienie.PozycjeZamowienia = new List<PozycjaZamowienia>();

            decimal koszykWartosc = 0;

            foreach (var koszykElement in koszyk){
                var nowaPozycjaZamowienia = new PozycjaZamowienia()
                {
                    PrzedmiotId = koszykElement.Przedmiot.PrzedmiotId,
                    Ilosc = koszykElement.Ilosc,
                    CenaZakupu = koszykElement.Przedmiot.Cena
                };

                koszykWartosc += (koszykElement.Ilosc * koszykElement.Przedmiot.Cena);
                noweZamowienie.PozycjeZamowienia.Add(nowaPozycjaZamowienia);
            }
            noweZamowienie.WartoscZamowienia = koszykWartosc;
            db.SaveChanges();
            return noweZamowienie;
        }

        public void PustyKoszyk()
        {
            session.Set<List<PozycjaKoszyka>>(Consts.KoszykSessionKlucz, null);
        }

    }

}