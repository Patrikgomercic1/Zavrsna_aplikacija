using Microsoft.AspNetCore.Mvc;
using OnlineTrgovina.Data;
using OnlineTrgovina.Models;
using OnlineTrgovina.Models.DTO;

namespace OnlineTrgovina.Controllers
{
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

            var inventar = _context.Inventar.ToList();
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
                    Kategorija = i.Kategorija,
                    Kolicina = i.Kolicina,
                    Dostupnost = i.Dostupnost,
                    SifraProizvod = i.Proizvod  //null-nadopuniti
                };
                vratiInventar.Add(idto);
            });

            return Ok(vratiInventar);
        }


    }
}
