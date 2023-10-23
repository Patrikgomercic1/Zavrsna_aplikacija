using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LjetnaAplikacija
{
    internal class Kosarica : Entitet
    {
        public Kupac? Kupac { get; }

        public Proizvod? Proizvod { get;}

        public DateTime? DatumStvaranja { get; set;}

        public List<Kosarica> Kosarice { get; set; }
        
        public override string ToString()
        {
            return null;//dodati prikaz  detaljnijih atributa kosarice
        }

    }
}
