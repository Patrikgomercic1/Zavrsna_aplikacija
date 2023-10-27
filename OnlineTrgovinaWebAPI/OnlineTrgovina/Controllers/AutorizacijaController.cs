
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using OnlineTrgovina.Data;
using OnlineTrgovina.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using EdunovaApp.Models;

namespace EdunovaAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AutorizacijaController : ControllerBase
    {

        private readonly OTContext _context;


        public AutorizacijaController(OTContext context)
        {
            _context = context;
        }



       
	}
}

