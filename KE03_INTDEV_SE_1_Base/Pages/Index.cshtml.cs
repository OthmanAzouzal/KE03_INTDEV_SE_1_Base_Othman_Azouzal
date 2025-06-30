using System.Collections.Generic;
using System.Linq;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ICustomerRepository _customerRepository;
        private readonly IProductRepository _productRepository;
        private readonly MatrixIncDbContext _context;

        public IList<Customer> Customers { get; set; }
        public IList<Product> SaleProducts { get; set; }
        public List<Part> DiscountedParts { get; set; } = new();

        public IndexModel(
            ILogger<IndexModel> logger,
            ICustomerRepository customerRepository,
            IProductRepository productRepository,
            MatrixIncDbContext context)
        {
            _logger = logger;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
            _context = context;
            Customers = new List<Customer>();
            SaleProducts = new List<Product>();
        }

        public IActionResult OnGet()
        {
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToPage("/Login");
            }

            // Haal alle producten op via repository
            var allProducts = _productRepository.GetAllProducts();

            // Filter producten met korting
            SaleProducts = allProducts
                .Where(p => p.DiscountPercentage > 0)
                .ToList();

            // Haal onderdelen met korting rechtstreeks uit de context
            DiscountedParts = _context.Parts
                .Where(p => p.DiscountPercentage > 0)
                .ToList();

            return Page();
        }
    }
}
