using SuperShop.Data.Ententies;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Data
{
    public class SeedDB
    {
        private readonly DataContext _contex;
        private Random _random;

        public SeedDB(DataContext contex)
        {
            _contex = contex;
            _random = new Random();
        }

        public async Task SeedAsync()
        {
            await _contex.Database.EnsureCreatedAsync();

            if(!_contex.Products.Any())
            {
                AddProduct("Iphone X");
                AddProduct("Magic Mouse");
                AddProduct("IWatch Series 4");
                AddProduct("Ipad Mini");
                await _contex.SaveChangesAsync();   
            }
        }

        private void AddProduct(string name)
        {
            _contex.Products.Add(new Product
            {
                Name = name,
                Price = _random.Next(1000),
                IsAvailable = true,
                Stock = _random.Next(1000),
            });
        }
    }
}
