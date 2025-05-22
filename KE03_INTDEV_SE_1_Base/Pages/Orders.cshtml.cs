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

        public List<Order> Orders { get; set; } = new();

        public OrdersModel(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void OnGet()
        {
            // In de toekomst: haal CustomerId uit ingelogde gebruiker
            Orders = _orderRepository.GetAllOrders()
                .Where(o => o.CustomerId == 1)
                .ToList();
        }
    }
}
