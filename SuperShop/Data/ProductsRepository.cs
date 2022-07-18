using SuperShop.Data.Ententies;

namespace SuperShop.Data
{
    public class ProductsRepository : Genericrepository<Product>, IProductsRepository
    {
        public ProductsRepository(DataContext context): base(context)
        {
        }
    }
}
