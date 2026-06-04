using COFFEE_SHOP.Data;
using COFFEE_SHOP.Models.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace COFFEE_SHOP.Models.Services
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly CoffeeshopDbContext _dbContext;

        public ShoppingCartRepository(CoffeeshopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string? ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();

        public static ShoppingCartRepository GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;
            var context = services.GetService<CoffeeshopDbContext>() ?? throw new Exception("Error initializing DbContext");
            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();
            session?.SetString("CartId", cartId);

            return new ShoppingCartRepository(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Product product)
        {
            
            if (product == null) return;

            
            var shoppingCartItem = _dbContext.ShoppingCartItems.FirstOrDefault(
                s => s.Product!.Id == product.Id && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                
                _dbContext.Products.Attach(product);

                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Qty = 1
                };
                _dbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Qty++;
            }

            
            _dbContext.SaveChanges();
        }

        public int RemoveFromCart(Product product)
        {
            var shoppingCartItem = _dbContext.ShoppingCartItems.SingleOrDefault(
                s => s.Product!.Id == product.Id && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;
            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Qty > 1)
                {
                    shoppingCartItem.Qty--;
                    localAmount = shoppingCartItem.Qty;
                }
                else
                {
                    _dbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            _dbContext.SaveChanges();
            return localAmount;
        }

        public List<ShoppingCartItem> GetAllShoppingCartItems()
        {
            return ShoppingCartItems = _dbContext.ShoppingCartItems
                .Where(c => c.ShoppingCartId == ShoppingCartId)
                .Include(s => s.Product)
                .ToList();
        }

        public void ClearCart()
        {
            var cartItems = _dbContext.ShoppingCartItems.Where(cart => cart.ShoppingCartId == ShoppingCartId);
            _dbContext.ShoppingCartItems.RemoveRange(cartItems);
            _dbContext.SaveChanges();
        }

        public decimal GetShoppingBagTotal()
        {
            var total = _dbContext.ShoppingCartItems
                .Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Product!.Price * c.Qty)
                .Sum();
            return total;
        }
    }
}