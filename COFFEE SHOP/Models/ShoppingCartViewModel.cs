using COFFEE_SHOP.Models.Interface;

namespace COFFEE_SHOP.Models
{
    public class ShoppingCartViewModel
    {
        public IShoppingCartRepository ShoppingCart { get; set; } = default!;
        public decimal ShoppingCartTotal { get; set; }
    }
}