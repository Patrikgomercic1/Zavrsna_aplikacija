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
            TestniPodaci();
        }

        public void PrikaziIzbornik()
        {
            Console.WriteLine();
            Console.WriteLine(" ---------------------------------------");
            Console.WriteLine(" ---| Izbornik za rad s proizvodima |---");
            Console.WriteLine(" ---------------------------------------");

            Console.WriteLine(" 1. Pregled postojećih proizvoda");
            Console.WriteLine(" 2. Unos novog proizvoda");
            Console.WriteLine(" 3. Promjena postojećeg proizvoda");
            Console.WriteLine(" 4. Brisanje proizvoda");
            Console.WriteLine(" 5. Povratak na glavni izbornik \n");

            switch (Pomocno.UcitajBrojRaspon("\nOdaberite stavku izbornika proizvod: ",  "\nOdabir mora biti od 1 do 5", 1, 5))
            {
                case 1:
                    PrikaziProizvode();
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
                    Console.WriteLine("\tGotov rad s proizvodima");
                    break;
            }
        }

        private void BrisanjeProizvoda()
        {
            PrikaziProizvode();
            int broj = Pomocno.UcitajBrojRaspon("\tOdaberite redni broj PROIZVODA za brisanje: ", "\tUneseni broj nije valjan.", 1, Proizvodi.Count());
            Proizvodi.RemoveAt(broj - 1);
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
                int index = Pomocno.UcitajBrojRaspon(" Odaberite redni broj proizvoda za promjenu, za ODUSTAJANJE unesite 0 za povratak: ", "Morate unijeti broj koji je trenutno u korištenju", 0, Proizvodi.Count());
                if(index == 0)
                {
                    Console.WriteLine(" Odustali ste od promjene.");
                }
                else
                {
                    var p = Proizvodi[index - 1];
                    p.Sifra = Pomocno.UcitajCijeliBroj(" Unesite novu ŠIFRU proizvoda (" + p.Sifra + "): ", " Obavezno morate unijeti ŠIFRU proizvoda");
                    p.Naziv = Pomocno.UcitajString(" Unesite NAZIV proizvoda (" + p.Naziv + "): ", " Obavezno morate unijeti NAZIV proizvoda");
                    p.Opis = Pomocno.UcitajString(" Unesite OPIS proizvoda (" + p.Opis + "): ", " Obavezno morate unijeti OPIS proizvoda");
                    p.Cijena = Pomocno.UcitajCijeliBroj("\tUnesite CIJENU proizvoda (" + p.Cijena + "): ", "\tObavezno morate unijeti CIJENU proizvoda");
                }
            }
        }

        private void UnosNovogProizvoda()
        {
            var p = new Proizvod();
            p.Naziv = Pomocno.UcitajString(" Unesite NAZIV proizvoda: ", " Obavezno morate unijeti NAZIV proizvoda");
            p.Opis = Pomocno.UcitajString(" Unesite OPIS proizvoda: ", " Obavezno morate unijeti OPIS proizvoda");
            p.Cijena = Pomocno.UcitajCijeliBroj(" Unesite CIJENU proizvoda: ", " Obavezno morate unijeti CIJENU proizvoda");//prebacuje se u idući red

            Proizvodi.Add(p);
        }

        private void PrikaziProizvode()
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

                Console.WriteLine("\n \t-----------------------------------");
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
