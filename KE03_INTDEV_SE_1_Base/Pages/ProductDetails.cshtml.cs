using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class ProductDetailsModel : PageModel
    {
        private readonly MatrixIncDbContext _context;
        private readonly CartRepository _cartRepository;

        public ProductDetailsModel(MatrixIncDbContext context, CartRepository cartRepository)
        {
            _context = context;
            _cartRepository = cartRepository;
        }

        public Product? Product { get; set; }

        public IActionResult OnGet(int id)
        {
            Product = _context.Products.Find(id);
            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPostAddToCart(int productId)
        {
            var product = _context.Products.Find(productId);
            if (product == null)
            {
                return NotFound();
            }

            _cartRepository.AddToCart(product);

            TempData["ProductName"] = product.Name;
            TempData["ProductImage"] = product.ImageUrl;

            return RedirectToPage("/Products");
        }
    }
}