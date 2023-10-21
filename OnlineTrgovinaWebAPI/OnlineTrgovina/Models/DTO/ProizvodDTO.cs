using System.ComponentModel.DataAnnotations;

namespace OnlineTrgovina.Models.DTO
{
    public class ProizvodDTO
    {
        public int Sifra { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public decimal Cijena { get; set; }
    }
}
