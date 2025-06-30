using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class MatrixIncDbContext : DbContext
    {
        public MatrixIncDbContext(DbContextOptions<MatrixIncDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Review> Reviews { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relaties
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId)
                .IsRequired();

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId);

            // Seeding van producten
            modelBuilder.Entity<Product>().HasData(
              new Product { Id = 201, Name = "Momentsleutel", Description = "Precisiegereedschap voor het nauwkeurig aandraaien van bouten en moeren tot het juiste draaimoment.", Price = 75m, ImageUrl = "/images/products/momentsleutel.jpg" },
              new Product { Id = 202, Name = "Acculader", Description = "Efficiënte acculader voor het snel en veilig opladen van 12V autobatterijen.", Price = 60m, ImageUrl = "/images/products/acculader.jpg" },
              new Product { Id = 203, Name = "Autolift", Description = "Professionele hydraulische autolift geschikt voor garages en monteurs.", Price = 1500m, ImageUrl = "/images/products/autolift.jpg" },
              new Product { Id = 204, Name = "Compressietester", Description = "Meet eenvoudig de compressie van cilinders in benzine- en dieselmotoren.", Price = 120m, ImageUrl = "/images/products/compressietester.jpg" },
              new Product { Id = 205, Name = "Demontageset", Description = "Complete set speciaal gereedschap voor het zorgvuldig demonteren van interieur- en motoronderdelen.", Price = 45m, ImageUrl = "/images/products/demontageset.jpg" },
              new Product { Id = 206, Name = "Diagnoseapparaat", Description = "OBD2-scanapparaat voor het uitlezen van voertuigstoringen en foutcodes.", Price = 200m, ImageUrl = "/images/products/diagnoseapparaat.jpg" },
              new Product { Id = 207, Name = "Schroefextractor", Description = "Praktische tool om afgebroken of beschadigde schroeven eenvoudig te verwijderen.", Price = 30m, ImageUrl = "/images/products/schroefextractor.jpg" },
              new Product { Id = 208, Name = "Olieopvangbak", Description = "Grote opvangbak voor het veilig afvoeren van afgewerkte motorolie tijdens onderhoud.", Price = 25m, ImageUrl = "/images/products/olieopvangbak.jpg" },
              new Product { Id = 209, Name = "Wieluitlijner", Description = "Professioneel uitlijnapparaat voor het afstellen van wielstanden volgens fabrieksspecificaties.", Price = 3000m, ImageUrl = "/images/products/wieluitlijner.jpg" },
              new Product { Id = 210, Name = "Remmenontluchter", Description = "Apparaat om het remsysteem snel en luchtvrij te ontluchten.", Price = 40m, ImageUrl = "/images/products/remmenontluchter.jpg" }
);


            modelBuilder.Entity<Part>().HasData(
                new Part { Id = 101, Name = "Tandwiel", Description = "Duurzaam tandwiel van gehard staal voor betrouwbare krachtoverbrenging.", Price = 15.50m, ImageUrl = "/images/parts/tandwiel.jpg" },
                new Part { Id = 102, Name = "M5 Boutje", Description = "RVS M5 boutje met fijne draad, geschikt voor precisietoepassingen.", Price = 0.10m, ImageUrl = "/images/parts/m5_boutje.jpg" },
                new Part { Id = 103, Name = "Hydraulische Cilinder", Description = "Hydraulische cilinder van 10 cm slag, perfect voor hef- en druktoepassingen.", Price = 120m, ImageUrl = "/images/parts/hydraulische_cilinder.jpg" },
                new Part { Id = 104, Name = "Koelvloeistof Pomp", Description = "Efficiënte pomp voor circulatie van koelvloeistof in motoren en industriële toepassingen.", Price = 85m, ImageUrl = "/images/parts/koelvloeistof_pomp.jpg" },
                new Part { Id = 105, Name = "Oliefilter", Description = "Hoogwaardige oliefilter voor optimale bescherming van de motor tegen vuil en slijtage.", Price = 10m, ImageUrl = "/images/parts/oliefilter.jpg" },
                new Part { Id = 106, Name = "Remblokken", Description = "Set hoogwaardige remblokken voor maximale remkracht en veiligheid.", Price = 45m, ImageUrl = "/images/parts/remblokken.jpg" },
                new Part { Id = 107, Name = "Bougie", Description = "Betrouwbare bougie voor efficiënte ontsteking en brandstofverbranding in benzinemotoren.", Price = 8m, ImageUrl = "/images/parts/bougie.jpg" },
                new Part { Id = 108, Name = "Radiateurslang", Description = "Slijtvaste rubberen slang voor betrouwbare koelvloeistofcirculatie in radiateursystemen.", Price = 15m, ImageUrl = "/images/parts/radiateurslang.jpg" },
                new Part { Id = 109, Name = "Brandstoffilter", Description = "Fijnmazige brandstoffilter voor schone toevoer en bescherming van de brandstofinjectie.", Price = 18m, ImageUrl = "/images/parts/brandstoffilter.jpg" },
                new Part { Id = 110, Name = "Koppeling", Description = "Complete koppeling set voor soepele krachtoverbrenging tussen motor en aandrijving.", Price = 120m, ImageUrl = "/images/parts/koppeling.jpg" }
            

);


            base.OnModelCreating(modelBuilder);
        }
    }
}
