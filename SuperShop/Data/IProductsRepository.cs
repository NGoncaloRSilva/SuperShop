using SuperShop.Data.Ententies;
using System.Linq;

namespace SuperShop.Data
{
    public interface IProductsRepository : IGenericrepository<Product>
    {
        public IQueryable GetAllWithUsers();
    }
}
