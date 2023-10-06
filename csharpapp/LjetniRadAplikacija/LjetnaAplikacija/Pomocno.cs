using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LjetnaAplikacija
{
    internal class Pomocno
    {

        public static bool Dev;

        public static int UcitajBrojRaspon(string poruka, string greska, int pocetak, int kraj)
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
                    Console.WriteLine(greska);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(greska);
                }
            }
        }

        internal static int UcitajCijeliBroj(string poruka, string greska)
        {
            int b;
            while (true)
            {
                Console.WriteLine(poruka);
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

        internal static decimal UcitajDecimalniBroj(string poruka, string greska)
        {
            decimal b;
            while (true)
            {
                Console.Write(poruka);
                try
                {
                    b = decimal.Parse(Console.ReadLine());
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

        internal static bool UcitajBool(string poruka)
        {
            Console.Write(poruka);

            return Console.ReadLine().Trim().ToLower().Equals("da") ? true : false;

        }

        internal static DateTime UcitajDatum(string poruka1, string poruka2)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine(poruka1);
                    return DateTime.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(poruka2);
                }
            }
        }


    }
}
