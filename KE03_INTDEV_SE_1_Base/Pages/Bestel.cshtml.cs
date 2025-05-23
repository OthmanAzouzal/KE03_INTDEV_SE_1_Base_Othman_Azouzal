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
                _context.SaveChanges();
            }

            var order = new Order
            {
                OrderDate = DateTime.Now,
                CustomerId = customer.Id,
                OrderItems = new List<OrderItem>()
            };

            foreach (var item in cartItems)
            {
                var orderItem = new OrderItem
                {
                    Quantity = item.Quantity,
                    UnitPrice = item.Price
                };

                if (item.IsProduct) // Stel je hebt een property die dit aangeeft
                {
                    orderItem.ProductId = item.ProductId;
                }
                else // anders is het een part
                {
                    orderItem.PartId = item.PartId;
                }

                order.OrderItems.Add(orderItem);
            }

            _context.Orders.Add(order);
            _context.SaveChanges();

            _cartRepository.ClearCart();

            HttpContext.Session.SetString("Username", Email);

            TempData["OrderSuccess"] = "Bedankt voor je bestelling!";
            return RedirectToPage("/Orders");
        }

    }
}
