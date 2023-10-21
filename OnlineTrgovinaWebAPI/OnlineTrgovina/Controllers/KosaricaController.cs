﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineTrgovina.Data;
using OnlineTrgovina.Models;
using OnlineTrgovina.Models.DTO;

namespace OnlineTrgovina.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]

    public class KosaricaController : ControllerBase
    {
        private readonly OTContext _context;
        private readonly ILogger<KosaricaController> _logger;

        public KosaricaController(OTContext context, ILogger<KosaricaController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Dohvaćam košaricu");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var kosarice = _context.Kosarica.Include(kk => kk.Kupac)
                    //.Include(kk => kk.Proizvod)
                    .ToList();
                if (kosarice == null)
                {
                    return new EmptyResult();
                }

                List<KosaricaDTO> vratiKosarica = new();

                kosarice.ForEach(kk =>
                {
                    vratiKosarica.Add(new KosaricaDTO()
                    {
                        Sifra = kk.Sifra,
                        Kupac = kk.Kupac.KorisnickoIme,
                        KupacSifra = kk.Kupac.Sifra,
                        DatumStvaranja = kk.DatumStvaranja,
                        //BrojProizvoda=kk.BrojProizvoda.Count()
                    });
                });

                return Ok(vratiKosarica);
            }
            catch (Exception kk)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, kk);
            }
        }

        public IActionResult Post(KosaricaDTO kosaricaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (kosaricaDTO.KupacSifra <= 0)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var kupac = _context.Kupac.Find(kosaricaDTO.KupacSifra);

                if (kupac == null)
                {
                    return BadRequest(ModelState);
                }

                Kosarica kk = new()
                {
                    DatumStvaranja = kosaricaDTO.DatumStvaranja,
                    Kupac = kupac,
                    //BrojProizvoda = kosaricaDTO.BrojProizvoda
                };

                _context.Kosarica.Add(kk);
                _context.SaveChanges();

                kosaricaDTO.Sifra = kk.Sifra;

                return Ok(kosaricaDTO);

            }
            catch (Exception kk)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, kk);
            }
        }

        [HttpPut]
        [Route("{sifra:int}")]
        public IActionResult Put(int sifra, KosaricaDTO kosaricaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (sifra <= 0 || kosaricaDTO == null)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var kupac = _context.Kupac.Find(kosaricaDTO.KupacSifra);

                if (kupac == null)
                {
                    return BadRequest(ModelState);
                }

                var kosarica = _context.Kosarica.Find(sifra);

                if (kosarica == null)
                {
                    return BadRequest(ModelState);
                }

                kosarica.DatumStvaranja = kosaricaDTO.DatumStvaranja;
                kosarica.Kupac = kupac;
                //kosarica.BrojProizvoda = kosaricaDTO.BrojProizvoda;

                _context.Kosarica.Update(kosarica);
                _context.SaveChanges();

                kosaricaDTO.Sifra = sifra;
                kosaricaDTO.Kupac = kupac.KorisnickoIme;

                return Ok(kosaricaDTO);

            }
            catch (Exception kk)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, kk);
            }
        }

        [HttpDelete]
        [Route("{sifra:int}")]
        [Produces("application/json")]
        public IActionResult Delete(int sifra)
        {
            if (sifra <= 0)
            {
                return BadRequest();
            }
            try
            {
                var KosaricaBaza = _context.Kosarica.Find(sifra);
                if (KosaricaBaza == null)
                {
                    return BadRequest();
                }
                _context.Kosarica.Remove(KosaricaBaza);
                _context.SaveChanges();

                return new JsonResult("Obrisano!");
            }
            catch (Exception kk)
            {
                return new JsonResult("Ne može se obrisati!");
            }
        }

        //dodavanje proizvoda
        [HttpGet]
        [Route("sifra:int/proizvodi")]
        public IActionResult GetProizvode(int sifra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (sifra <= 0)
            {
                return BadRequest();
            }

            try
            {
                var kosarica = _context.Kosarica
                    .Include(kk => kk.Proizvodi) //dodati proizvod u context
                    .FirstOrDefault(kk => kk.sifra == sifra);

                if (kosarica == null)
                {
                    return BadRequest();
                }

                if (kosarica.Proizvodi == null || kosarica.Proizvodi.Count == 0)
                {
                    return new EmptyResult();
                }

                List<ProizvodDTO> vrati = new();
                kosarica.Proizvodi.ForEach(p =>
                {
                    vrati.Add(new ProizvodDTO()
                    {
                        Sifra = p.Sifra,
                        Naziv = p.Naziv,
                        Opis = p.Opis,
                        Cijena = p.Cijena
                    });
                });

                return Ok(vrati);

            }
            catch (Exception kp)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, kp);
            }
        }

        [HttpPost]
        [Route("sifra:int/dodavanje/{proizvodSifra:int}")]
        public IActionResult DodajProizvode(int sifra, int proizvodSifra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (sifra <= 0 || proizvodSifra <= 0)
            {
                return BadRequest();
            }

            try
            {
                var kosarica = _context.Kosarica
                    .Include(kk => kk.Proizvodi) //dodati proizvod u context
                    .FirstOrDefault(kk => kk.sifra == sifra);

                if (kosarica == null)
                {
                    return BadRequest();
                }

                var proizvod =_context.Proizvod.Find(proizvodSifra);

                if(proizvod == null)
                {
                    return BadRequest();
                }

                kosarica.Proizvodi.Add(proizvod); //kombinirati s brojem proizvoda u košarici

                _context.Kosarica.Update(kosarica);
                _context.SaveChanges();

                return Ok();

            }
            catch (Exception p)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, p);
            }
        }

        [HttpDelete]
        [Route("sifra:int/brisanje/{proizvodSifra:int}")]
        public IActionResult BrisiProizvode(int sifra, int proizvodSifra)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (sifra <= 0 || proizvodSifra <= 0)
            {
                return BadRequest();
            }

            try
            {
                var kosarica = _context.Kosarica
                    .Include(kk => kk.Proizvodi) //dodati proizvod u context
                    .FirstOrDefault(kk => kk.sifra == sifra);

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
            catch (Exception p)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, p);
            }
        }
    }
}