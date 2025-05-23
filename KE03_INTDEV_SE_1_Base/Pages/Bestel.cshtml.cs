using System;
using System.Linq;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class BestelModel : PageModel
    {
        private readonly MatrixIncDbContext _context;
        private readonly CartRepository _cartRepository;
        private readonly ICustomerRepository _customerRepository;

        public BestelModel(MatrixIncDbContext context, CartRepository cartRepository, ICustomerRepository customerRepository)
        {
            _context = context;
            _cartRepository = cartRepository;
            _customerRepository = customerRepository;
        }

        [BindProperty]
        public string Naam { get; set; }

        [BindProperty]
        public string Adres { get; set; }

        [BindProperty]
        public string Email { get; set; }

        public IActionResult OnPost()
        {
            var cartItems = _cartRepository.GetCart();

            if (!cartItems.Any())
            {
                ModelState.AddModelError(string.Empty, "Je winkelmand is leeg.");
                return Page();
            }

            // Zoek bestaande klant of maak nieuwe aan
            var customer = _customerRepository.GetCustomerByEmail(Email);
            if (customer == null)
            {
                customer = new Customer
                {
                    Name = Naam,
                    Address = Adres,
                    Email = Email
                };
                _context.Customers.Add(customer);
                _context.SaveChanges(); // nodig om Customer.Id te krijgen
            }

            // Maak order en orderitems aan
            var order = new Order
            {
                OrderDate = DateTime.Now,
                CustomerId = customer.Id,
                OrderItems = cartItems.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Price
                }).ToList()
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            _context.Entry(customer).Reload();


            // Leeg winkelmand
            _cartRepository.ClearCart();

            // Opslaan van e-mail in sessie voor orderhistorie
            HttpContext.Session.SetString("Username", Email);

            TempData["OrderSuccess"] = "Bedankt voor je bestelling!";
            return RedirectToPage("/Orders");
        }
    }
}
