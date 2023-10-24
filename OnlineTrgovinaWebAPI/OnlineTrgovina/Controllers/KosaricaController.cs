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
                var kosarica = _context.Kosarica.Include(kk => kk.Kupac)
                    .Include(kk => kk.Proizvodi)
                    .ToList();
                if (kosarica == null || kosarica.Count == 0)
                {
                    return new EmptyResult();
                }

                List<KosaricaDTO> vratiKosarica = new();

                kosarica.ForEach(kk =>
                {
                    var kdto = new KosaricaDTO()
                    {
                        Sifra = kk.Sifra,
                        Kupac = kk.Kupac.KorisnickoIme,
                        //Dodati prikaz naziva proizvoda
                        KolicinaProizvod =kk.KolicinaProizvod,
                        DatumStvaranja = kk.DatumStvaranja

                    };
                    vratiKosarica.Add(kdto);
                });

                return Ok(vratiKosarica);
            }
            catch (Exception kk)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, kk);
            }
        }

        
    }
}
