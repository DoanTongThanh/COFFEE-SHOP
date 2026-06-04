using COFFEE_SHOP.Data;
using COFFEE_SHOP.Models;
using COFFEE_SHOP.Models.Interface;

using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace COFFEE_SHOP.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;


        
        public ShoppingCartController(IProductRepository productRepository, IShoppingCartRepository shoppingCartRepository)
        {
            _productRepository = productRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        
        public ViewResult Index()
        {
            
            var items = _shoppingCartRepository.GetAllShoppingCartItems();
            _shoppingCartRepository.ShoppingCartItems = items;

            
            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCartRepository,
                ShoppingCartTotal = _shoppingCartRepository.GetShoppingBagTotal()
            };

            return View(shoppingCartViewModel);
        }

        
        public RedirectToActionResult AddToCart(int id)
        {
            
            var selectedProduct = _productRepository.GetAllProducts().FirstOrDefault(p => p.Id == id);

            if (selectedProduct != null)
            {
                
                _shoppingCartRepository.AddToCart(selectedProduct);
                HttpContext.Session.SetInt32("CartCount", _shoppingCartRepository.GetAllShoppingCartItems().Count);
            }

            
            return RedirectToAction("Index");
        }

        
        public RedirectToActionResult RemoveFromCart(int id)
        {
            var selectedProduct = _productRepository.GetAllProducts().FirstOrDefault(p => p.Id == id);

            if (selectedProduct != null)
            {
                _shoppingCartRepository.RemoveFromCart(selectedProduct);
                HttpContext.Session.SetInt32("CartCount", _shoppingCartRepository.GetAllShoppingCartItems().Count);
            }

            return RedirectToAction("Index");

        }
        
    }
}