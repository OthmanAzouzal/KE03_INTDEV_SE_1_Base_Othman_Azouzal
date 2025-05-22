using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class PartsModel : PageModel
    {
        public List<Product> Parts { get; set; } = new();

        [BindProperty]
        public int SelectedPartId { get; set; }

        private readonly CartRepository _cartRepository;

        public PartsModel(CartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public void LoadParts()
        {
            Parts = new List<Product>
            {
                new Product { Id = 101, Name = "Tandwiel", Description = "Precisie tandwiel", Price = 15.50m, ImageUrl = "/images/parts/tandwiel.jpg" },
                new Product { Id = 102, Name = "M5 Boutje", Description = "Roestvrij staal M5 boutje", Price = 0.10m, ImageUrl = "/images/parts/m5_boutje.jpg" },
                new Product { Id = 103, Name = "Hydraulische Cilinder", Description = "Hydraulische cilinder 10cm", Price = 120m, ImageUrl = "/images/parts/hydraulische_cilinder.jpg" },
                new Product { Id = 104, Name = "Koelvloeistof Pomp", Description = "Pomp voor koelvloeistof", Price = 85m, ImageUrl = "/images/parts/koelvloeistof_pomp.jpg" },
                new Product { Id = 105, Name = "Oliefilter", Description = "Filter voor motorolie", Price = 10m, ImageUrl = "/images/parts/oliefilter.jpg" },
                new Product { Id = 106, Name = "Remblokken", Description = "Set remblokken voor schijfrem", Price = 45m, ImageUrl = "/images/parts/remblokken.jpg" },
                new Product { Id = 107, Name = "Bougie", Description = "Bougie voor benzinemotor", Price = 8m, ImageUrl = "/images/parts/bougie.jpg" },
                new Product { Id = 108, Name = "Radiateurslang", Description = "Slang voor koelvloeistof", Price = 15m, ImageUrl = "/images/parts/radiateurslang.jpg" },
                new Product { Id = 109, Name = "Brandstoffilter", Description = "Filter voor brandstof", Price = 18m, ImageUrl = "/images/parts/brandstoffilter.jpg" },
                new Product { Id = 110, Name = "Koppeling", Description = "Complete koppeling set", Price = 120m, ImageUrl = "/images/parts/koppeling.jpg" }
            };
        }

        public void OnGet()
        {
            LoadParts();
        }

        public IActionResult OnPostAddToCart()
        {
            LoadParts();

            var part = Parts.FirstOrDefault(p => p.Id == SelectedPartId);
            if (part != null)
            {
                _cartRepository.AddToCart(part);
                TempData["ProductName"] = part.Name;
                TempData["ProductImage"] = part.ImageUrl;
            }

            return RedirectToPage();
        }
    }
}
