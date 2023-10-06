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
            Console.WriteLine("Izbornik za rad s košaricom");
            Console.WriteLine("\t1. Pregled košarice");
            Console.WriteLine("\t2. Promjena košarice");
            Console.WriteLine("\t3. Dodavanje košarice");
            Console.WriteLine("\t4. Brisanje košarice");
            Console.WriteLine("\t5. Povratak na glavni izbornik");

            switch (Pomocno.UcitajBrojRaspon("Odaberite stavku izbornika: ", "Odabir mora biti između 1 i 5", 1, 5))
            {
                case 1:
                    PregledKosarice();
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
                    Console.WriteLine("\t~~Vraćanje na prijašnji izbornik~~");
                    break;
            }
        }

        private void BrisanjeKosarice()
        {
            if (Kosarice.Count == 0)
            {
                Console.WriteLine("\tNema proizvoda za brisanje!");
            }
            else
            {
                PregledKosarice();
                int index = Pomocno.UcitajBrojRaspon("\tOdaberite redni broj košarice za brisanje, za ODUSTAJANJE unesite 0: ", "\tMorate unijeti broj koji je trenutno u korištenju", 0, Kosarice.Count());
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
                Console.WriteLine("\tNema košarice za promjenu");
            }
            else
            {
                PregledKosarice();
                int index = Pomocno.UcitajBrojRaspon("\tOdaberite redni broj košarice za promjenu, za ODUSTAJANJE unesite 0: ", "\tMorate unijeti broj koji je trenutno u korištenju", 0, Kosarice.Count());
                if (index == 0)
                {
                    Console.WriteLine("\tOdustali ste od promjene.");
                }
                else
                {
                    var k = Kosarice[index - 1];
                    k.Proizvod = UcitajProizvod();
                    k.Kolicina = Pomocno.UcitajCijeliBroj("\tUnesite količinu proizvoda (" + k.Kolicina + "): ", "\tObavezno morate unijeti količinu proizvoda");
                    
                }
            }
        }

        private void DodavanjeKosarice()
        {
            var k = new Kosarica();
            k.Proizvod = UcitajProizvod();
            k.Kolicina = Pomocno.UcitajCijeliBroj("\tUnesite količinu proizvoda: ", "\tGreška! Morate unijeti cijeli broj.");
            k.Kupac = UcitajKupca();
            Kosarice.Add(k);
        }

        private Kupac UcitajKupca()
        {
            Izbornik.ObradaKupac.PrikaziKupce();
            int index = Pomocno.UcitajBrojRaspon("\tOdaberite broj kupca za dodavanje u košaricu: ", "\tGreška! Odabir mora biti jedan od ponuđenih brojeva.", 1, Izbornik.ObradaProizvod.Proizvodi.Count());
            return Izbornik.ObradaKupac.Kupci[index - 1];
        }

        private Proizvod UcitajProizvod()
        {
            Izbornik.ObradaProizvod.PrikaziProizvode();
            int index = Pomocno.UcitajBrojRaspon("\tOdaberite broj proizvoda za dodavanje u košaricu: ", "\tGreška! Odabir mora biti jedan od ponuđenih brojeva.", 1, Izbornik.ObradaProizvod.Proizvodi.Count());
            return Izbornik.ObradaProizvod.Proizvodi[index - 1];
        }

        private void PregledKosarice()
        {
            if (Kosarice.Count == 0)
            {
                Console.WriteLine("\tNema popunjene košarice za prikaz");
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

                Console.WriteLine("\n \t|---------------------------------|");
            }
        }
    }
}
