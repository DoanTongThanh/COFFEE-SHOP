using Microsoft.AspNetCore.Mvc;

namespace COFFEE_SHOP.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}