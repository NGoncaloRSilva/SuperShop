using Microsoft.EntityFrameworkCore;
using SuperShop.Data.Ententies;
using System.Linq;

namespace SuperShop.Data
{
    public class ProductsRepository : Genericrepository<Product>, IProductsRepository
    {
        private readonly DataContext _context;

        public ProductsRepository(DataContext context): base(context)
        {
            _context = context;
        }

        public IQueryable GetAllWithUsers()
        {
            return _context.Products.Include(p => p.User);
        }
    }
}
