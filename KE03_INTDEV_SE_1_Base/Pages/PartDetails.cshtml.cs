using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class PartDetailsModel : PageModel
    {
        private readonly MatrixIncDbContext _context;
        private readonly CartRepository _cartRepository;

        public PartDetailsModel(MatrixIncDbContext context, CartRepository cartRepository)
        {
            _context = context;
            _cartRepository = cartRepository;
        }

        public Part? Part { get; set; }

        public IActionResult OnGet(int id)
        {
            Part = _context.Parts.FirstOrDefault(p => p.Id == id);

            if (Part == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPostAddToCart(int productId)
        {
            var part = _context.Parts.Find(productId);
            if (part == null)
            {
                return NotFound();
            }

            var product = new Product
            {
                Id = part.Id,
                Name = part.Name,
                Description = part.Description,
                Price = part.Price,
                ImageUrl = part.ImageUrl
            };

            _cartRepository.AddToCart(product);

            TempData["ProductName"] = product.Name;
            TempData["ProductImage"] = product.ImageUrl;

            return RedirectToPage("/Parts");
        }
    }
}
