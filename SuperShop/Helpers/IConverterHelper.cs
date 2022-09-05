using SuperShop.Data.Ententies;
using SuperShop.Models;
using System;

namespace SuperShop.Helpers
{
    public interface IConverterHelper
    {
        Product toProduct(ProductViewModel model, Guid imageId, bool isNew);
        
        ProductViewModel toProductViewModel(Product product);

    }
}
