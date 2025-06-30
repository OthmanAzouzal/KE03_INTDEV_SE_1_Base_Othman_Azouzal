using System.Collections.Generic;
using System.Linq;
using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class PartsModel : PageModel
    {
        private readonly MatrixIncDbContext _context;
        private readonly CartRepository _cartRepository;

        public PartsModel(MatrixIncDbContext context, CartRepository cartRepository)
        {
            _context = context;
            _cartRepository = cartRepository;
        }

        public List<Part> Parts { get; set; } = new();

        [BindProperty]
        public int SelectedPartId { get; set; }

        public void OnGet()
        {
            Parts = _context.Parts.ToList();
        }

        public IActionResult OnPostAddToCart()
        {
            var part = _context.Parts.FirstOrDefault(p => p.Id == SelectedPartId);
            if (part != null)
            {
                var discountedPrice = part.Price * (1 - part.DiscountPercentage / 100m);

                var product = new Product
                {
                    Id = part.Id,
                    Name = part.Name,
                    Description = part.Description,
                    Price = discountedPrice, // prijs met korting doorgeven
                    ImageUrl = part.ImageUrl
                };

                _cartRepository.AddToCart(product);

                TempData["ProductName"] = product.Name;
                TempData["ProductImage"] = product.ImageUrl;
            }
            return RedirectToPage();
        }
    }
}
