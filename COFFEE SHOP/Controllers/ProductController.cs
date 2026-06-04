using COFFEE_SHOP.Models.Interface;
using COFFEE_SHOP.Models.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using COFFEE_SHOP;

namespace COFFEE_SHOP.Controllers
{
    public class ProductController : Controller
    {
        
        private readonly IProductRepository _productRepository;
        private object productRepository;

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
            ViewBag.title = "Our Menu";
            ViewBag.subTitle = "Make your mornings better with our top-selling product.";
            HttpContext.Session.SetString("CartSummary", "Bạn đang có 3 sản phẩm trong giỏ hàng!");

            var products = _productRepository.GetAllProducts();

            
            return View(products);
        }
        public IActionResult Detail(int id)
        {
            var product = _productRepository.GetProductDetail(id);
            if (product != null)
            {
                HttpContext.Session.SetObjectAsJson("LatelyProduct", product);
                var savedProduct = HttpContext.Session.GetObjectFromJson<Models.Product>("LatelyProduct");
                var sessionData = HttpContext.Session.GetString("CartSummary");
                ViewBag.SessionInfo = sessionData;
                return View(product);
            }
            return NotFound();
        }
    }
}