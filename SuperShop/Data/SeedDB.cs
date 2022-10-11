using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SuperShop.Data.Ententies;
using SuperShop.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Data
{
    public class SeedDB
    {
        private readonly DataContext _contex;
        private readonly IUserHelper _userHelper;
        private Random _random;

        public SeedDB(DataContext contex, IUserHelper userHelper)
        {
            _contex = contex;
            _userHelper = userHelper;
            _random = new Random();
        }

        public async Task SeedAsync()
        {
            await _contex.Database.MigrateAsync();

            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Customer");

            var user = await _userHelper.GetUserbyEmailAsync("ngoncalorsilva@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Nuno",
                    LastName = "Silva",
                    Email = "ngoncalorsilva@gmail.com",
                    UserName = "ngoncalorsilva@gmail.com",
                    PhoneNumber = "212344555"
                };

                var result = await _userHelper.AddUserAsync(user, "123456");

                if(result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }

            var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");

            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }

            //Criação customer

            var user2 = await _userHelper.GetUserbyEmailAsync("joaoricardo@gmail.com");
            if (user2 == null)
            {
                user2 = new User
                {
                    FirstName = "Joao",
                    LastName = "Ricardo",
                    Email = "joaoricardo@gmail.com",
                    UserName = "joaoricardo@gmail.com",
                    PhoneNumber = "212344555"
                };

                var result = await _userHelper.AddUserAsync(user2, "123456");

                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await _userHelper.AddUserToRoleAsync(user2, "Customer");
            }

            var isInRole2 = await _userHelper.IsUserInRoleAsync(user, "Customer");

            if (!isInRole2)
            {
                await _userHelper.AddUserToRoleAsync(user2, "Customer");
            }

            if (!_contex.Products.Any())
            {
                AddProduct("Iphone X", user);
                AddProduct("Magic Mouse", user);
                AddProduct("IWatch Series 4", user);
                AddProduct("Ipad Mini", user);
                await _contex.SaveChangesAsync();   
            }
        }

        private void AddProduct(string name, User user)
        {
            _contex.Products.Add(new Product
            {
                Name = name,
                Price = _random.Next(1000),
                IsAvailable = true,
                Stock = _random.Next(1000),
                User = user
            });
        }
    }
}
