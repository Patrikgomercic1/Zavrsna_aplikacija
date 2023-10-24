using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineTrgovina.Data;
using OnlineTrgovina.Models;
using OnlineTrgovina.Models.DTO;

namespace OnlineTrgovina.Controllers
{
    /// <summary>
    /// Namjenjeno za CRUD operacije nad Košaricom
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]

    public class KosaricaController : ControllerBase
    {
        //Interface Logger za prikaz čega se događa kada se pozove Košarica
        private readonly OTContext _context;
        private readonly ILogger<KosaricaController> _logger;

        public KosaricaController(OTContext context, ILogger<KosaricaController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]   //Dohvaća šifru košarice, zadanog kupca, količinu proizvoda i datum stvaranja
        public IActionResult Get()
        {
            _logger.LogInformation("Dohvaćam košaricu");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var kosarice = _context.Kosarica.Include(k => k.Kupac).Include(p => p.Proizvodi).ToList();

                if (kosarice == null || kosarice.Count == 0)
                {
                    return new EmptyResult();
                }

                List<KosaricaDTO> vrati = new();

                kosarice.ForEach(kk =>
                {
                    vrati.Add(new KosaricaDTO
                    {
                        Sifra = kk.Sifra,
                        Kupac = kk.Kupac?.KorisnickoIme,
                        SifraKupac = kk.Kupac.Sifra,
                        KolicinaProizvod = kk.KolicinaProizvod,
                        DatumStvaranja = kk.DatumStvaranja,
                        Proizvodi = kk.Proizvodi.Count    //Koliko individualnih proizvoda ima u košarici
                    });
                });

                return Ok(vrati);
            }
            catch (Exception kk)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, kk.Message);
            }
        }

        [HttpPost]
        public IActionResult Post(KosaricaDTO kosaricaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (kosaricaDTO.SifraKupac <= 0)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var kupac = _context.Kupac.Find(kosaricaDTO.SifraKupac);

                if (kupac == null)
                {
                    return BadRequest(ModelState);
                }

                Kosarica kk = new Kosarica()
                {
                    Kupac = kupac,
                    KolicinaProizvod = kosaricaDTO.KolicinaProizvod,
                    DatumStvaranja = kosaricaDTO.DatumStvaranja
                };

                _context.Kosarica.Add(kk);
                _context.SaveChanges();

                kosaricaDTO.Sifra = kk.Sifra;
                kosaricaDTO.Kupac = kupac.KorisnickoIme;

                return Ok(kosaricaDTO);
            }
            catch (Exception kk)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, kk.Message);
            }
        }

        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult Put(int sifra, KosaricaDTO kosaricaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (sifra <= 0 || kosaricaDTO == null)
            {
                return BadRequest();
            }

            try
            {
                var kupac = _context.Kupac.Find(kosaricaDTO.SifraKupac);
                if (kupac == null)
                {
                    return BadRequest();
                }

                var kosarica = _context.Kosarica.Find(sifra);
                if (kosarica == null)
                {
                    return BadRequest();
                }

                kosarica.Kupac = kupac;
                kosarica.KolicinaProizvod = kosaricaDTO.KolicinaProizvod;
                kosarica.DatumStvaranja = kosaricaDTO.DatumStvaranja;

                _context.Kosarica.Update(kosarica);
                _context.SaveChanges();

                kosaricaDTO.Sifra = sifra;
                kosaricaDTO.Kupac = kupac.KorisnickoIme;

                return Ok(kosaricaDTO);
            }
            catch (Exception kk)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, kk.Message);
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

            var kosaricaBaza = _context.Kosarica.Find(sifra);
            if (kosaricaBaza == null)
            {
                return BadRequest();
            }

            try
            {
                _context.Kosarica.Remove(kosaricaBaza);
                _context.SaveChanges();

                return new JsonResult("{\"poruka\":\"Obrisano\"}");
            }
            catch (Exception k)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Ne može se obrisati košarica zbog toga što ima na sebi kupca!");
            }
        }

        //"dohvaća opis proizvoda s odabranom šifrom"
        [HttpGet]
        [Route("{sifra:int}/proizvodi")]
        public IActionResult GetProizvodi(int sifra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (sifra <= 0)
            {
                return BadRequest();
            }

            try
            {
                var kosarica = _context.Kosarica.Include(kk => kk.Proizvodi).FirstOrDefault(kk => kk.Sifra == sifra);   //košarica i svi njeni proizvodi

                if(kosarica == null)
                {
                    return BadRequest();
                }

                if(kosarica.Proizvodi.Count == 0 || kosarica.Proizvodi == null)
                {
                    return new EmptyResult();
                }

                List<ProizvodDTO> vrati = new();
                kosarica.Proizvodi.ForEach(p =>
                {
                    vrati.Add(new ProizvodDTO
                    {
                        Sifra=p.Sifra,
                        Naziv=p.Naziv,
                        Opis=p.Opis,
                        Cijena=p.Cijena
                    });
                });
                return Ok(vrati);

            }
            catch (Exception kk)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, kk.Message);
            }

        }

        //dodavanje proizvoda na košaricu
        [HttpPost]
        [Route("{sifra:int}/dodaj/{proizvodSifra:int}")]
        public IActionResult DodajProizvod(int sifra, int proizvodSifra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (sifra <= 0 || proizvodSifra <=0)
            {
                return BadRequest();
            }

            try
            {
                var kosarica = _context.Kosarica.Include(kk => kk.Proizvodi).FirstOrDefault(kk => kk.Sifra == sifra);   

                if (kosarica == null)
                {
                    return BadRequest();
                }

                var proizvod = _context.Proizvod.Find(proizvodSifra);

                if(proizvod == null)
                {
                    return BadRequest();
                }

                kosarica.Proizvodi.Add(proizvod);

                _context.Kosarica.Update(kosarica);
                _context.SaveChanges();

                return Ok();

            }
            catch (Exception kk)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, kk.Message);
            }

        }

        //Brisanje proizvoda iz košarice
        [HttpDelete]
        [Route("{sifra:int}/obrisi{proizvodSifra:int}")]
        public IActionResult ObrišiProizvod(int sifra, int proizvodSifra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (sifra <= 0 || proizvodSifra <= 0)
            {
                return BadRequest();
            }

            try
            {
                var kosarica = _context.Kosarica.Include(kk => kk.Proizvodi).FirstOrDefault(kk => kk.Sifra == sifra);

                if (kosarica == null)
                {
                    return BadRequest();
                }

                var proizvod = _context.Proizvod.Find(proizvodSifra);

                if (proizvod == null)
                {
                    return BadRequest();
                }

                kosarica.Proizvodi.Remove(proizvod);

                _context.Kosarica.Update(kosarica);
                _context.SaveChanges();

                return Ok();

            }
            catch (Exception kk)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, kk.Message);
            }
        }

    }
}
