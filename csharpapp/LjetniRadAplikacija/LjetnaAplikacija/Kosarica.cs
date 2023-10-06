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
        public Kupac Kupac { get; set; }
        public Proizvod Proizvod { get; set; }
        public List<Kosarica> Kosarice { get; set; }
        
        public override string ToString()
        {
            return Kupac + ", " + Proizvod + "(" + Kolicina + ")";
        }

    }
}
