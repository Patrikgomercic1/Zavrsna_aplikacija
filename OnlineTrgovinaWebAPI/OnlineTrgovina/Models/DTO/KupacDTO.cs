using System.ComponentModel.DataAnnotations;

namespace OnlineTrgovina.Models.DTO
{
    public class KupacDTO
    {
        public int Sifra { get; set; }
        public string KorisnickoIme { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Lozinka { get; set; }
        public string Telefon { get; set; }
        public string Adresa { get; set; }
    }
}
