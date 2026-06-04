
using System.Collections.Generic;

namespace COFFEE_SHOP.Models.Interface
{
    public interface IShoppingCartRepository
    {
        void AddToCart(Product product);
        int RemoveFromCart(Product product);
        List<ShoppingCartItem> GetAllShoppingCartItems();
        void ClearCart();
        decimal GetShoppingBagTotal();
        List<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}