using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineTrgovina.Models.DTO
{
    public class InventarDTO
    {
        public int Sifra { get; set; }

        public string Proizvod {  get; set; }

        public string Kategorija { get; set; }

        public int Kolicina { get; set; }

        public bool Dostupnost { get; set; }

        //koristi se za prikaz šifre proizvoda u inventaru
        public Proizvod? SifraProizvod { get; set; }
    }
}
