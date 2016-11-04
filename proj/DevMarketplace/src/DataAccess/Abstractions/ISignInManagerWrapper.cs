using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Authentication;

namespace DataAccess.Abstractions
{
    public interface ISignInManagerWrapper<in TUser> where TUser : class
    {
        /// <summary>
        /// Signs in the specified user.
        /// </summary>
        /// <param name="user">The user to sign-in.</param>
        /// <param name="authenticationProperties">Properties applied to the login and authentication cookie.</param>
        /// <param name="authenticationMethod">Name of the method used to authenticate the user.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task SignInAsync(TUser user, AuthenticationProperties authenticationProperties, string authenticationMethod = null);

        /// <summary>
        /// Signs in the specified user.
        /// </summary>
        /// <param name="user">The user to sign-in.</param>
        /// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
        /// <param name="authenticationMethod">Name of the method used to authenticate the user.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task SignInAsync(TUser user, bool isPersistent, string authenticationMethod = null);
  
        /// <summary>
        /// Signs the current user out of the application.
        /// </summary>
        /// <returns></returns>
        Task SignOutAsync();
    }
}
