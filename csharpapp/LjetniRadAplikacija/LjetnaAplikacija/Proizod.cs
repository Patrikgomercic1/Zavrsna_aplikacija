using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LjetnaAplikacija
{
    internal class Proizvod : Entitet
    {
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public int Cijena { get; set; }
    }
}
