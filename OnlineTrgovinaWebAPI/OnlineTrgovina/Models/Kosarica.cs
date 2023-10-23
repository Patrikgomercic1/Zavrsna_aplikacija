using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace OnlineTrgovina.Models
{
    public class Kosarica : Entitet
    {
        [ForeignKey("kupac")]
        public Kupac? Kupac { get; set; }

        [Required]
        public int KolicinaProizvod { get; set; }

        public DateTime? DatumStvaranja { get; set; }


        public List<Proizvod> Proizvodi { get; set; }
    }
}

