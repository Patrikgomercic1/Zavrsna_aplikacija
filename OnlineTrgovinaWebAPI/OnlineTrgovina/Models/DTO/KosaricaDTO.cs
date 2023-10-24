namespace OnlineTrgovina.Models.DTO
{
    public class KosaricaDTO
    {
        public int Sifra { get; set; }

        public string Kupac { get; set; }

        public int KolicinaProizvod { get; set; }

        public DateTime? DatumStvaranja { get; set; }

        //Koristi se za prikaz količine individualnih proizvoda u košarici
        public int Proizvodi {  get; set; }

        //Koristi se za Post route 
        public int SifraKupac { get; set; }
    }
}