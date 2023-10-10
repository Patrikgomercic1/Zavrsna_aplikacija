namespace OnlineTrgovina.Models
{
    public class Proizvod : Entitet
    {
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public int Cijena { get; set; }
        public int Kolicina { get; set; }
        public bool Dostupnost { get; set; }
    }
}
