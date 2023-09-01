using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LjetnaAplikacija
{
    internal class Izbornik
    {
        private ObradaGrupa ObradaGrupa;
        public ObradaSmjer ObradaSmjer { get; }
        public ObradaPolaznik ObradaPolaznik { get; }

        public Izbornik()
        {
            ObradaGrupa = new ObradaGrupa(this);
            ObradaPolaznik = new ObradaPolaznik();
            ObradaSmjer = new ObradaSmjer();
            PozdravnaPoruka();
            PrikaziIzborinik();
        }

        private void PozdravnaPoruka()
        {
            Console.WriteLine("************************************");
            Console.WriteLine("***** Edunova Console App v 1.0*****");
            Console.WriteLine("************************************");
        }

        private void PrikaziIzborinik()
        {
            Console.WriteLine("Glavni Izbornik");
            Console.WriteLine("1. Smjerovi");
            Console.WriteLine("2. Polaznici");
            Console.WriteLine("3. Grupe");
            Console.WriteLine("4. Izlaz iz programa");

            switch (Pomocno.UcitajRaspon("Odaberite stavku izbornika: ", "Odabir mora biti 1 - 4.", 1, 4))
            {
                case 1:
                    ObradaSmjer.PrikaziIzbornik();
                    PrikaziIzborinik();
                    break;
                case 2:
                    ObradaPolaznik.PrikaziIzbornik();
                    PrikaziIzborinik();
                    break;
                case 3:
                    ObradaGrupa.PrikaziIzbornik();
                    PrikaziIzborinik();
                    break;
                case 4:
                    Console.WriteLine("Hvala na korištenju, doviđenja");
                    break;
            }

        }
    }
}
