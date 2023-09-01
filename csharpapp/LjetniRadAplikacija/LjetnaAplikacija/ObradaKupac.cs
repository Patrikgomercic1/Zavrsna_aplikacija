using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LjetnaAplikacija
{
    internal class ObradaKupac
    {
        public List<Polaznik> Polaznici { get; }

        public ObradaPolaznik()
        {
            Polaznici = new List<Polaznik>();
            TestniPodaci();
        }

        private void TestniPodaci()
        {
            Polaznici.Add(new Polaznik()
            {
                Ime = "Marija",
                Prezime = "Zimska"
            });

            Polaznici.Add(new Polaznik()
            {
                Ime = "Goran",
                Prezime = "Jater"
            });

            Polaznici.Add(new Polaznik()
            {
                Ime = "Matija",
                Prezime = "Matijević"
            });
        }

        public void PrikaziIzbornik()
        {
            Console.WriteLine("Izbornik za rad s polaznicima");
            Console.WriteLine("1. Pregled postojećih polaznika");
            Console.WriteLine("2. Unos novog polaznika");
            Console.WriteLine("3. Promjena postojećeg polaznika");
            Console.WriteLine("4. Brisanje polaznika");
            Console.WriteLine("5. Povratak na glavni izbornik");

            switch (Pomocno.UcitajRaspon("Odaberite stavku izbornika polaznika: ", "Odabir mora biti 1-5", 1, 5))
            {
                case 1:
                    PregledPolaznika();
                    PrikaziIzbornik();
                    break;

                case 2:
                    UcitajPolaznika();
                    PrikaziIzbornik();
                    break;
                case 3:
                    PromjenaPolaznika();
                    PrikaziIzbornik();
                    break;

                case 4:
                    if (Polaznici.Count == 0)
                    {
                        Console.WriteLine("Nema polaznika za brisanje");
                    }
                    else
                    {
                        BrisanjePolaznika();
                    }
                    PrikaziIzbornik();
                    break;
                case 5:
                    Console.WriteLine("Gotov rad s polaznicima");
                    break;
            }

        }

        private void PromjenaPolaznika()
        {
            if (Polaznici.Count == 0)
            {
                Console.WriteLine("Nema polaznika za promjenu");
            }
            else
            {
                PregledPolaznika();
                int index = Pomocno.UcitajRaspon("Odaberi redni broj polaznika za promjenu, za odustajanje unesi 00 za povratak: ", "Nije dobro", 00, Polaznici.Count());
                if (index == 00)
                {
                    Console.WriteLine("Odustao si od promjene");
                }
                else
                {
                    var p = Polaznici[index - 1];

                    p.Sifra = Pomocno.ucitajCijeliBroj("Unesi novu šifru polaznika (" + p.Sifra + "): ", "Unos mora biti pozitivan cijeli broj");
                    p.Ime = Pomocno.UcitajString("Unesi novo ime polaznika (" + p.Ime + "): ", "Unos imena je obavezan");
                    p.Prezime = Pomocno.UcitajString("Unesi novo prezime polaznika (" + p.Prezime + "): ", "Unos prezimena je obavezan");
                    p.Email = Pomocno.UcitajString("Unesi novi e-mail polaznika (" + p.Email + "): ", "Unos e-maila je obavezan");
                    p.Oib = Pomocno.UcitajString("Unesi novi OIB polaznika (" + p.Oib + "): ", "Unos OIB-a je obavezan");
                }

            }
        }

        private void BrisanjePolaznika()
        {
            PregledPolaznika();
            int broj = Pomocno.UcitajRaspon("Odaberi redni broj polaznika za brisanje: ", "Nije dobro", 1, Polaznici.Count());
            Polaznici.RemoveAt(broj - 1);
        }

        public void PregledPolaznika()
        {
            int b = 1;
            foreach (Polaznik polaznik in Polaznici)
            {
                Console.WriteLine("\t {0}. {1}", b++, polaznik);
            }
        }

        private void UcitajPolaznika()
        {
            var p = new Polaznik();
            p.Ime = Pomocno.UcitajString("Unesi ime polaznika:", "Ime obavezno");
            p.Prezime = Pomocno.UcitajString("Unesi prezime polaznika:", "Prezime obavezno");
            //ostala svojstva kasnije
            Polaznici.Add(p);
        }
    }
}
