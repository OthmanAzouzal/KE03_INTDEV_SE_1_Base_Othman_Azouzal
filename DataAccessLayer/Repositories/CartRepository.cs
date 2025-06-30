using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Http;

namespace DataAccessLayer.Repositories
{
    public class CartRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string CartKey = "Cart";

        public CartRepository(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public List<CartItem> GetCart()
        {
            var session = _httpContextAccessor.HttpContext!.Session;
            var json = session.GetString(CartKey);
            return json == null ? new List<CartItem>() : JsonSerializer.Deserialize<List<CartItem>>(json)!;
        }

        public void SaveCart(List<CartItem> cart)
        {
            var session = _httpContextAccessor.HttpContext!.Session;
            session.SetString(CartKey, JsonSerializer.Serialize(cart));
        }

        public void AddToCart(Product product)
        {
            var cart = GetCart();

            var existingItem = cart.FirstOrDefault(c => c.ProductId == product.Id);
            if (existingItem != null)
            {
                existingItem.Quantity += 1;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = 1,
                    ImageUrl = product.ImageUrl
                });
            }

            SaveCart(cart);
        }

        public void ClearCart()
        {
            _httpContextAccessor.HttpContext!.Session.Remove(CartKey);
        }
    }
}
