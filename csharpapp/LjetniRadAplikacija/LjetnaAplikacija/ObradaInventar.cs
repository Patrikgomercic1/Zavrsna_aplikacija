using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LjetnaAplikacija
{   
    internal class ObradaInventar
    {
        public List<Inventar> Inventari { get; }

        public ObradaInventar() 
        {
            Inventari = new List<Inventar>();
        }

        public void PrikaziIzbornik()
        {
            Console.WriteLine("Izbornik za rad s Inventarom");
            Console.WriteLine("1. Pregled postojećih inventara");
            Console.WriteLine("2. Unos novog inventara");
            Console.WriteLine("3. Promjena postojećeg inventara");
            Console.WriteLine("4. Brisanje inventara");
            Console.WriteLine("5. Povratak na glavni izbornik");

            switch (Pomocno.UcitajBrojRaspon("Odaberite stavku izbornika kupca: ", "Odabri mora biti od 1 do 5", 1, 5))
            {
                case 1:
                    Console.WriteLine("PrikaziInventar"); 
                    PrikaziIzbornik();
                    break;
                case 2:
                    Console.WriteLine("UnosNovogInventara");
                    PrikaziIzbornik();
                    break;
                case 3:
                    Console.WriteLine("PromjenaInventara");
                    PrikaziIzbornik();
                    break;
                case 4:
                    Console.WriteLine("BrisanjeInventara");
                    PrikaziIzbornik();
                    break;
                case 5:
                    Console.WriteLine("Gotov rad s kupcima");
                    break;
            }
        }


    }

    
}
