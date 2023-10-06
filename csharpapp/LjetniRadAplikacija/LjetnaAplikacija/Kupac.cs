using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LjetnaAplikacija
{
    internal class Kupac : Entitet
    {
        public string KorisnickoIme { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Lozinka { get; set; }
        public int Telefon { get; set; }    //promjeniti u sql na int
        public string Adresa { get; set; }
        public List<Kupac> Kupci { get; set; }
        public override string ToString()
        {
            return KorisnickoIme;
        }
    }
}
