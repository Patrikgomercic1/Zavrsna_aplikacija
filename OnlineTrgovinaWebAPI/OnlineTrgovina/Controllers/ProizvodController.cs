using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using OnlineTrgovina.Data;
using OnlineTrgovina.Models;

namespace OnlineTrgovina.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]

    public class ProizvodController : ControllerBase
    {
        private readonly OTContext _context;

        public ProizvodController(OTContext context)
        {
            _context = context;
        }

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

        [HttpPost]
        public IActionResult Post(Proizvod proizvod)
        {
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
                ProizvodBaza.Kolicina = proizvod.Kolicina;
                ProizvodBaza.Dostupnost = proizvod.Dostupnost;
                _context.Proizvod.Update(ProizvodBaza);
                _context.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, ProizvodBaza);
            }
            catch (Exception x)     //kasnije maknuti instancu x
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, x.Message);
            }           
        }

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
            catch (Exception ex)
            {
                return new JsonResult("Ne može se obrisati!");
            }
        }

    }
}
