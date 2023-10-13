using System.ComponentModel.DataAnnotations;

namespace OnlineTrgovina.Models
{
    public abstract class Entitet
    {
        [Key]
        public int Sifra { get; set; }
    }
}
