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
    }
}
