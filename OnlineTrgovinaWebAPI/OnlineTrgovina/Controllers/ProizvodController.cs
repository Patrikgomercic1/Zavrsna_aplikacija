using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using OnlineTrgovina.Data;
using OnlineTrgovina.Models;
using OnlineTrgovina.Models.DTO;

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
            //Ručno presipavanje vrijednosti u DTO
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

                List<ProizvodDTO> vrati = new();

                proizvodi.ForEach(p =>
                {
                    var pdto = new ProizvodDTO()
                    {
                        Sifra = p.Sifra,
                        Naziv=p.Naziv,
                        Opis = p.Opis,
                        Cijena = p.Cijena
                    };
                    vrati.Add(pdto);
                });

                return Ok(vrati);
            }
            catch (Exception p)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, p.Message);
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
        public IActionResult Post(ProizvodDTO dto)
        {   
            //Presipavanje vrijednosti iz DTO - kasnije će se kroz automapper
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
               Proizvod p = new Proizvod();
                {
                    p.Naziv = dto.Naziv;
                    p.Opis = dto.Opis;
                    p.Cijena = dto.Cijena;
                };

                _context.Proizvod.Add(p);
                _context.SaveChanges();

                dto.Sifra = p.Sifra;

                return Ok(dto);
            }
            catch (Exception p)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, p.Message);
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
        public IActionResult Put(int sifra, ProizvodDTO pdto)
        {
            if (sifra <= 0 || pdto == null)
            {
                return BadRequest();
            }

            try
            {
                var proizvodBaza = _context.Proizvod.Find(sifra);
                if (proizvodBaza == null)
                {
                    return BadRequest();
                }

                proizvodBaza.Naziv = pdto.Naziv;
                proizvodBaza.Opis = pdto.Opis;
                proizvodBaza.Cijena = pdto.Cijena;

                _context.Proizvod.Update(proizvodBaza);
                _context.SaveChanges();

                pdto.Sifra = proizvodBaza.Sifra;
                return StatusCode(StatusCodes.Status200OK, proizvodBaza);

            }
            catch (Exception k)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, k);
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
            var proizvodBaza = _context.Proizvod.Find(sifra);

            if(proizvodBaza == null)
            {
                return BadRequest();
            }

            try
            {
                _context.Proizvod.Remove(proizvodBaza);
                _context.SaveChanges();

                return new JsonResult("{ \"poruka\":\"Obrisano!\"}");
            }
            catch (Exception x)
            {
                return new JsonResult("{ \"poruka\":\"Ne može se obrisati!\"}");
            }
            
        }

    }
}
