using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LjetnaAplikacija
{
    internal class ObradaKupac
    {
        public List<Kupac> Kupci { get;  }

        public ObradaKupac() 
        {
            Kupci = new List<Kupac>();
            TestniPodaci();

        }

        public void PrikaziIzbornik()
        {
            Console.WriteLine("Izbornik za rad s kupcima");
            Console.WriteLine("1. Pregled postojećih kupaca");
            Console.WriteLine("2. Unos novog kupca");
            Console.WriteLine("3. Promjena postojećeg kupca");
            Console.WriteLine("4. Brisanje kupca");
            Console.WriteLine("5. Povratak na glavni izbornik");

            switch(Pomocno.UcitajBrojRaspon("Odaberite stavku izbornika kupca: ", "Odabri mora biti od 1 do 5", 1, 5))
            {
                case 1:
                    PrikaziKupce();
                    PrikaziIzbornik();
                    break;
                case 2:
                    UnosNovogKupca();
                    PrikaziIzbornik();
                    break;
                case 3:
                    Console.WriteLine("PromjenaKupca");
                    PrikaziIzbornik();
                    break;
                case 4:
                    Console.WriteLine("BrisanjeKupca");
                    PrikaziIzbornik();
                    break;
                case 5:
                    Console.WriteLine("Gotov rad s kupcima");
                    break;
            }
        }

        private void UnosNovogKupca()
        {
            var k = new Kupac();
            k.Sifra = Pomocno.UcitajCijeliBroj("Unesite šifru kupca: ", "Unos mora biti pozitivan cijeli broj");
            k.KorisnickoIme = Pomocno.UcitajString("Unesite korisničko ime kupca: ", "Unos mora biti niz slova");
            k.Ime = Pomocno.UcitajString("Unesite ime korisnika: ", "Unos je obavezan");
            k.Prezime = Pomocno.UcitajString("Unesite prezime korisnika: ", "Unos je obavezan");
            k.Lozinka = Pomocno.UcitajString("Unesite lozinku korisnika: ", "Unos je obavezan");
            k.Telefon = Pomocno.UcitajString("Unesite broj telefona korisnika: ", "Unos je obavezan");
            k.Adresa = Pomocno.UcitajString("Unesite adresu korisnika: ", "Unos je obavezan");
            Kupci.Add(k);
        }

        private void PrikaziKupce()
        {
            foreach(Kupac kupac in Kupci)
            {
                Console.WriteLine(kupac.KorisnickoIme);
            }
        }

        private void TestniPodaci()
        {
            Kupci.Add(new Kupac() { KorisnickoIme = "MarkoMark" });
        }
    }
}
