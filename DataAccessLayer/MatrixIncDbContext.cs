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
                new Product { Id = 201, Name = "Momentsleutel", Description = "Sleutel voor nauwkeurig aandraaien", Price = 75m, ImageUrl = "/images/products/momentsleutel.jpg" },
                new Product { Id = 202, Name = "Acculader", Description = "Lader voor autobatterijen", Price = 60m, ImageUrl = "/images/products/acculader.jpg" },
                new Product { Id = 203, Name = "Autolift", Description = "Hydraulische lift voor auto's", Price = 1500m, ImageUrl = "/images/products/autolift.jpg" },
                new Product { Id = 204, Name = "Compressietester", Description = "Testtool voor motorcompressie", Price = 120m, ImageUrl = "/images/products/compressietester.jpg" },
                new Product { Id = 205, Name = "Demontageset", Description = "Set gereedschap voor demonteren", Price = 45m, ImageUrl = "/images/products/demontageset.jpg" },
                new Product { Id = 206, Name = "Diagnoseapparaat", Description = "Elektronische diagnose scanner", Price = 200m, ImageUrl = "/images/products/diagnoseapparaat.jpg" },
                new Product { Id = 207, Name = "Schroefextractor", Description = "Tool om beschadigde schroeven te verwijderen", Price = 30m, ImageUrl = "/images/products/schroefextractor.jpg" },
                new Product { Id = 208, Name = "Olieopvangbak", Description = "Bak voor opvang gebruikte olie", Price = 25m, ImageUrl = "/images/products/olieopvangbak.jpg" },
                new Product { Id = 209, Name = "Wieluitlijner", Description = "Machine voor wieluitlijning", Price = 3000m, ImageUrl = "/images/products/wieluitlijner.jpg" },
                new Product { Id = 210, Name = "Remmenontluchter", Description = "Tool om remmen te ontluchten", Price = 40m, ImageUrl = "/images/products/remmenontluchter.jpg" }
            );

            modelBuilder.Entity<Part>().HasData(
                new Part { Id = 101, Name = "Tandwiel", Description = "Precisie tandwiel", Price = 15.50m, ImageUrl = "/images/parts/tandwiel.jpg" },
                new Part { Id = 102, Name = "M5 Boutje", Description = "Roestvrij staal M5 boutje", Price = 0.10m, ImageUrl = "/images/parts/m5_boutje.jpg" },
                new Part { Id = 103, Name = "Hydraulische Cilinder", Description = "Hydraulische cilinder 10cm", Price = 120m, ImageUrl = "/images/parts/hydraulische_cilinder.jpg" },
                new Part { Id = 104, Name = "Koelvloeistof Pomp", Description = "Pomp voor koelvloeistof", Price = 85m, ImageUrl = "/images/parts/koelvloeistof_pomp.jpg" },
                new Part { Id = 105, Name = "Oliefilter", Description = "Filter voor motorolie", Price = 10m, ImageUrl = "/images/parts/oliefilter.jpg" },
                new Part { Id = 106, Name = "Remblokken", Description = "Set remblokken voor schijfrem", Price = 45m, ImageUrl = "/images/parts/remblokken.jpg" },
                new Part { Id = 107, Name = "Bougie", Description = "Bougie voor benzinemotor", Price = 8m, ImageUrl = "/images/parts/bougie.jpg" },
                new Part { Id = 108, Name = "Radiateurslang", Description = "Slang voor koelvloeistof", Price = 15m, ImageUrl = "/images/parts/radiateurslang.jpg" },
                new Part { Id = 109, Name = "Brandstoffilter", Description = "Filter voor brandstof", Price = 18m, ImageUrl = "/images/parts/brandstoffilter.jpg" },
                new Part { Id = 110, Name = "Koppeling", Description = "Complete koppeling set", Price = 120m, ImageUrl = "/images/parts/koppeling.jpg" }
);


            base.OnModelCreating(modelBuilder);
        }
    }
}
