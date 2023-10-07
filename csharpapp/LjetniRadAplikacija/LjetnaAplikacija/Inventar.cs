using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LjetnaAplikacija
{
    internal class Inventar : Entitet
    {
        public Proizvod Proizvod { get; set; }       
        public List<Proizvod> Proizvodi { get; set; }
    }
}
