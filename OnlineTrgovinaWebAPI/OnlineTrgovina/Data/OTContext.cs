using Microsoft.EntityFrameworkCore;
using OnlineTrgovina.Models;

namespace OnlineTrgovina.Data
{
    public class OTContext : DbContext
    {
        public OTContext(DbContextOptions<OTContext> opcije) : base(opcije)
        {

        }

        public DbSet<Kupac> Kupac { get; set; }

        public DbSet<Kosarica> Kosarica { get; set; }

        public DbSet<Proizvod> Proizvod { get; set; }

        public DbSet<Inventar> Inventar { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //implementacija veze 1:n - Proizvod ima jedan Inventar -- PROBATI OBRNUTO
            //modelBuilder.Entity<Proizvod>().HasOne(p => p.Inventar);

            //implementacija veze 1:n - Kosarica ima jednog Kupca
            modelBuilder.Entity<Kosarica>().HasOne(kk => kk.Kupac);

            modelBuilder.Entity<Kosarica>().HasOne(kk => kk.Proizvod);

            //Inventar i Proizvod
            /*
            modelBuilder.Entity<Kosarica>().HasMany(kk => kk.Proizvod)
                .WithMany(p => p.Kosarica)
                .UsingEntity<Dictionary<string, object>>("kosaricaProizvod",
                kP => kP.HasOne<Proizvod>().WithMany().HasForeignKey("proizvod"),
                kP => kP.HasOne<Kosarica>().WithMany().HasForeignKey("kosarica"),
                kP => kP.ToTable("kosaricaProizvod")
                );
            */

        }
    
    }
}
