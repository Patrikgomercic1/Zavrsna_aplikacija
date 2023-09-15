using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LjetnaAplikacija
{
    internal class Izbornik
    {

        private ObradaKupac ObradaKupac;
        private ObradaKosarica ObradaKosarica;
        private ObradaProizvod ObradaProizvod;
        private ObradaInventar ObradaInventar;

        public Izbornik()
        {
            ObradaKupac = new ObradaKupac();
            ObradaKosarica = new ObradaKosarica();
            ObradaProizvod = new ObradaProizvod();
            ObradaInventar = new ObradaInventar();
            PozdravnaPoruka();
            PrikaziIzbornik();     
        }

        private void PozdravnaPoruka()
        {
            Console.WriteLine("******************************************");
            Console.WriteLine("******** Online Trgovina v1.0 ************");
            Console.WriteLine("******************************************");
        }

        private void PrikaziIzbornik()  
        {
            Console.WriteLine("Glavni Izbornik");
            Console.WriteLine("1. Kupac");
            Console.WriteLine("2. Košarica");
            Console.WriteLine("3. Proizvod");
            Console.WriteLine("4. Inventar");
            Console.WriteLine("5. Izlaz iz programa");
            
            switch(Pomocno.UcitajBrojRaspon("Odaberite stavku izbornika: ", "Odabir mora biti od 1 do 5", 1, 5))
            {
                case 1:
                    ObradaKupac.PrikaziIzbornik();
                    PrikaziIzbornik();
                    break;
                case 2:
                    ObradaKosarica.PrikaziIzbornik();
                    PrikaziIzbornik();
                    break;
                case 3:
                    Console.WriteLine("Rad s proizvodima");
                    PrikaziIzbornik();
                    break;
                case 4:
                    Console.WriteLine("Rad s inventarom");
                    PrikaziIzbornik();
                    break;
                case 5:
                    Console.WriteLine("Hvala na korištenju, doviđenja");
                    break;
            }

        }

    }
}
