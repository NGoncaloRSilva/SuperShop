using SuperShop.Data.Ententies;
using SuperShop.Models;
using System;

namespace SuperShop.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public Product toProduct(ProductViewModel model, Guid imageId, bool isNew)
        {
            return new Product
            {
                Id = isNew ? 0 : model.Id,
                ImageId = imageId,
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
                ImageId = product.ImageId,
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
