using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LjetnaAplikacija
{
    internal class Inventar : Entitet
    {
        public int Proizvod { get; set; }
        public int Kolicina { get; set; }
        public string Dostupnost { get; set; }
    }
}
