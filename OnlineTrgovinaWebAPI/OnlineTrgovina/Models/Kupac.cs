using OnlineTrgovina.Validations;
using System.ComponentModel.DataAnnotations;

namespace OnlineTrgovina.Models
{
    public class Kupac : Entitet
    {
		[Required]
        [NazivNeMozeBitiBroj]
        public string KorisnickoIme {  get; set; }

        [Required]
        [NazivNeMozeBitiBroj]
        public string Ime {  get; set; }

        [Required]
        [NazivNeMozeBitiBroj]
        public string Prezime {  get; set; }

        [Required]
        public string Lozinka {  get; set; }

		public string Telefon {  get; set; }

		public string Adresa {  get; set; } 
    }
}
