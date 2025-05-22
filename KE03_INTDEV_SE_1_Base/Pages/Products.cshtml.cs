using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class ProductsModel : PageModel
    {
        public List<Product> Products { get; set; }

        [BindProperty]
        public int SelectedProductId { get; set; }

        private readonly CartRepository _cartRepository;

        public ProductsModel(CartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public void LoadProducts()
        {
            Products = new List<Product>
            {
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
            };
        }

        public void OnGet()
        {
            LoadProducts();
        }

        public IActionResult OnPostAddToCart()
        {
            LoadProducts();

            var product = Products.FirstOrDefault(p => p.Id == SelectedProductId);
            if (product != null)
            {
                _cartRepository.AddToCart(product);
            }

            TempData["ProductName"] = product?.Name;
            TempData["ProductImage"] = product?.ImageUrl;

            return RedirectToPage();
        }
    }
}
