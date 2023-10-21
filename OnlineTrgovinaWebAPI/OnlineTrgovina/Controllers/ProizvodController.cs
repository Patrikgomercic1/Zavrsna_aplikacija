using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using OnlineTrgovina.Data;
using OnlineTrgovina.Models;

namespace OnlineTrgovina.Controllers
{
    /// <summary>
    /// Namjenjeno za CRUD operacije nad Proizvodima
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    
    public class ProizvodController : ControllerBase
    {
        private readonly OTContext _context;

        public ProizvodController(OTContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Dohvaća sve proizvode iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    GET api/v1/Proizvod
        ///
        /// </remarks>
        /// <returns>Proizvodi u bazi</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 
        [HttpGet]   
        public IActionResult Get()
        {
            //Upit za slučaj lošeg upita
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var proizvodi = _context.Proizvod.ToList();
                if(proizvodi == null || proizvodi.Count == 0)
                {
                    return new EmptyResult();
                }
                return new JsonResult(_context.Proizvod.ToList());
            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, x.Message);
            }
        }

        /// <summary>
        /// Dodaje proizvod u bazu
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    POST api/v1/Proizvod
        ///    {Naziv:"",Opis:"",Cijena=""}
        ///
        /// </remarks>
        /// <returns>Kreirani proizvod u bazi sa svim podacima</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 
        [HttpPost]
        public IActionResult Post(Proizvod proizvod)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _context.Proizvod.Add(proizvod);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, proizvod);
            }
            catch (Exception x)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, x.Message);
            }
           

        }

        /// <summary>
        /// Mijenja podatke postojećeg proizvoda u bazi
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    PUT api/v1/Polaznik/{sifra}
        ///
        /// {
        ///   "sifra": 0,
        ///   "naziv": "string",
        ///   "opis": "string",
        ///   "cijena": "10000"
        /// }
        ///
        /// </remarks>
        /// <param name="sifra">Šifra proizvoda koji se mijenja</param>  
        /// <returns>Svi poslani podaci od proizvoda</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">U bazi nema proizvoda kojeg želimo promijeniti</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 
        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult Put(int sifra,Proizvod proizvod)
        {
            if(sifra <= 0 || proizvod == null)
            {
                return BadRequest();
            }
            try
            {
                var ProizvodBaza=_context.Proizvod.Find(sifra);
                if(ProizvodBaza == null)
                {
                    return BadRequest();
                }
                ProizvodBaza.Naziv = proizvod.Naziv;
                ProizvodBaza.Opis = proizvod.Opis;
                ProizvodBaza.Cijena = proizvod.Cijena;
                _context.Proizvod.Update(ProizvodBaza);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, ProizvodBaza);
            }
            catch (Exception x)     
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, x.Message);
            }           
        }

        /// <summary>
        /// Briše proizvod iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    DELETE api/v1/Polaznik/{sifra}
        ///    
        /// </remarks>
        /// <param name="sifra">Šifra proizvoda koji se briše</param>  
        /// <returns>Odgovor da li je obrisano ili ne</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">U bazi nema proizvoda kojeg želimo obrisati</response>
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
            try
            {
                var ProizvodBaza = _context.Proizvod.Find(sifra);
                if(ProizvodBaza == null)
                {
                    return BadRequest();
                }
                _context.Proizvod.Remove(ProizvodBaza);
                _context.SaveChanges();
                return new JsonResult("Obrisano!");
            }
            catch (Exception x)
            {
                return new JsonResult("Ne može se obrisati!");
            }
        }

    }
}
