using Microsoft.AspNetCore.Mvc.Rendering;
using SuperShop.Data.Ententies;
using System.Collections.Generic;
using System.Linq;

namespace SuperShop.Data
{
    public interface IProductsRepository : IGenericrepository<Product>
    {
        public IQueryable GetAllWithUsers();

        public IEnumerable<SelectListItem> GetComboProducts();
    }
}
