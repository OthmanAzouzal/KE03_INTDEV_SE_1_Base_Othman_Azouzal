using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace KE03_INTDEV_SE_1_Base.Pages
{
        public class OrdersModel : PageModel
        {
            private readonly IOrderRepository _orderRepository;
            private readonly ICustomerRepository _customerRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public List<Order> Orders { get; set; } = new();

            public OrdersModel(
                IOrderRepository orderRepository,
                ICustomerRepository customerRepository,
                IHttpContextAccessor httpContextAccessor)
            {
                _orderRepository = orderRepository;
                _customerRepository = customerRepository;
                _httpContextAccessor = httpContextAccessor;
            }

        public void OnGet()
        {
            var email = _httpContextAccessor.HttpContext?.Session.GetString("Username");

            if (string.IsNullOrEmpty(email))
            {
                Orders = new List<Order>();
                return;
            }

            var customer = _customerRepository.GetCustomerByEmail(email);
            if (customer != null)
            {
                Orders = _orderRepository.GetOrdersByCustomerId(customer.Id).ToList(); // ToList is optioneel
            }
        }
    }
}



