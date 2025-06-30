using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace KE03_INTDEV_SE_1_Base
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Maak een builder aan voor de WebApplication
            var builder = WebApplication.CreateBuilder(args);

            // Registreer services vóór Build
            builder.Services.AddDbContext<MatrixIncDbContext>(options =>
                options.UseSqlite("Data Source=MatrixInc.db")); // Configureer de database met SQLite

            // Voeg repositories toe aan dependency injection container
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IPartRepository, PartRepository>();
            builder.Services.AddScoped<IDiscountRepository, DiscountRepository>(); // Kortingsrepository toegevoegd

            // Voeg de HttpContextAccessor toe zodat je toegang hebt tot de sessie
            builder.Services.AddHttpContextAccessor();

            // Configureer sessies en caching
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession();

            // Voeg andere services toe zoals cart repository en cookiebeleid
            builder.Services.AddScoped<CartRepository>();
            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // Voeg Razor Pages toe voor de viewlaag
            builder.Services.AddRazorPages();

            // Bouw de applicatie
            var app = builder.Build();

            // Configureer de middleware pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error"); // Foutafhandelingspagina
                app.UseHsts(); // HTTPS Strict Transport Security
            }

            // Initieer de database bij het opstarten als deze nog niet bestaat
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<MatrixIncDbContext>();
                context.Database.EnsureCreated(); // Zorg ervoor dat de database wordt aangemaakt als die nog niet bestaat
                MatrixIncDbInitializer.Initialize(context); // Initialiseer de database met eventuele voorbeelddata
            }

            // Configuraties voor sessies en routing
            app.UseSession(); // Sessies gebruiken
            app.UseHttpsRedirection(); // Verplichten om HTTPS te gebruiken
            app.UseStaticFiles(); // Statische bestanden zoals CSS en afbeeldingen beschikbaar stellen

            app.UseRouting(); // Routing middleware

            app.UseAuthorization(); // Autorisatie middleware (als je autorisatie hebt geconfigureerd)

            // Map Razor Pages (voor routing naar .cshtml pagina's)
            app.MapRazorPages();

            // Start de applicatie
            app.Run();
        }
    }
}
