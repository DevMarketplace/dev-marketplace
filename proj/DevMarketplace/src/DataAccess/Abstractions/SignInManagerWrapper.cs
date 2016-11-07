using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Abstractions
{
    public class SignInManagerWrapper<TUser> : ISignInManagerWrapper<TUser> where TUser : class 
    {
        public SignInManager<TUser> SignInManager { get; set; }

        /// <summary>
        /// Signs in the specified user.
        /// </summary>
        /// <param name="user">The user to sign-in.</param>
        /// <param name="authenticationProperties">Properties applied to the login and authentication cookie.</param>
        /// <param name="authenticationMethod">Name of the method used to authenticate the user.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task SignInAsync(TUser user, AuthenticationProperties authenticationProperties, string authenticationMethod = null)
        {
            return SignInManager.SignInAsync(user, authenticationProperties, authenticationMethod);
        }
    
        /// <summary>
        /// Signs in the specified user.
        /// </summary>
        /// <param name="user">The user to sign-in.</param>
        /// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is
        ///     closed.</param>
        /// <param name="authenticationMethod">Name of the method used to authenticate the user.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        public Task SignInAsync(TUser user, bool isPersistent, string authenticationMethod = null)
        {
            return SignInManager.SignInAsync(user, isPersistent, authenticationMethod);
        }

        /// <summary>
        /// Signs out an user
        /// </summary>
        /// <returns></returns>
        public Task SignOutAsync()
        {
            return SignInManager.SignOutAsync();
        }

        /// <summary>
        /// Signs in an user with a password
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="isPersistent"></param>
        /// <param name="lockoutOnFailure"></param>
        /// <returns></returns>
        public Task<SignInResult> PasswordSignInAsync(TUser user, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return SignInManager.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
        }

        /// <summary>
        /// Signs in by username and a password
        /// </summary>
        /// <param name="userName">The user name to sign in.</param>
        /// <param name="password">The password to attempt to sign in with.</param>
        /// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is
        /// closed.</param>
        /// <param name="lockoutOnFailure">Flag indicating if the user account should be locked if the sign in fails.</param>
        /// <returns>The task object representing the asynchronous operation containing the SignInResult
        ///     for the sign-in attempt.</returns>
        public Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return SignInManager.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
        }
    }
}
