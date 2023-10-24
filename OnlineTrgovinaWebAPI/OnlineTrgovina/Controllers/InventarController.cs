using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineTrgovina.Data;
using OnlineTrgovina.Models;
using OnlineTrgovina.Models.DTO;
using System.Globalization;

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

        /// <summary>
        /// Dohvaća Inventare iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    GET api/v1/Inventar
        ///
        /// </remarks>
        /// <returns>Proizvodi u bazi</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 
        [HttpGet]   //Dohvaća šifru proizvoda u inventaru, naziv proizvoda, naziv kategorije, količinu te dostupnost
        public IActionResult Get()
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var inventari = _context.Inventar.Include(i => i.Proizvod).Include(p => p.Proizvod).ToList();

                if (inventari == null || inventari.Count == 0)
                {
                    return new EmptyResult();
                }

                List<InventarDTO> vrati = new();

                inventari.ForEach(i =>
                {
                    vrati.Add(new InventarDTO
                    {
                        Sifra = i.Sifra,
                        Proizvod = i.Proizvod?.Naziv,
                        Kategorija = i.Kategorija,
                        Kolicina = i.Kolicina,
                        Dostupnost = i.Dostupnost,
                        SifraProizvod = i.Proizvod.Sifra
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
        /// Dodaje Inventar u bazu
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    POST api/v1/Inventar
        ///    {sifra: 0, proizvod: "string", kategorija: "string", kolicina: 0, dostupnost: true, sifraProizvod: 0}
        ///    
        ///
        /// </remarks>
        /// <returns>Kreiran inventar u bazi sa svim podacima</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="400">Zahtjev nije valjan (BadRequest)</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 
        [HttpPost]  //dodavanje novog inventara
        public IActionResult Post(InventarDTO inventarDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (inventarDTO.SifraProizvod <= 0)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var proizvod = _context.Proizvod.Find(inventarDTO.SifraProizvod);

                if (proizvod == null)
                {
                    return BadRequest(ModelState);
                }

                Inventar i = new Inventar()
                {
                    Proizvod = proizvod,
                    Kategorija = inventarDTO.Kategorija,
                    Kolicina = inventarDTO.Kolicina,
                    Dostupnost = inventarDTO.Dostupnost
                };

                _context.Inventar.Add(i);
                _context.SaveChanges();

                inventarDTO.Sifra = i.Sifra;
                inventarDTO.Proizvod = proizvod.Naziv;

                return Ok(inventarDTO);
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
        ///    PUT api/v1/Inventar/{sifra}
        ///
        ///     {sifra: 0, proizvod: "string", kategorija: "string", kolicina: 0, dostupnost: true, sifraProizvod: 0}
        /// 
        ///
        /// </remarks>
        /// <param name="sifra">Šifra inventara koji se mijenja</param>  
        /// <returns>Svi poslani podaci od inventara</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">U bazi nema inventara kojeg želimo promijeniti</response>
        /// <response code="415">Nismo poslali JSON</response> 
        /// <response code="503">Na azure treba dodati IP u firewall</response> 
        [HttpPut]       //promjena inventara
        [Route("{sifra:int}")]
        public IActionResult Put(int sifra, InventarDTO inventarDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (sifra <= 0 || inventarDTO == null)
            {
                return BadRequest();
            }

            try
            {
                var proizvod = _context.Proizvod.Find(inventarDTO.SifraProizvod);
                if (proizvod == null)
                {
                    return BadRequest();
                }

                var inventar = _context.Inventar.Find(sifra);
                if (inventar == null)
                {
                    return BadRequest();
                }

                inventar.Proizvod = proizvod;
                inventar.Kategorija = inventarDTO.Kategorija;
                inventar.Kolicina = inventarDTO.Kolicina;
                inventar.Dostupnost = inventarDTO.Dostupnost;

                _context.Inventar.Update(inventar);
                _context.SaveChanges();

                inventarDTO.Sifra = sifra;
                inventarDTO.Proizvod = proizvod.Naziv;

                return Ok(inventarDTO);
            }
            catch (Exception kk)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, kk.Message);
            }

        }

        /// <summary>
        /// Briše inventar iz baze
        /// </summary>
        /// <remarks>
        /// Primjer upita:
        ///
        ///    DELETE api/v1/Inventar/{sifra}
        ///    
        /// </remarks>
        /// <param name="sifra">Šifra inventara koji se briše</param>  
        /// <returns>Odgovor da li je obrisano ili ne</returns>
        /// <response code="200">Sve je u redu</response>
        /// <response code="204">U bazi nema inventara kojeg želimo obrisati</response>
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

            var inventarBaza = _context.Inventar.Find(sifra);
            if (inventarBaza == null)
            {
                return BadRequest();
            }

            try
            {
                _context.Inventar.Remove(inventarBaza);
                _context.SaveChanges();

                return new JsonResult("{\"poruka\":\"Obrisano\"}");
            }
            catch (Exception k)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Ne može se obrisati inventar zbog toga što ima na sebi proizvod!");
            }
        }


        //NEDOSTUPNO! Inventar sadrži samo vanjski ključ Proizvoda, ne listu, zbog toga se ne može izvesti dodavanje, brisanje i dohvaćanje proizvoda šifrom.

        ////"dohvaća opis proizvoda s odabranom šifrom"
        //[HttpGet]
        //[Route("{sifra:int}/proizvodi")]
        //public IActionResult GetProizvodi(int sifra)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }

        //    if (sifra <= 0)
        //    {
        //        return BadRequest();
        //    }

        //    try
        //    {
        //        var inventar = _context.Inventar.Include(i => i.Proizvod).FirstOrDefault(i => i.Sifra == sifra);   //inventar i svi njeni proizvodi

        //        if (inventar == null)
        //        {
        //            return BadRequest();
        //        }

        //        if (inventar.Proizvod == null)
        //        {
        //            return new EmptyResult();
        //        }

        //        List<ProizvodDTO> vrati = new();
        //        inventar.Proizvod.ForEach(p =>
        //        {
        //            vrati.Add(new ProizvodDTO
        //            {
        //                Sifra = p.Sifra,
        //                Naziv = p.Naziv,
        //                Opis = p.Opis,
        //                Cijena = p.Cijena
        //            });
        //        });
        //        return Ok(vrati);

        //    }
        //    catch (Exception kk)
        //    {
        //        return StatusCode(StatusCodes.Status503ServiceUnavailable, kk.Message);
        //    }

        //}

        ////dodavanje proizvoda u inventar
        //[HttpPost]
        //[Route("{sifra:int}/dodaj/{proizvodSifra:int}")]
        //public IActionResult DodajProizvod(int sifra, int proizvodSifra)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }

        //    if (sifra <= 0 || proizvodSifra <= 0)
        //    {
        //        return BadRequest();
        //    }

        //    try
        //    {
        //        var inventar = _context.Inventar.Include(i => i.Proizvod).FirstOrDefault(i => i.Sifra == sifra);

        //        if (inventar == null)
        //        {
        //            return BadRequest();
        //        }

        //        var proizvod = _context.Proizvod.Find(proizvodSifra);

        //        if (proizvod == null)
        //        {
        //            return BadRequest();
        //        }

        //        inventar.Proizvod.Add(proizvod);

        //        _context.Inventar.Update(inventar);
        //        _context.SaveChanges();

        //        return Ok();

        //    }
        //    catch (Exception kk)
        //    {
        //        return StatusCode(StatusCodes.Status503ServiceUnavailable, kk.Message);
        //    }

        //}

        ////Brisanje proizvoda iz inventara
        //[HttpDelete]
        //[Route("{sifra:int}/obrisi{proizvodSifra:int}")]
        //public IActionResult ObrišiProizvod(int sifra, int proizvodSifra)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }

        //    if (sifra <= 0 || proizvodSifra <= 0)
        //    {
        //        return BadRequest();
        //    }

        //    try
        //    {
        //        var kosarica = _context.Kosarica.Include(kk => kk.Proizvodi).FirstOrDefault(kk => kk.Sifra == sifra);

        //        if (kosarica == null)
        //        {
        //            return BadRequest();
        //        }

        //        var proizvod = _context.Proizvod.Find(proizvodSifra);

        //        if (proizvod == null)
        //        {
        //            return BadRequest();
        //        }

        //        kosarica.Proizvodi.Remove(proizvod);

        //        _context.Kosarica.Update(kosarica);
        //        _context.SaveChanges();

        //        return Ok();

        //    }
        //    catch (Exception kk)
        //    {
        //        return StatusCode(StatusCodes.Status503ServiceUnavailable, kk.Message);
        //    }
        //}

    }
}
