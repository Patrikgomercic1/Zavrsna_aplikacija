using Microsoft.EntityFrameworkCore;
using OnlineTrgovina.Models;

namespace OnlineTrgovina.Data
{
    public class OTContext : DbContext
    {
        public OTContext(DbContextOptions<OTContext> opcije) : base(opcije)
        {
            
        }

        public DbSet<Proizvod> Proizvod { get; set; }

    }
}
