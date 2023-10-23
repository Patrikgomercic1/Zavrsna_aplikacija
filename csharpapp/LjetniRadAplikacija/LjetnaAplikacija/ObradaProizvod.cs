using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LjetnaAplikacija
{
    internal class ObradaProizvod
    {
        public List<Proizvod> Proizvodi {  get; }

        public ObradaProizvod() 
        {
            Proizvodi = new List<Proizvod>();
            if(Pomocno.Dev==true)
            {
                TestniPodaci();
            }
            
        }

        public void PrikaziIzbornik()
        {
            Console.WriteLine();
            Console.WriteLine(" \t---------------------------------------");
            Console.WriteLine(" \t---| Izbornik za rad s Proizvodima |---");
            Console.WriteLine(" \t---------------------------------------");
            Console.WriteLine();

            Console.WriteLine("\t1. Pregled postojećih proizvoda");
            Console.WriteLine("\t2. Promjena postojećih proizvoda");
            Console.WriteLine("\t3. Povratak na glavni izbornik\n");

            switch (Pomocno.UcitajBrojRaspon("\n\tOdaberite stavku izbornika: ", "\n\tGreška!Odabir mora biti između 1 i 3!", 1, 3))
            {
                case 1:
                    PrikaziProizvode();
                    DetaljiProizvoda();
                    PrikaziIzbornik();
                    break;
                case 2: 
                    PromjenaProizvoda();
                    PrikaziIzbornik();
                    break;
                case 3:
                    Console.WriteLine("\n\t~~Vraćanje na prijašnji izbornik~~\n");
                    break;
            }
        }

        public void BrisanjeProizvoda()
        {
            if(Proizvodi.Count == 0)
            {
                Console.WriteLine("\tGreška! Nema proizvoda za brisanje!");
            }
            else
            {
                PrikaziProizvode();
                int index = Pomocno.UcitajBrojRaspon("\tOdaberite redni broj proizvoda za brisanje, za odustajanje unesite 0: ", "\tGreška! Morate unijeti broj koji je trenutno u korištenju!", 0, Proizvodi.Count());
                if (index == 0)
                {
                    Console.WriteLine("\tOdustali ste od promjene.");
                }
                else
                {
                    Proizvodi.RemoveAt(index - 1);
                    Console.WriteLine("\tProizvod je uspješno obrisan.");
                }
            }           
        }

        private void PromjenaProizvoda()
        {
            if (Proizvodi.Count == 0)
            {
                Console.WriteLine("\tNema proizvoda za promjenu!");
            }
            else
            {
                PrikaziProizvode();
                int index = Pomocno.UcitajBrojRaspon("\tOdaberite redni broj proizvoda za promjenu, za odustajanje unesite 0: ", "\tGreška! Morate unijeti broj koji je trenutno u korištenju!", 0, Proizvodi.Count());
                if(index == 0)
                {
                    Console.WriteLine("\tOdustali ste od promjene.");
                }
                else
                {
                    var p = Proizvodi[index - 1];
                    p.Naziv = Pomocno.UcitajString("\tUnesite NAZIV proizvoda (" + p.Naziv + "): ", "\tGreška! Morate unijeti znak/broj!");
                    p.Opis = Pomocno.UcitajString("\tUnesite OPIS proizvoda (" + p.Opis + "): ", "\tGreška! Morate unijeti znak/broj!");
                    p.Cijena = Pomocno.UcitajCijeliBroj("\tUnesite CIJENU proizvoda (" + p.Cijena + "): ", "\tGreška! Morate unijeti cijeli broj!");
                }
            }
        }

        public void UnosNovogProizvoda()
        {
            var p = new Proizvod();
            p.Naziv = Pomocno.UcitajString("\tUnesite NAZIV proizvoda: ", "\tGreška! Morate unijeti znak/broj!");
            p.Opis = Pomocno.UcitajString("\tUnesite OPIS proizvoda: ", "\tGreška! Morate unijeti znak/broj!");
            p.Cijena = Pomocno.UcitajCijeliBroj("\tUnesite CIJENU proizvoda: ", "\tGreška! Morate unijeti cijeli broj!");  //prebacuje se u idući red
            Proizvodi.Add(p);
        }

        public void PrikaziProizvode()
        {
            if(Proizvodi.Count == 0) 
            {
                Console.WriteLine("\tNema proizvoda za prikaz");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("  \t-----------------------------------");
                Console.WriteLine("\t|            PROIZVODI            |");
                Console.WriteLine("  \t-----------------------------------");
                Console.WriteLine();
                int broj = 1;

                foreach(Proizvod p in Proizvodi)
                {
                    Console.WriteLine("\t {0}. {1}", broj++,p);
                }

                Console.WriteLine("\n \t|---------------------------------|\n");
            }
        }

        private void DetaljiProizvoda()
        {
            Console.WriteLine();
            int index = Pomocno.UcitajBrojRaspon("\tZa detalje odaberite broj proizvoda (0 za povratak na izbornik): ", "\tOdabir mora biti jedan od ponuđenih brojeva", 0, Proizvodi.Count());
            if (index != 0)
            {
                var p = Proizvodi[index - 1];

                Console.WriteLine("\t|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|");
                Console.WriteLine();
                Console.WriteLine("\t Naziv proizvoda: {0}", p.Naziv);
                Console.WriteLine("\t Opis proizvoda: {0}", p.Opis);
                Console.WriteLine("\t Cijena proizvoda: {0}", p.Cijena);
                Console.WriteLine();
                Console.WriteLine("\t|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|");
            }
        }

        private void PrikazAtriburaInventara()
        {
            var p = new Proizvod();
            Izbornik.ObradaInventar.DetaljiInventara();
            Proizvodi.Add(p);
        }

        private void TestniPodaci()
        {
            Proizvodi.Add(new Proizvod() 
            { 
                Naziv = "Majica" ,
                Opis = "Testni proizvod", 
                Cijena = 10                
            });

            Proizvodi.Add(new Proizvod()
            {
                Naziv = "Potkošulja",
                Opis = "Testni proizvod2",
                Cijena = 20
            });

            Proizvodi.Add(new Proizvod()
            {
                Naziv = "Hlače",
                Opis = "Testni proizvod3",
                Cijena = 30
            });
        }

    }
}
