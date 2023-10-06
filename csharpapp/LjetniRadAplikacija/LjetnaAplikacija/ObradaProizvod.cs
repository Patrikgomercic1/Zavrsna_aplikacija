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
            Console.WriteLine(" \t---| Izbornik za rad s proizvodima |---");
            Console.WriteLine(" \t---------------------------------------");

            Console.WriteLine("\t1. Pregled postojećih proizvoda");
            Console.WriteLine("\t2. Unos novog proizvoda");
            Console.WriteLine("\t3. Promjena postojećeg proizvoda");
            Console.WriteLine("\t4. Brisanje proizvoda");
            Console.WriteLine("\t5. Povratak na glavni izbornik\n");

            switch (Pomocno.UcitajBrojRaspon("\n\tOdaberite stavku izbornika: ",  "\n\tOdabir mora biti između 1 i 5", 1, 5))
            {
                case 1:
                    PrikaziProizvode();
                    DetaljiProizvoda();
                    PrikaziIzbornik();
                    break;
                case 2:
                    UnosNovogProizvoda();
                    PrikaziIzbornik();
                    break;
                case 3:
                    PromjenaProizvoda();
                    PrikaziIzbornik();
                    break;
                case 4:
                    BrisanjeProizvoda();
                    PrikaziIzbornik();
                    break;
                case 5:
                    Console.WriteLine("\t~~Vraćanje na prijašnji izbornik~~");
                    break;
            }
        }

        public void BrisanjeProizvoda()
        {
            if(Proizvodi.Count == 0)
            {
                Console.WriteLine("\tNema proizvoda za brisanje!");
            }
            else
            {
                PrikaziProizvode();
                int index = Pomocno.UcitajBrojRaspon("\tOdaberite redni broj proizvoda za brisanje, za ODUSTAJANJE unesite 0: ", "\tMorate unijeti broj koji je trenutno u korištenju", 0, Proizvodi.Count());
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
                Console.WriteLine("\tNema proizvoda za promjenu");
            }
            else
            {
                PrikaziProizvode();
                int index = Pomocno.UcitajBrojRaspon("\tOdaberite redni broj proizvoda za promjenu, za ODUSTAJANJE unesite 0: ", "\tMorate unijeti broj koji je trenutno u korištenju", 0, Proizvodi.Count());
                if(index == 0)
                {
                    Console.WriteLine("\tOdustali ste od promjene.");
                }
                else
                {
                    var p = Proizvodi[index - 1];
                    //p.Sifra = Pomocno.UcitajCijeliBroj("\tUnesite novu ŠIFRU proizvoda (" + p.Sifra + "): ", "\tObavezno morate unijeti ŠIFRU proizvoda");
                    p.Naziv = Pomocno.UcitajString("\tUnesite NAZIV proizvoda (" + p.Naziv + "): ", "\tObavezno morate unijeti NAZIV proizvoda");
                    p.Opis = Pomocno.UcitajString("\tUnesite OPIS proizvoda (" + p.Opis + "): ", "\tObavezno morate unijeti OPIS proizvoda");
                    p.Cijena = Pomocno.UcitajCijeliBroj("\tUnesite CIJENU proizvoda (" + p.Cijena + "): ", "\tObavezno morate unijeti CIJENU proizvoda");
                }
            }
        }

        public void UnosNovogProizvoda()
        {
            var p = new Proizvod();
            p.Naziv = Pomocno.UcitajString("\tUnesite NAZIV proizvoda: ", "\tObavezno morate unijeti NAZIV proizvoda");
            p.Opis = Pomocno.UcitajString("\tUnesite OPIS proizvoda: ", "\tObavezno morate unijeti OPIS proizvoda");
            p.Cijena = Pomocno.UcitajCijeliBroj("\tUnesite CIJENU proizvoda: ", "\tObavezno morate unijeti CIJENU proizvoda");//prebacuje se u idući red

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

                Console.WriteLine("\n \t|---------------------------------|");
            }
        }

        private void DetaljiProizvoda()
        {
            Console.WriteLine();
            int index = Pomocno.UcitajBrojRaspon("\tZa detalje odaberite broj proizvoda (0 za povratak na izbornik): ", "\tOdabir mora biti jedan od ponuđenih brojeva", 0, Proizvodi.Count());
            if (index != 0)
            {
                var p = Proizvodi[index - 1];

                Console.WriteLine("\t|---------------------------------|");
                Console.WriteLine();
                Console.WriteLine("\t Naziv proizvoda: {0}", p.Naziv);
                Console.WriteLine("\t Opis proizvoda: {0}", p.Opis);
                Console.WriteLine("\t Cijena proizvoda: {0}", p.Cijena);
                Console.WriteLine();
                Console.WriteLine("\t|---------------------------------|");
            }
        }   

        private void TestniPodaci()
        {
            Proizvodi.Add(new Proizvod() 
            { 
                Naziv = "Majica" ,
                Opis="Testni proizvod", 
                Cijena=25
            });

            Proizvodi.Add(new Proizvod()
            {
                Naziv = "Majica2",
                Opis = "Testni proizvod2",
                Cijena = 50
            });
        }

    }
}
