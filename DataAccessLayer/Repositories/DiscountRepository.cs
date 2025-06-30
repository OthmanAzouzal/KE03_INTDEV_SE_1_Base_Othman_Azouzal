using DataAccessLayer.Models;
using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly MatrixIncDbContext _context;  // Gebruik MatrixIncDbContext in plaats van ApplicationDbContext

        public DiscountRepository(MatrixIncDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Discount> GetActiveDiscounts()
        {
            return _context.Discounts
                .Where(d => d.StartDate <= DateTime.Now && d.EndDate >= DateTime.Now)
                .ToList();
        }
    }
}
