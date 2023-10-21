using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineTrgovina.Models.DTO
{
    public class InventarDTO
    {
        public int Sifra { get; set; }
        public int SifraProizvod { get; set; }
        public int Kolicina { get; set; }
        public bool Dostupnost { get; set; }
    }
}
