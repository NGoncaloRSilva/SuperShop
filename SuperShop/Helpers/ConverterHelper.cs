using SuperShop.Data.Ententies;
using SuperShop.Models;

namespace SuperShop.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public Product toProduct(ProductViewModel model,string path, bool isNew)
        {
            return new Product
            {
                Id = isNew ? 0 : model.Id,
                ImageUrl = path,
                IsAvailable = model.IsAvailable,
                Name = model.Name,
                LastPurchase = model.LastPurchase,
                LastSale = model.LastSale,
                Price = model.Price,
                Stock = model.Stock,
                User = model.User,

            };
        }

        public ProductViewModel toProductViewModel(Product product)
        {
            return new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                ImageUrl = product.ImageUrl,
                IsAvailable = product.IsAvailable,
                LastPurchase = product.LastPurchase,
                LastSale = product.LastSale,
                Price = product.Price,
                Stock = product.Stock,
                User = product.User
            };
        }
    }
}
