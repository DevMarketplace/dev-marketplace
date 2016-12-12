#region License
// The Developer Marketplace is a web application that allows individuals, 
// teams and companies share KanBan stories, cards, tasks and items from 
// their KanBan boards and Scrum boards. 
// All shared stories become available on the Developer Marketplace website
//  and software engineers from all over the world can work on these stories. 
// 
// Copyright (C) 2016 Tosho Toshev
// 
//     This program is free software: you can redistribute it and/or modify
//     it under the terms of the GNU General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     This program is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU General Public License for more details.
// 
//     You should have received a copy of the GNU General Public License
//     along with this program.  If not, see <http://www.gnu.org/licenses/>.
// 
// GitHub repository: https://github.com/cracker4o/dev-marketplace
// e-mail: cracker4o@gmail.com
#endregion

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;

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

        /// <summary>
        /// Signs in an user with a password
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="isPersistent"></param>
        /// <param name="lockoutOnFailure"></param>
        /// <returns></returns>
        Task<SignInResult> PasswordSignInAsync(TUser user, string password, bool isPersistent, bool lockoutOnFailure);

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
        Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure);

        /// <summary>
        /// Signs in a user via a previously registered third party login, as an asynchronous
        /// operation.
        /// </summary>
        /// <param name="loginProvider">The login provider to use.</param>
        /// <param name="providerKey">The unique provider identifier for the user.</param>
        /// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
        /// <returns></returns>
        Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent);

        /// <summary>
        /// Gets a collection of Microsoft.AspNetCore.Http.Authentication.AuthenticationDescriptions
        /// for the known external login providers.
        /// </summary>
        /// <returns>
        ///  A collection of Microsoft.AspNetCore.Http.Authentication.AuthenticationDescriptions
        ///  for the known external login providers.
        /// </returns>
        IEnumerable<AuthenticationDescription> GetExternalAuthenticationSchemes();

        /// <summary>
        /// Gets the external login information for the current login, as an asynchronous
        /// operation.
        /// </summary>
        /// <param name="expectedXsrf">Flag indication whether a Cross Site Request Forgery token was expected in the
        /// current request.</param>
        /// <returns>
        /// The task object representing the asynchronous operation containing the ExternalLoginInfo
        /// for the sign-in attempt.
        /// </returns>
        Task<ExternalLoginInfo> GetExternalLoginInfoAsync(string expectedXsrf = null);

        /// <summary>
        /// Configures the redirect URL and user identifier for the specified external login
        /// provider.
        /// </summary>
        /// <param name="provider">The provider to configure.</param>
        /// <param name="redirectUrl">The external login URL users should be redirected to during the login flow.</param>
        /// <param name="userId">The current user's identifier, which will be used to provide CSRF protection.</param>
        /// <returns>A configured Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties.</returns>
        AuthenticationProperties ConfigureExternalAuthenticationProperties(string provider, string redirectUrl, string userId = null);

        /// <summary>
        /// Stores any authentication tokens found in the external authentication cookie
        /// into the associated user.
        /// </summary>
        /// <param name="externalLogin">The information from the external login provider.</param>
        /// <returns>
        ///  The System.Threading.Tasks.Task that represents the asynchronous operation, containing
        ///  the Microsoft.AspNetCore.Identity.IdentityResult of the operation. 
        /// </returns>
        Task<IdentityResult> UpdateExternalAuthenticationTokensAsync(ExternalLoginInfo externalLogin);
    }
}