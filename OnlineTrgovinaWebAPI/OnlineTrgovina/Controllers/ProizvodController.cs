using Microsoft.AspNetCore.Mvc;
using OnlineTrgovina.Models;

namespace OnlineTrgovina.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]

    public class ProizvodController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var lista = new List<Proizvod>() 
            { 
                new (){Naziv="Test1"},
                new (){Naziv="Test2"}
            };
            return new JsonResult(lista);
        }

        [HttpPost]
        public IActionResult Post(Proizvod proizvod)
        {
            return Created("/api/v1/Proizvod",proizvod);
        }

        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult Put(int sifra,Proizvod proizvod)
        {
            return StatusCode(StatusCodes.Status200OK, proizvod);
        }

        [HttpDelete]
        [Route("{sifra:int}")]
        public IActionResult Delete(int sifra)
        {
            return StatusCode(StatusCodes.Status200OK, "Obrisano!");
        }

    }
}
