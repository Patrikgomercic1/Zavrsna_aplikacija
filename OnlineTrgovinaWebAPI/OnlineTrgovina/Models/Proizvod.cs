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
        [Range(6,2)]
        public decimal Cijena { get; set; }
        [Required]
        public int Kolicina { get; set; }
        [Required]
        public bool Dostupnost { get; set; }
    }
}
