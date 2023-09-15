using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LjetnaAplikacija
{
    internal class Kosarica : Entitet
    {
        public int Kolicina { get; set; }
        public int Kupac { get; set; }
        public int Proizvod { get; set; }

        public override string ToString()
        {
            return Kolicina + " " + Kupac + " " + Proizvod;
        }
    }
}
