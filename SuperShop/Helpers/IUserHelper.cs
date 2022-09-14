using Microsoft.AspNetCore.Identity;
using SuperShop.Data.Ententies;
using SuperShop.Models;
using System.Threading.Tasks;

namespace SuperShop.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserbyEmailAsync(string email);


        Task<IdentityResult> AddUserAsync(User user, string password);

        Task<SignInResult> LoginAsync(LoginViewModel model);
        Task LogoutAsync();

        Task<IdentityResult> UpdateUserAsync(User user);

        Task<IdentityResult> ChangePasswordAsync(User user, string oldPassword, string newPassword);
    }
}
