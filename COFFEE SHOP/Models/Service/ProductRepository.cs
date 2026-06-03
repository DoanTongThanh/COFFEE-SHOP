using COFFEE_SHOP.Data;
using COFFEE_SHOP.Models.Interface;
using System.Collections.Generic;
using System. Linq;
using Microsoft.EntityFrameworkCore;

namespace COFFEE_SHOP.Models.Service
{
    public class ProductRepository : IProductRepository
    {
        private List<Product> ProductsLlist = new List<Product>()
        {
            new Product { Id = 1, Name = "America", Price = 25, Detail = "Name Product", ImageUrl = "https://b.zmtcdn.com/data/pictures/8/18697938/24bd8ad17bfd32e5032e20647e445856.jpg" },
            new Product { Id = 2, Name = "Vietnam", Price = 20, Detail = "Vietnamese Product", ImageUrl = "https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcQH_8fQYfVcxk7pVXVfW2RqifHrwcH2nkS7WYO3Bk_aNxFFw4nn" },
            new Product { Id = 3, Name = "United kingdom", Price = 15, Detail = "Name Product", ImageUrl = "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcT-ag5XBtkJvt5nAHjghQCcaEP3nrtX3c5QfEoSWC4lMTeCe-1X" },
        };
        public IEnumerable<Product> GetAllProducts()
        {
            return ProductsLlist;
        }
        public Product? GetProductDetail(int id)
        {
            return ProductsLlist.FirstOrDefault(p => p.Id == id);
        }
        public IEnumerable<Product> GetTrendingProducts()
        {
            return ProductsLlist.Where(p => p.IsTrendingProduct);
        }
        private CoffeeshopDbContext DbContext;
        public ProductRepository(CoffeeshopDbContext dbContext)
        {
            this.DbContext = dbContext;
        }
    }
}
