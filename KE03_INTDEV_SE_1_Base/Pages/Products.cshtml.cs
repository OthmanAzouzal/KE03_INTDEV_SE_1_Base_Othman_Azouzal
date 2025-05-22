using System.Collections.Generic;
using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly MatrixIncDbContext _context;
        private readonly CartRepository _cartRepository;

        public ProductsModel(MatrixIncDbContext context, CartRepository cartRepository)
        {
            _context = context;
            _cartRepository = cartRepository;
        }

        public List<Product> Products { get; set; } = new();

        public void OnGet()
        {
            Products = _context.Products.ToList();
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

            return RedirectToPage(); // Herlaad de pagina zodat de toast getoond wordt
        }
    }
}
