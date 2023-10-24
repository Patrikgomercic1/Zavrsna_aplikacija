using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineTrgovina.Data;
using OnlineTrgovina.Models;
using OnlineTrgovina.Models.DTO;

namespace OnlineTrgovina.Controllers
{
    /// <summary>
    /// Namjenjeno za CRUD nad Inventarom
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]

    public class InventarController : ControllerBase
    {
        private readonly OTContext _context;

        public InventarController(OTContext context)
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

            var inventar = _context.Inventar.Include(i => i.Proizvod).ToList();
            if (inventar == null || inventar.Count == 0)
            {
                return new EmptyResult();
            }

            List<InventarDTO> vratiInventar = new();

            inventar.ForEach(i =>
            {
                var idto = new InventarDTO()
                {
                    Sifra = i.Sifra,
                    Proizvod=i.Proizvod?.Naziv,
                    Kategorija = i.Kategorija,
                    Kolicina = i.Kolicina,
                    Dostupnost = i.Dostupnost 
                };
                vratiInventar.Add(idto);
            });

            return Ok(vratiInventar);
        }

        [HttpPost]//zamjeniti sa rutom proizvod{sifra}
        public IActionResult Post(InventarDTO inventarDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (inventarDTO.Proizvod.Count() <= 0)
            {
                return BadRequest(ModelState);
            }
            try
            {
                //var proizvod = _context.Proizvod.Find(inventarDTO.Proizvod);
                var proizvod = _context.Proizvod.Find(inventarDTO.SifraProizvod);
                if (proizvod == null)
                {
                    return BadRequest(ModelState);
                }

                Inventar i = new(); //??????
                {
                    i.Kategorija = inventarDTO.Kategorija;
                    i.Proizvod = proizvod;
                };

                _context.Inventar.Add(i);
                _context.SaveChanges();

                inventarDTO.Sifra = i.Sifra;
                inventarDTO.Proizvod = proizvod.Naziv;

                return Ok(inventarDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex);
            }
            
        }

    }
}
