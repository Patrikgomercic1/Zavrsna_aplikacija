using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LjetnaAplikacija
{
    internal class ObradaKosarica
    {
        public List<Kosarica> Kosarice { get; }
        private Izbornik Izbornik;

        public ObradaKosarica()
        {
            Kosarice = new List<Kosarica>();

        }

        public ObradaKosarica(Izbornik izbornik) : this()
        {
            this.Izbornik = izbornik;
        }

        public void PrikaziIzbornik()
        {
            Console.WriteLine();
            Console.WriteLine(" \t-----------------------------------");
            Console.WriteLine(" \t--| Izbornik za rad s Košaricom |--");
            Console.WriteLine(" \t-----------------------------------");
            Console.WriteLine();

            Console.WriteLine("\t1. Pregled košarice");
            Console.WriteLine("\t2. Promjena košarice");
            Console.WriteLine("\t3. Dodavanje stavki u košaricu");
            Console.WriteLine("\t4. Brisanje košarice");
            Console.WriteLine("\t5. Povratak na glavni izbornik");

            switch (Pomocno.UcitajBrojRaspon("\n\tOdaberite stavku izbornika: ", "\n\tGreška! Odabir mora biti između 1 i 5!", 1, 5))
            {
                case 1:
                    PregledKosarice();
                    DetaljiKosarice();
                    PrikaziIzbornik();
                    break;
                case 2:
                    PromjeniKosaricu();
                    PrikaziIzbornik();
                    break;
                case 3:
                    DodavanjeKosarice();
                    PrikaziIzbornik();                    
                    break;
                case 4:
                    BrisanjeKosarice();
                    PrikaziIzbornik();
                    break;
                case 5:
                    Console.WriteLine("\n\t~~Vraćanje na prijašnji izbornik~~\n");
                    break;
            }
        }

        private void BrisanjeKosarice()
        {
            if (Kosarice.Count == 0)
            {
                Console.WriteLine("\tGreška! Nema proizvoda za brisanje!");
            }
            else
            {
                PregledKosarice();
                int index = Pomocno.UcitajBrojRaspon("\tOdaberite redni broj košarice za brisanje, za odustajanje unesite 0: ", "\tMorate unijeti broj koji je trenutno u korištenju!", 0, Kosarice.Count());
                if (index == 0)
                {
                    Console.WriteLine("\tOdustali ste od promjene.");
                }
                else
                {
                    Kosarice.RemoveAt(index - 1);
                    Console.WriteLine("\tKošarica je uspješno obrisana.");
                }
            }
        }

        private void PromjeniKosaricu()
        {
            if (Kosarice.Count == 0)
            {
                Console.WriteLine("\tGreška! Nema košarice za promjenu");
            }
            else
            {
                PregledKosarice();
                int index = Pomocno.UcitajBrojRaspon("\tOdaberite redni broj košarice za promjenu, za odustajanje unesite 0: ", "\tGreška! Morate unijeti broj koji je trenutno u korištenju!", 0, Kosarice.Count());
                if (index == 0)
                {
                    Console.WriteLine("\tOdustali ste od promjene.");
                }
                else
                {
                    var k = Kosarice[index - 1];
                    UcitajProizvod();

                }
            }
        }

        private void DodavanjeKosarice()
        {
            var k = new Kosarica();
            UcitajKupca();
            UcitajProizvod();
            Kosarice.Add(k);
        }

        private Kupac UcitajKupca()
        {
            Izbornik.ObradaKupac.PrikaziKupce();
            int index = Pomocno.UcitajBrojRaspon("\tOdaberite broj kupca za dodavanje u košaricu: ", "\tGreška! Odabir mora biti jedan od ponuđenih brojeva!", 1, Izbornik.ObradaKupac.Kupci.Count());
            return Izbornik.ObradaKupac.Kupci[index - 1];
        }

        private List<Proizvod> PostaviProizvode()
        {
            List<Proizvod> proizvodi = new List<Proizvod>();
            while (Pomocno.UcitajBool("\tŽelite li dodati proizvod? (unesite ''da'' za dodavanje, bilo što drugo za ne): "))
            {
                proizvodi.Add(UcitajProizvod());
            }

            return proizvodi;
        }

        private Proizvod UcitajProizvod()
        {
            Izbornik.ObradaProizvod.PrikaziProizvode();
            int index = Pomocno.UcitajBrojRaspon("\tOdaberite broj proizvoda za dodavanje u košaricu: ", "\tGreška! Odabir mora biti jedan od ponuđenih brojeva!", 1, Izbornik.ObradaProizvod.Proizvodi.Count());
            return Izbornik.ObradaProizvod.Proizvodi[index - 1];
        }

        private void DetaljiKosarice()
        {
            Console.WriteLine();
            int index = Pomocno.UcitajBrojRaspon("\tZa detalje odaberite broj korisnika (0 za povratak na izbornik): ", "\tGreška! Odabir mora biti jedan od ponuđenih brojeva", 0, Kosarice.Count());
            if (index != 0)
            {
                var k = Kosarice[index - 1];

                Console.WriteLine("\t|---------------------------------------------------------------|");
                Console.WriteLine("\t|                         DETALJI KOSARICE                      |");
                Console.WriteLine("\t|---------------------------------------------------------------|");
                Console.WriteLine();
                Console.WriteLine("\tKorisničko ime: {0}", k.Kupac.KorisnickoIme);
                Console.WriteLine("\tNaziv košarice: {0}", k.Kosarice);
                Console.WriteLine("\tDatum stvaranja korisnika: {0}", k.DatumStvaranja);
                //Console.WriteLine("\tPrezime korisnika: {0}", p.Lozinka);
                //Console.WriteLine("\tPrezime korisnika: {0}", p.Telefon);
                //Console.WriteLine("\tAdresa korisnika: {0}", p.Adresa);
                Console.WriteLine();
                Console.WriteLine("\t|---------------------------------------------------------------|\n");
            }
        }

        private void PregledKosarice()
        {
            if (Kosarice.Count == 0)
            {
                Console.WriteLine("\tNema popunjene košarice za prikaz!");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("  \t-------------------------------------------");
                Console.WriteLine("\t|            PREGLED KOŠARICE            |");
                Console.WriteLine("  \t-------------------------------------------");
                Console.WriteLine();
                int broj = 1;

                foreach (Kosarica k in Kosarice)
                {
                    Console.WriteLine("\t {0}. {1}", broj++, k);
                }

                Console.WriteLine("\n \t|------------------------------------------|");
            }
        }
    }
}
