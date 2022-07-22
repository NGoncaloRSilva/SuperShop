using SuperShop.Data.Ententies;
using SuperShop.Models;

namespace SuperShop.Helpers
{
    public interface IConverterHelper
    {
        Product toProduct(ProductViewModel model, string path, bool isNew);
        
        ProductViewModel toProductViewModel(Product product);

    }
}
