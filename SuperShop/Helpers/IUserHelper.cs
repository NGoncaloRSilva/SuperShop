using Microsoft.AspNetCore.Identity;
using SuperShop.Data.Ententies;
using System.Threading.Tasks;

namespace SuperShop.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserbyEmailAsync(string email);


        Task<IdentityResult> AddUserAsync(User user, string password);

        
    }
}
