using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Abstractions
{
    public class UserManagerWrapper<TUser> : IUserManagerWrapper<TUser> where TUser : class
    {
        public UserManager<TUser> UserManager { get; set; }

        public Task<IdentityResult> CreateAsync(TUser user, string password)
        {
            return UserManager.CreateAsync(user, password);
        }

        public Task<string> GenerateEmailConfirmationTokenAsync(TUser user)
        {
            return UserManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public Task<TUser> FindByEmailAsync(string email)
        {
            return UserManager.FindByEmailAsync(email);
        }

        public Task<IdentityResult> ConfirmEmailAsync(TUser user, string token)
        {
            return UserManager.ConfirmEmailAsync(user, token);
        }

        public Task<TUser> FindByIdAsync(string userId)
        {
            return UserManager.FindByIdAsync(userId);
        }

        public Task<bool> IsEmailConfirmedAsync(TUser user)
        {
            return UserManager.IsEmailConfirmedAsync(user);
        }

        public Task<string> GeneratePasswordResetTokenAsync(TUser user)
        {
            return UserManager.GeneratePasswordResetTokenAsync(user);
        }

        public Task<IdentityResult> ResetPasswordAsync(TUser user, string token, string newPassword)
        {
            return UserManager.ResetPasswordAsync(user, token, newPassword);
        }

        public Task<TUser> GetUserAsync(ClaimsPrincipal principal)
        {
            return UserManager.GetUserAsync(principal);
        }

        public Task<IdentityResult> UpdateAsync(TUser user)
        {
            return UserManager.UpdateAsync(user);
        }
    }
}
