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
        public List<Kosarica> Kosarice { get;  }

        public ObradaKosarica() 
        {
            Kosarice = new List<Kosarica>();
            
        }


        public void PrikaziIzbornik()
        {
            Console.WriteLine("Izbornik za rad s košaricom");
            Console.WriteLine("1. Pregled košarice");
            Console.WriteLine("2. Promjena košarice");
            Console.WriteLine("3. Brisanje košarice");
            Console.WriteLine("4. Povratak na glavni izbornik");

            switch (Pomocno.UcitajBrojRaspon("Odaberite stavku izbornika košarice: ", "Odabri mora biti od 1 do 4", 1, 4))
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
                    Console.WriteLine("BrisanjeKosarice");
                    PrikaziIzbornik();
                    break;
                case 4:
                    Console.WriteLine("Gotov rad s košaricom");
                    break;
            }
        }

        private void PregledKosarice()  //pregled proizvoda u košarici
        {
            
        }

        private void PromjeniKosaricu()
        {
           

        }
    }
}
