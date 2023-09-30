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
            Console.WriteLine();
            Console.WriteLine(" ---------------------------------------");
            Console.WriteLine(" ---| Izbornik za rad s kupcima |---");
            Console.WriteLine(" ---------------------------------------");

            Console.WriteLine(" 1. Pregled postojećih kupaca");
            Console.WriteLine(" 2. Unos novog kupca");
            Console.WriteLine(" 3. Promjena postojećeg kupca");
            Console.WriteLine(" 4. Brisanje kupca");
            Console.WriteLine(" 5. Povratak na glavni izbornik");

            switch(Pomocno.UcitajBrojRaspon("\tOdaberite stavku izbornika kupca: ", "\tOdabri mora biti od 1 do 5", 1, 5))
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
                    PromjenaKupca();
                    PrikaziIzbornik();
                    break;
                case 4:
                    BrisanjeKupca();
                    PrikaziIzbornik();
                    break;
                case 5:
                    Console.WriteLine("Gotov rad s kupcima");
                    break;
            }
        }

        private void BrisanjeKupca()
        {
            if(Kupci.Count == 0)
            {
                Console.WriteLine("\t Nema kupaca za brisanje");
            }
            else
            {
                PrikaziKupce();
                int index = Pomocno.UcitajBrojRaspon("\tOdaberite redni broj kupca za brisanje, za ODUSTAJANJE unesite 0: ", "Morate unijeti broj koji je trenutno u korištenju", 0, Kupci.Count());
                if (index == 0)
                {
                    Console.WriteLine("\tOdustali ste od promjene.");
                }
                else
                {
                    Kupci.RemoveAt(index - 1);
                }
            }            
        }

        private void PromjenaKupca()
        {
            if (Kupci.Count == 0)
            {
                Console.WriteLine("\tNema kupaca za promjenu");
            }
            else
            {
                PrikaziKupce();
                int index = Pomocno.UcitajBrojRaspon("\tUnesite redni broj kupca za promjenu, za odustajanje unesite 0: ", "\tMorate unijeti broj koji je trenutno u korištenju", 0, Kupci.Count());
                if (index == 0)
                {
                    Console.WriteLine("\tOdustali ste od promjene");
                }
                else
                {
                    var k = Kupci[index - 1];
                    k.Sifra = Pomocno.UcitajCijeliBroj("\tUnesite šifru kupca(" + k.Sifra + "): ", "\tUnos mora biti pozitivan cijeli broj");
                    k.KorisnickoIme = Pomocno.UcitajString("\tUnesite korisničko ime kupca(" + k.KorisnickoIme + "): ", "\tUnos mora biti niz slova");
                    k.Ime = Pomocno.UcitajString("\tUnesite ime korisnika(" + k.Ime + "): ", "\tUnos je obavezan");
                    k.Prezime = Pomocno.UcitajString("\tUnesite prezime korisnika(" + k.Prezime + "): ", "\tUnos je obavezan");
                    k.Lozinka = Pomocno.UcitajString("\tUnesite lozinku korisnika(" + k.Lozinka + "): ", "\tUnos je obavezan");
                    k.Telefon = Pomocno.UcitajCijeliBroj("\tUnesite broj telefona korisnika(" + k.Telefon + "): ", "\tUnos je obavezan");
                    k.Adresa = Pomocno.UcitajString("\tUnesite adresu korisnika(" + k.Adresa + "): ", "\tUnos je obavezan");
                }
            }
        }

        private void UnosNovogKupca()
        {
            var k = new Kupac();
            k.KorisnickoIme = Pomocno.UcitajString("\tUnesite korisničko ime kupca: ", "\tUnos mora biti niz slova");
            k.Ime = Pomocno.UcitajString("\tUnesite ime korisnika: ", "\tUnos je obavezan");
            k.Prezime = Pomocno.UcitajString("\tUnesite prezime korisnika: ", "\tUnos je obavezan");
            k.Lozinka = Pomocno.UcitajString("\tUnesite lozinku korisnika: ", "\tUnos je obavezan");
            k.Telefon = Pomocno.UcitajCijeliBroj("\tUnesite broj telefona korisnika: ", "\tUnos je obavezan");
            k.Adresa = Pomocno.UcitajString("\tUnesite adresu korisnika: ", "\tUnos je obavezan");
            Kupci.Add(k);
        }

        private void PrikaziKupce()
        {
            if(Kupci.Count == 0) 
            {
                Console.WriteLine("\n Nema kupaca za prikaz");
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

        private void TestniPodaci()
        {
            Kupci.Add(new Kupac() 
            { 
                KorisnickoIme = "MarkoMark", 
                Ime = "Marko",
                Prezime = "Marković",
                Lozinka = "kldagčkas33sld",
                Telefon = 255121335,
                Adresa = "primjeradresa1"


            });

            Kupci.Add(new Kupac()
            {
                KorisnickoIme = "MarkoMark2",
                Ime = "Marko",
                Prezime = "Marković",
                Lozinka = "kldagčkas33sld",
                Telefon = 255121335,
                Adresa = "primjeradresa2"


            });

            Kupci.Add(new Kupac()
            {
                KorisnickoIme = "MarkoMark3",
                Ime = "Marko",
                Prezime = "Marković",
                Lozinka = "kldagčkas33sld",
                Telefon = 255121335,
                Adresa = "primjeradresa3"


            });


        }
    }
}
