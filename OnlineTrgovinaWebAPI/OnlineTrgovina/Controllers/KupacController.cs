using Microsoft.AspNetCore.Mvc;
using OnlineTrgovina.Data;
using OnlineTrgovina.Models;
using OnlineTrgovina.Models.DTO;
//using OnlineTrgovina.Models.DTO;

namespace OnlineTrgovina.Controllers
{
    /// <summary>
    /// Namjenjeno za CRUD operacije nad Kupcima
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]

    public class KupacController : ControllerBase
    {
        private readonly OTContext _context;

        public KupacController(OTContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Dohvaća sve kupce iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    GET api/v1/Kupac
        ///
        /// </remarks>
        /// <returns>Kupci u bazi</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 
        [HttpGet]
        public IActionResult Get() 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var kupci=_context.Kupac.ToList();
                if(kupci == null || kupci.Count == 0)
                {
                    return new EmptyResult();
                }
                return new JsonResult(_context.Kupac.ToList());
            }
            catch (Exception k)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, k.Message);
            }
        }

        /// <summary>
        /// Traži kupca po njegovoj šifri
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///     
        ///     GET /api/v1/kupac/{sifra}
        /// 
        /// </remarks>
        /// <param name="sifra">Šifra kupca</param>
        /// <returns>Kupac u bazi</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 
        [HttpGet]   //Traženje po šifri
        [Route("{sifra:int}")]
        public IActionResult GetBySifra(int sifra) 
        {
            if (sifra <= 0)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var k = _context.Kupac.Find(sifra);

                if (k == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, k);
                }

                return new JsonResult(k);

            }
            catch (Exception k)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, k.Message);
            }
        }


        /// <summary>
        /// Dodaje kupca u bazu
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    POST api/v1/Kupac
        ///    
        /// {sifra: 0, korisnickoIme: "string", ime: "string", prezime: "string", lozinka: "string", telefon: "string", adresa: "string"}
        /// 
        /// 
        /// </remarks>
        /// <returns>Kreirani kupac u bazi sa svim podacima</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 
        [HttpPost]      //DODAJE KUPCA
        public IActionResult Post(Kupac kupac)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Kupac.Add(kupac);
                _context.SaveChanges();

                return StatusCode(StatusCodes.Status201Created, kupac);
            }
            catch (Exception k)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, k.Message);
            }
        }

        /// <summary>
        /// Mijenja podatke postojećeg kupca u bazi
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    PUT api/v1/Kupac/{sifra}
        ///
        /// {sifra: 0, korisnickoIme: "string", ime: "string", prezime: "string", lozinka: "string", telefon: "string", adresa: "string"}
        /// 
        ///
        /// </remarks>
        /// <param name="sifra">Šifra kupca koji se mijenja</param>  
        /// <returns>Svi poslani podaci od proizvoda</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">U bazi nema kupca kojeg želimo promijeniti</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 
        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult Put (int sifra, Kupac kupac)
        {
            if(sifra <= 0 || kupac == null)
            {
                return BadRequest();
            }

            try
            {
                var kupacBaza = _context.Kupac.Find(sifra);
                if(kupacBaza == null)
                {
                    return BadRequest();
                }

                kupacBaza.KorisnickoIme = kupac.KorisnickoIme;
                kupacBaza.Ime = kupac.Ime;
                kupacBaza.Prezime = kupac.Prezime;
                kupacBaza.Lozinka = kupac.Lozinka;
                kupacBaza.Telefon = kupac.Telefon;
                kupacBaza.Adresa = kupac.Adresa;

                _context.Kupac.Update(kupacBaza);
                _context.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, kupacBaza);
            }
            catch (Exception k)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, k.Message);
            }
        }


        /// <summary>
        /// Briše kupca iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    DELETE api/v1/Kupac/{sifra}
        ///    
        /// </remarks>
        /// <param name="sifra">Šifra kupca koji se briše</param>  
        /// <returns>Odgovor da li je obrisano ili ne</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">U bazi nema kupca kojeg želimo obrisati</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 
        [HttpDelete]
        [Route("{sifra:int}")]
        [Produces("application/json")]
        public IActionResult Delete(int sifra)
        {
            if(sifra <= 0)
            {
                return BadRequest();
            }

            var kupacBaza=_context.Kupac.Find(sifra);
            if(kupacBaza == null)
            {
                return BadRequest();
            }

            try
            {
                _context.Kupac.Remove(kupacBaza);
                _context.SaveChanges();

                return new JsonResult("{\"poruka\":\"Obrisano\"}");
            }
            catch (Exception k)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Ne može se obrisati kupac zbog toga što ima na sebi košaricu!");
            }
        }

    }
}
