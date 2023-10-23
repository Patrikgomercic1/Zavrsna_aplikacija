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
            if (Pomocno.Dev == true)
            {
                TestniPodaci();
            }

        }

        public void PrikaziIzbornik()
        {
            Console.WriteLine();
            Console.WriteLine(" \t-----------------------------------");
            Console.WriteLine(" \t---| Izbornik za rad s Kupcima |---");
            Console.WriteLine(" \t-----------------------------------");
            Console.WriteLine();

            Console.WriteLine("\t1. Pregled postojećih kupaca");
            Console.WriteLine("\t2. Unos novog kupca");
            Console.WriteLine("\t3. Promjena postojećeg kupca");
            Console.WriteLine("\t4. Brisanje kupca");
            Console.WriteLine("\t5. Povratak na glavni izbornik\n");

            switch(Pomocno.UcitajBrojRaspon("\n\tOdaberite stavku izbornika: ", "\n\tOdabir mora biti između 1 i 5!", 1, 5))
            {
                case 1:
                    PrikaziKupce();
                    DetaljiKorisnika();
                    PrikaziIzbornik();
                    break;
                case 2:
                    UnosNovogKupca();
                    PrikaziIzbornik();
                    break;
                case 3:
                    PromjenaKupca();
                    PrikaziIzbornik();
                    break;
                case 4:
                    BrisanjeKupca();
                    PrikaziIzbornik();
                    break;
                case 5:
                    Console.WriteLine("\n\t~~Vraćanje na prijašnji izbornik~~\n");
                    break;
            }
        }

        private void BrisanjeKupca()
        {
            if(Kupci.Count == 0)
            {
                Console.WriteLine("\tGreška! Nema kupaca za brisanje!");
            }
            else
            {
                PrikaziKupce();
                int index = Pomocno.UcitajBrojRaspon("\tOdaberite redni broj kupca za brisanje, za odustajanje unesite 0: ", "Greška! Morate unijeti broj koji je trenutno u korištenju!", 0, Kupci.Count());
                if (index == 0)
                {
                    Console.WriteLine("\tOdustali ste od promjene.");
                }
                else
                {
                    Kupci.RemoveAt(index - 1);
                    Console.WriteLine("\tKupac je uspješno obrisan.");
                }
            }            
        }

        private void PromjenaKupca()
        {
            if (Kupci.Count == 0)
            {
                Console.WriteLine("\tGreška! Nema kupaca za promjenu!");
            }
            else
            {
                PrikaziKupce();
                int index = Pomocno.UcitajBrojRaspon("\tUnesite redni broj kupca za promjenu, za odustajanje unesite 0: ", "\tGreška! Morate unijeti broj koji je trenutno u korištenju!", 0, Kupci.Count());
                if (index == 0)
                {
                    Console.WriteLine("\tOdustali ste od promjene.");
                }
                else
                {
                    var k = Kupci[index - 1];
                    k.Sifra = Pomocno.UcitajCijeliBroj("\tUnesite ŠIFRU kupca(" + k.Sifra + "): ", "\tGreška! Unos mora biti pozitivan cijeli broj");
                    k.KorisnickoIme = Pomocno.UcitajString("\tUnesite KORISNIČKO IME kupca(" + k.KorisnickoIme + "): ", "\tGreška! Unos mora biti niz slova/brojeva");
                    k.Ime = Pomocno.UcitajString("\tUnesite IME korisnika(" + k.Ime + "): ", "\tGreška! Unos mora biti niz slova");
                    k.Prezime = Pomocno.UcitajString("\tUnesite PREZIME korisnika(" + k.Prezime + "): ", "\tGreška! Unos mora biti niz slova");
                    k.Lozinka = Pomocno.UcitajString("\tUnesite LOZINKU korisnika(" + k.Lozinka + "): ", "\tGreška! Unos mora biti niz slova/brojeva");
                    k.Telefon = Pomocno.UcitajCijeliBroj("\tUnesite broj TELEFONA korisnika(" + k.Telefon + "): ", "\tGreška! Unos mora biti pozitivan cijeli broj");
                    k.Adresa = Pomocno.UcitajString("\tUnesite ADRESU korisnika(" + k.Adresa + "): ", "\tGreška! Unos mora biti niz slova/brojeva");
                }
            }
        }

        private void UnosNovogKupca()
        {
            var k = new Kupac();
            k.KorisnickoIme = Pomocno.UcitajString("\tUnesite KORISNIČKO IME kupca(" + k.KorisnickoIme + "): ", "\tGreška! Unos mora biti niz slova/brojeva");
            k.Ime = Pomocno.UcitajString("\tUnesite IME korisnika(" + k.Ime + "): ", "\tGreška! Unos mora biti niz slova");
            k.Prezime = Pomocno.UcitajString("\tUnesite PREZIME korisnika(" + k.Prezime + "): ", "\tGreška! Unos mora biti niz slova");
            k.Lozinka = Pomocno.UcitajString("\tUnesite LOZINKU korisnika(" + k.Lozinka + "): ", "\tGreška! Unos mora biti niz slova/brojeva");
            k.Telefon = Pomocno.UcitajCijeliBroj("\tUnesite broj TELEFONA korisnika(" + k.Telefon + "): ", "\tGreška! Unos mora biti pozitivan cijeli broj");
            k.Adresa = Pomocno.UcitajString("\tUnesite ADRESU korisnika(" + k.Adresa + "): ", "\tGreška! Unos mora biti niz slova/brojeva");
            Kupci.Add(k);
        }

        public void PrikaziKupce()
        {
            if(Kupci.Count == 0) 
            {
                Console.WriteLine("\nGreška! Nema kupaca za prikaz");
            }
            else
            {

                Console.WriteLine();
                Console.WriteLine("  \t-----------------------------------------------------------------");
                Console.WriteLine("\t|                   PREGLED POSTOJEĆIH KUPACA                   |");
                Console.WriteLine("  \t-----------------------------------------------------------------");
                Console.WriteLine();
                int broj = 1;

                foreach (Kupac k in Kupci)
                {
                    Console.WriteLine("\t {0}. {1}", broj++, k);
                }
                Console.WriteLine("\n \t|---------------------------------------------------------------|");
            }

        }

        private void DetaljiKorisnika()
        {
            Console.WriteLine();
            int index = Pomocno.UcitajBrojRaspon("\tZa detalje odaberite broj korisnika (0 za povratak na izbornik): ", "\tGreška! Odabir mora biti jedan od ponuđenih brojeva", 0, Kupci.Count());
            if (index != 0)
            {
                var p = Kupci[index - 1];

                Console.WriteLine();
                Console.WriteLine("\t|---------------------------------------------------------------|");
                Console.WriteLine("\t|                          DETALJI KUPCA                        |");
                Console.WriteLine("\t|---------------------------------------------------------------|");
                Console.WriteLine();
                Console.WriteLine("\tKorisničko ime: {0}", p.KorisnickoIme);
                Console.WriteLine("\tIme korisnika: {0}", p.Ime);
                Console.WriteLine("\tPrezime korisnika: {0}", p.Prezime);
                Console.WriteLine("\tPrezime korisnika: {0}", p.Lozinka);
                Console.WriteLine("\tPrezime korisnika: {0}", p.Telefon);
                Console.WriteLine("\tAdresa korisnika: {0}", p.Adresa);
                Console.WriteLine();
                Console.WriteLine("\t|---------------------------------------------------------------|\n");
            }
        }

        private void TestniPodaci()
        {
            Kupci.Add(new Kupac() 
            { 
                KorisnickoIme = "MarkoMark", 
                Ime = "Marko",
                Prezime = "Marković",
                Lozinka = "MarkoMark123",
                Telefon = 255121335,
                Adresa = "primjeradresa1"


            });

            Kupci.Add(new Kupac()
            {
                KorisnickoIme = "MarkoMark2",
                Ime = "Marko",
                Prezime = "Marković",
                Lozinka = "MarkoMark1234",
                Telefon = 255121335,
                Adresa = "primjeradresa2"


            });

            Kupci.Add(new Kupac()
            {
                KorisnickoIme = "MarkoMark3",
                Ime = "Marko",
                Prezime = "Marković",
                Lozinka = "MarkoMark12345",
                Telefon = 255121335,
                Adresa = "primjeradresa3"


            });


        }
    }
}
