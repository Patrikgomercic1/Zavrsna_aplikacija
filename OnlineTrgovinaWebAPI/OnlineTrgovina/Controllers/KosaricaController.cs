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

        /// <summary>
        /// Dohvaća sve Košarice iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    GET api/v1/Kosarica
        ///
        /// </remarks>
        /// <returns>Proizvodi u bazi</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 
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

        /// <summary>
        /// Dodaje Košaricu u bazu
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    POST api/v1/Kosarica
        ///    {Naziv:"",Opis:"",Cijena=""}
        ///
        /// </remarks>
        /// <returns>Kreirana košarica u bazi sa svim podacima</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 
        [HttpPost]  //dodavanje nove košarice
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

        /// <summary>
        /// Mijenja podatke postojeće košarice u bazi
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    PUT api/v1/Kosarica/{sifra}
        ///
        /// {sifra: 0, kupac: "string", kolicinaProizvod: 0, datumStvaranja: "2023-10-24T16:36:54.559Z", proizvodi: 0, sifraKupac: 0}
        /// 
        ///
        /// </remarks>
        /// <param name="sifra">Šifra košarice koja se mijenja</param>  
        /// <returns>Svi poslani podaci od košarice</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">U bazi nema košarice koju želimo promijeniti</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 
        [HttpPut]       //promjena košarice
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

        /// <summary>
        /// Briše košaricu iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    DELETE api/v1/Kosarica/{sifra}
        ///    
        /// </remarks>
        /// <param name="sifra">Šifra košarice koja se briše</param>  
        /// <returns>Odgovor da li je obrisano ili ne</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">U bazi nema košarice koju želimo obrisati</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 
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

        /// <summary>
        /// Dohvaća opis proizvoda s odabranom šifrom
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    GET api/v1/Kosarica/{sifra}/proizvodi
        ///    
        /// </remarks>
        /// <param name="sifra">Šifra košarice koja se provjerava</param>  
        /// <returns>Odgovor da li je provjereno ili ne</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">U bazi nema proizvoda za pregled</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 
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

        /// <summary>
        /// Dodavaje proizvod na košaricu
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    POST api/v1/Kosarica/{sifra}/dodaj/{proizvodSifra}
        ///    
        /// </remarks>
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

        /// <summary>
        /// Brisanje proizvoda iz košarice
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    POST api/v1/Kosarica/{sifra}/obrisi/{proizvodSifra}
        /// 
        /// </remarks>
        /// <param name="sifra"></param>
        /// <param name="proizvodSifra"></param>
        /// <returns></returns>
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
