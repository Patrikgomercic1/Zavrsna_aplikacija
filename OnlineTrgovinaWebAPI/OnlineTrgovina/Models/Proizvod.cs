using System.ComponentModel.DataAnnotations;

namespace OnlineTrgovina.Models
{
    public class Proizvod : Entitet
    {
        [Required]
        public string Naziv { get; set; }

        [Required]
        public string Opis { get; set; }

        [Required]
        [Range(0,10000)]
        public decimal Cijena { get; set; }

        public ICollection<Inventar> Inventar { get; }
        public ICollection<Kosarica> Kosarica { get; }

    }
}
