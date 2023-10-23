using System.ComponentModel.DataAnnotations;

namespace OnlineTrgovina.Models
{
    public class Kupac : Entitet
    {
		[Required]
		public string KorisnickoIme {  get; set; }

        [Required]
        public string Ime {  get; set; }

        [Required]
        public string Prezime {  get; set; }

        [Required]
        public string Lozinka {  get; set; }

		public string Telefon {  get; set; }

		public string Adresa {  get; set; }

       
    }
}
