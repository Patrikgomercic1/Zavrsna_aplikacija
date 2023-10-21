using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineTrgovina.Models
{
    public class Kosarica : Entitet
    {
        [ForeignKey("kupac")]
        public Kupac? Kupac { get; set; }

        public DateTime? DatumStvaranja { get; set; }



        //dodati proizvod kao strani ključ?
        [ForeignKey("proizvod")]
        public Proizvod? Proizvod { get; set; }

        public ICollection<Kosarica> Kosarica { get; }
    }
}
