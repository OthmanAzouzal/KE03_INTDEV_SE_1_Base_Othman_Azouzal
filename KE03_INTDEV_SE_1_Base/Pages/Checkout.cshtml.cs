using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly CartRepository _cartRepository;

        public CheckoutModel(
            ICustomerRepository customerRepository,
            IOrderRepository orderRepository,
            CartRepository cartRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
        }

        [BindProperty]
        public string CustomerName { get; set; }

        [BindProperty]
        public string CustomerEmail { get; set; }

        [BindProperty]
        public string CustomerAddress { get; set; }

        public IActionResult OnGet()
        {
            var cart = _cartRepository.GetCart();
            if (!cart.Any())
            {
                return RedirectToPage("/Winkelwagen");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // 1. Customer aanmaken
            var customer = new Customer
            {
                Name = CustomerName,
                Email = CustomerEmail,
                Address = CustomerAddress
            };

            _customerRepository.AddCustomer(customer);

            // 2. Bestelling aanmaken
            var cartItems = _cartRepository.GetCart();

            var order = new Order
            {
                CustomerId = customer.Id,
                OrderDate = DateTime.Now,
                OrderItems = cartItems.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Price
                }).ToList()
            };

            _orderRepository.AddOrder(order);

            // 3. Winkelwagen leegmaken
            _cartRepository.ClearCart();

            return RedirectToPage("/OrderBevestiging");
        }
    }
}
