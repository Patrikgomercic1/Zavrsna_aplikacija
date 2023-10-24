using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

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

        //Omogućuje prikaz proizvoda unutar unesenih klasa
        public ICollection<Kosarica> Kosarice { get; } = new List<Kosarica>();  
    }
}
