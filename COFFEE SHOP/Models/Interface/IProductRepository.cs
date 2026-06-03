
using System.Collections.Generic;
using COFFEE_SHOP.Models;
using Microsoft.EntityFrameworkCore;

namespace COFFEE_SHOP.Models.Interface
{
    
    
        public interface IProductRepository
        {
        
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetTrendingProducts();
        Product? GetProductDetail(int id);



    }
    
}
