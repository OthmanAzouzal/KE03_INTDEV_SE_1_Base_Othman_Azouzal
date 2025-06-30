using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MatrixIncDbContext _context;
        private readonly IDiscountRepository _discountRepository; // Voeg dit toe

        public ProductRepository(MatrixIncDbContext context, IDiscountRepository discountRepository)
        {
            _context = context;
            _discountRepository = discountRepository;  // Initialiseer de discount repository
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var products = _context.Products.Include(p => p.Parts).ToList();
            foreach (var product in products)
            {
                // Pas de korting toe op elk product
                product.Price = ApplyDiscount(product);
            }
            return products;
        }

        public Product? GetProductById(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                // Pas de korting toe op het product
                product.Price = ApplyDiscount(product);
            }
            return product;
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        // Apply discount logica
        private decimal ApplyDiscount(Product product)
        {
            var discount = _discountRepository.GetActiveDiscounts()
                .FirstOrDefault(d => d.ProductId == product.Id); // Kortingen aan producten koppelen

            if (discount != null)
            {
                return product.Price - (product.Price * discount.Percentage / 100);
            }

            return product.Price; // Geen korting, prijs blijft hetzelfde
        }
    }
}
