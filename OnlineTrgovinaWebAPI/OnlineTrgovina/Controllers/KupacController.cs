using Microsoft.AspNetCore.Mvc;
using OnlineTrgovina.Data;
using OnlineTrgovina.Models;
using OnlineTrgovina.Models.DTO;
//using OnlineTrgovina.Models.DTO;

namespace OnlineTrgovina.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]

    public class KupacController : ControllerBase
    {
        private readonly OTContext _context;

        public KupacController(OTContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get() 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var kupci = _context.Kupac.ToList();
            if(kupci == null || kupci.Count == 0) 
            {
                return new EmptyResult();
            }

            List<KupacDTO> vratiKupac = new();

            kupci.ForEach(k =>
            {
                var kdto = new KupacDTO()   //ručno presipavanje
                {
                    Sifra = k.Sifra,
                    KorisnickoIme = k.KorisnickoIme,
                    Ime = k.Ime,
                    Prezime = k.Prezime,
                    Lozinka = k.Lozinka,
                    Telefon = k.Telefon,
                    Adresa = k.Adresa
                };
                vratiKupac.Add(kdto);
            });

            return Ok(vratiKupac);
        }

        [HttpPost]
        public IActionResult Post(KupacDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Kupac k = new Kupac()
                {
                    KorisnickoIme = dto.KorisnickoIme,
                    Ime = dto.Ime,
                    Prezime = dto.Prezime,
                    Lozinka = dto.Lozinka,
                    Telefon = dto.Telefon,
                    Adresa = dto.Adresa
                };

                _context.Kupac.Add(k);
                _context.SaveChanges();
                dto.Sifra = k.Sifra;
                return Ok(k);
            }
            catch (Exception k)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, k.Message);
            }
        }

        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult Put(int sifra, KupacDTO kdto)
        {
            if (sifra <= 0 || kdto == null)
            {
                return BadRequest();
            }
            try
            {
                var KupacBaza = _context.Kupac.Find(sifra);
                if (KupacBaza == null)
                {
                    return BadRequest();
                }
                KupacBaza.KorisnickoIme = kdto.KorisnickoIme;
                KupacBaza.Ime = kdto.Ime;
                KupacBaza.Prezime = kdto.Prezime;
                KupacBaza.Lozinka = kdto.Lozinka;
                KupacBaza.Telefon= kdto.Telefon;
                KupacBaza.Adresa = kdto.Adresa;

                _context.Kupac.Update(KupacBaza);
                _context.SaveChanges();

                kdto.Sifra=KupacBaza.Sifra;

                return StatusCode(StatusCodes.Status200OK, KupacBaza);
            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, x.Message);
            }
        }

        [HttpDelete]
        [Route("{sifra:int}")]
        [Produces("application/json")]
        public IActionResult Delete(int sifra)
        {
            if (sifra <= 0)
            {
                return BadRequest();
            }
            try
            {
                var KupacBaza = _context.Kupac.Find(sifra);
                if (KupacBaza == null)
                {
                    return BadRequest();
                }
                _context.Kupac.Remove(KupacBaza);
                _context.SaveChanges();

                return new JsonResult("Obrisano!");
            }
            catch (Exception k)
            {
                return new JsonResult("Ne može se obrisati!");
            }
        }

    }
}
