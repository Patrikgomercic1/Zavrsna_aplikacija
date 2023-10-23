using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineTrgovina.Models
{
    public class Inventar : Entitet
    {
        [ForeignKey("proizvod")]
        public Proizvod? Proizvod { get; set; }

        public string? Kategorija { get; set; }

        [Required]
        public int Kolicina { get; set; }

        [Required]
        public bool Dostupnost {  get; set; }


       
    }
}
