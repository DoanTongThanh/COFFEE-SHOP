using Microsoft.AspNetCore.Mvc;
using COFFEE_SHOP.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace COFFEE_SHOP.Controllers
{
    public class ProductController : Controller
    {
        
        private readonly IProductRepository _productRepository;

        
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        
        public IActionResult Get()
        {
            
            var products = _productRepository.GetAllProducts();

            
            return Ok(products);
        }
        public IActionResult Shop()
        {
            
            var products = _productRepository.GetAllProducts();

            
            return View(products);
        }
    }
}