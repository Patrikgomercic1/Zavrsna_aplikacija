using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LjetnaAplikacija
{   
    internal class ObradaInventar
    {
        public List<Inventar> Inventari;
        private Izbornik Izbornik;

        public ObradaInventar() 
        {
            Inventari = new List<Inventar>();
        }

        public ObradaInventar(Izbornik izbornik):this()
        {
            this.Izbornik = izbornik;
        }

        public void PrikaziIzbornik()
        {
            Console.WriteLine("Izbornik za rad s Inventarom");
            Console.WriteLine("\t1. Pregled proizvoda u inventaru");
            Console.WriteLine("\t2. Unos novih proizvoda u inventar");
            Console.WriteLine("\t3. Promjena proizvoda u inventaru");
            Console.WriteLine("\t4. Brisanje proizvoda s inventara");
            Console.WriteLine("\t5. Povratak na glavni izbornik");

            switch (Pomocno.UcitajBrojRaspon("\tOdaberite stavku izbornika: ", "\tOdabri mora biti između 1 i 5", 1, 5))
            {
                case 1:
                    PrikaziInventar();
                    PrikaziIzbornik();
                    break;
                case 2:
                    UnosNovogProizvoda();
                    PrikaziIzbornik();
                    break;
                case 3:
                    PromjenaProizvodaInventara();
                    PrikaziIzbornik();
                    break;
                case 4:
                    BrisanjeProizvodaInventara();
                    PrikaziIzbornik();
                    break;
                case 5:
                    Console.WriteLine("\t~~Vraćanje na prijašnji izbornik~~");
                    break;
            }
        }

        private void UnosNovogProizvoda()
        {
            var i = new Inventar();
            Izbornik.ObradaProizvod.UnosNovogProizvoda();
            i.Kolicina = Pomocno.UcitajCijeliBroj("\tUnesite količinu odabranog proizvoda: ", "\tGreška! Morate unijeti cijeli broj.");
            i.Dostupnost = Pomocno.UcitajBool("\tOdaberite dostupnost odabranog proizvoda, za potvrdu unesite ''da'', za odbijanje unesite bilo što: ");
            Inventari.Add(i);
        }

        private void PrikaziInventar()
        {
            Izbornik.ObradaProizvod.PrikaziIzbornik();
        }

        private void BrisanjeProizvodaInventara()
        {
            Izbornik.ObradaProizvod.BrisanjeProizvoda();
        }

        private void PromjenaProizvodaInventara()
        {
            var i = new Inventar();
            i.Proizvod = UcitajProizvod();
            i.Kolicina = Pomocno.UcitajCijeliBroj("\tUnesite količinu odabranog proizvoda: ", "\tGreška! Morate unijeti cijeli broj.");
            i.Dostupnost = Pomocno.UcitajBool("\tOdaberite dostupnost odabranog proizvoda, za potvrdu unesite ''da'', za odbijanje unesite bilo što: ");
            Inventari.Add(i);
        }

        private Proizvod UcitajProizvod()
        {
            Izbornik.ObradaProizvod.PrikaziProizvode();
            int index = Pomocno.UcitajBrojRaspon("\tOdaberite broj proizvoda za prikaz: ", "\tGreška! Odabir mora biti jedan od ponuđenih brojeva.", 1, Izbornik.ObradaProizvod.Proizvodi.Count());
            return Izbornik.ObradaProizvod.Proizvodi[index - 1];
        }

    }
  
}
