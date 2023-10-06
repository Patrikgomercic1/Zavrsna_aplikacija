using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LjetnaAplikacija
{
    internal class Izbornik
    {

        public ObradaKupac ObradaKupac {  get; }
        private ObradaKosarica ObradaKosarica;
        public ObradaProizvod ObradaProizvod { get; }
        private ObradaInventar ObradaInventar;

        public Izbornik()
        {
            ObradaKupac = new ObradaKupac();
            ObradaKosarica = new ObradaKosarica(this);
            ObradaProizvod = new ObradaProizvod();
            ObradaInventar = new ObradaInventar(this);
            PozdravnaPoruka();
            PrikaziIzbornik();     
        }

        private void PozdravnaPoruka()
        {
            Console.WriteLine();
            Console.WriteLine("\t------------------*-----------------------");
            Console.WriteLine("\t|******* Online Trgovina v3.0 ***********|");
            Console.WriteLine("\t------------------*-----------------------\n");
        }

        private void PrikaziIzbornik()  
        {
            Console.WriteLine("  \t¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯ ");
            Console.WriteLine(" \t|¯¯¯¯¯¯¯¯| GLAVNI IZBORNIK |¯¯¯¯¯¯¯¯|");
            Console.WriteLine("  \t¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯¯\n ");
            Console.WriteLine(" \t1. Kupac");
            Console.WriteLine(" \t2. Košarica");
            Console.WriteLine(" \t3. Proizvodi");
            Console.WriteLine(" \t4. Inventar");
            Console.WriteLine(" \t5. Izlaz iz programa");
            
            switch(Pomocno.UcitajBrojRaspon("\n \tOdaberite stavku izbornika: ", "\n \tOdabir mora biti broj između 1 i 5", 1, 5))
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
                    ObradaProizvod.PrikaziIzbornik();
                    PrikaziIzbornik();
                    break;
                case 4:
                    ObradaInventar.PrikaziIzbornik();
                    PrikaziIzbornik();
                    break;
                case 5:
                    Console.WriteLine("\n \tHvala na korištenju, doviđenja.");
                    break;
            }

        }

    }
}
