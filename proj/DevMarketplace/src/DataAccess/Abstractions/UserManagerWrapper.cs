using System;
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
    }
}
