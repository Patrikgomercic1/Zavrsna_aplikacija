using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LjetnaAplikacija
{
    internal class Pomocno
    {
        public static int UcitajRaspon(string poruka, string greska, int pocetak, int kraj)
        {
            int b;
            while (true)
            {
                Console.Write(poruka);
                try
                {
                    b = int.Parse(Console.ReadLine());
                    if (b >= pocetak && b <= kraj)
                    {
                        return b;
                    }
                    Console.WriteLine("Greška");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Greška");
                }
            }

        }

        internal static int ucitajCijeliBroj(string poruka, string greska)
        {
            int b;
            while (true)
            {
                Console.Write(poruka);
                try
                {
                    b = int.Parse(Console.ReadLine());
                    if (b > 0)
                    {
                        return b;
                    }
                    Console.WriteLine(greska);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(greska);
                }
            }
        }

        internal static string UcitajString(string poruka, string greska)
        {
            string s = "";
            while (true)
            {
                Console.Write(poruka);
                s = Console.ReadLine();
                if (s != null && s.Trim().Length > 0)
                {
                    return s;
                }
                Console.WriteLine(greska);

            }
        }

        internal static string UcitajStringMijenjanje(string poruka, string vrati)
        {
            Console.Write(poruka);
            string odgovor = Console.ReadLine();
            if (odgovor == "")
                return vrati;
            else
                return odgovor;
        }


        internal static int UcitajBrojMijenjanje(string poruka, int vrati)
        {
            Console.Write(poruka);
            string odgovor = Console.ReadLine();
            if (odgovor == "")
                return vrati;
            else
                return Int32.Parse(odgovor);
        }
    }
}
