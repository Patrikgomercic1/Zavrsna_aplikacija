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

        }

        public void PrikaziIzbornik()
        {
            Console.WriteLine("Izbornik za rad s proizvodima");
            Console.WriteLine("1. Pregled postojećih proizvoda");
            Console.WriteLine("2. Unos novog proizvoda");
            Console.WriteLine("3. Promjena postojećeg proizvoda");
            Console.WriteLine("4. Brisanje proizvoda");
            Console.WriteLine("5. Povratak na glavni izbornik");

            switch (Pomocno.UcitajBrojRaspon("Odaberite stavku izbornika kupca: ", "Odabri mora biti od 1 do 5", 1, 5))
            {
                case 1:
                    Console.WriteLine("PrikaziProizvode");
                    PrikaziIzbornik();
                    break;
                case 2:
                    Console.WriteLine("UnosNovogProizvoda");
                    PrikaziIzbornik();
                    break;
                case 3:
                    Console.WriteLine("PromjenaProizvoda");
                    PrikaziIzbornik();
                    break;
                case 4:
                    Console.WriteLine("BrisanjeProizvoda");
                    PrikaziIzbornik();
                    break;
                case 5:
                    Console.WriteLine("Gotov rad s kupcima");
                    break;
            }
        }

    }
}
