using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KE03_INTDEV_SE_1_Base.Pages
{
    public class WinkelwagenModel : PageModel
    {
        private readonly CartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public List<CartItem> CartItems { get; set; } = new();

        public WinkelwagenModel(CartRepository cartRepo, IOrderRepository orderRepo, IProductRepository productRepo)
        {
            _cartRepository = cartRepo;
            _orderRepository = orderRepo;
            _productRepository = productRepo;
        }

        public void OnGet()
        {
            var items = _cartRepository.GetCart();
            foreach (var item in items)
            {
                var product = _productRepository.GetProductById(item.ProductId);
                if (product != null)
                {
                    item.ImageUrl = product.ImageUrl;
                }
            }

            CartItems = items;
        }


        public IActionResult OnPostBestellen()
        {
            var items = _cartRepository.GetCart();

            var order = new Order
            {
                CustomerId = 1,
                OrderDate = DateTime.Now
            };

            foreach (var item in items)
            {
                var product = _productRepository.GetProductById(item.ProductId);
                if (product != null)
                {
                    order.Products.Add(product);
                }
            }

            _orderRepository.AddOrder(order);
            _cartRepository.ClearCart();

            TempData["OrderSuccess"] = "Je bestelling is geplaatst!";

            return RedirectToPage("/Winkelwagen");

        }

        public IActionResult OnPostVerwijderItem(int productId)
        {
            var cart = _cartRepository.GetCart();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);

            if (item != null)
            {
                item.Quantity--;
                if (item.Quantity <= 0)
                {
                    cart.Remove(item);
                }
                _cartRepository.SaveCart(cart);
            }

            return RedirectToPage();
        }

        public IActionResult OnPostToevoegenItem(int productId)
        {
            var cart = _cartRepository.GetCart();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);

            if (item != null)
            {
                item.Quantity++;
                _cartRepository.SaveCart(cart);
            }

            return RedirectToPage();
        }


    }


}
