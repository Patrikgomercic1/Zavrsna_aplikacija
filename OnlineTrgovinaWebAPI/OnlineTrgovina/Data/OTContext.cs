using OnlineTrgovina.Models;
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

 
 

        //ORM
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //implementacija veze 1:n - Košarica ima jednog Kupca
            modelBuilder.Entity<Kosarica>().HasOne(kk => kk.Kupac);

            //implementacija veze n:n - više Košarica ima više Proizvoda           
            modelBuilder.Entity<Kosarica>()
                .HasMany(kk => kk.Proizvodi)
                .WithMany(p => p.Kosarice)
                .UsingEntity<Dictionary<string, object>>("kosaricaProizvod",
                kP => kP.HasOne<Proizvod>().WithMany().HasForeignKey("proizvod"),
                kP => kP.HasOne<Kosarica>().WithMany().HasForeignKey("kosarica"),
                kP => kP.ToTable("kosaricaProizvod")
                );


        }

    }
}
