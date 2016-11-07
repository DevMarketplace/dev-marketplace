using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace DataAccess.Abstractions
{
    public interface IUserManagerWrapper<TUser> where TUser : class
    {
        /// <summary>
        /// Creates the specified user in the backing store with given password, as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user to create.</param>
        /// <param name="password">The password for the user to hash and store.</param>
        /// <returns></returns>
        Task<IdentityResult> CreateAsync(TUser user, string password);

        /// <summary>
        /// Generates an EmailConfirmationToken
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<string> GenerateEmailConfirmationTokenAsync(TUser user);    

        /// <summary>
        /// Gets the user, if any, associated with the specified, normalized email address.
        /// </summary>
        /// <param name="email">The normalized email address to return the user for.</param>
        /// <returns>
        /// The task object containing the results of the asynchronous lookup operation,
        /// the user if any associated with the specified normalized email address.
        /// </returns>
        Task<TUser> FindByEmailAsync(string email);

        /// <summary>
        /// Validates that an email confirmation token matches the specified user.
        /// </summary>
        /// <param name="user">The user to validate the token against.</param>
        /// <param name="token">The email confirmation token to validate.</param>
        /// <returns>
        /// The System.Threading.Tasks.Task that represents the asynchronous operation, containing
        /// the Microsoft.AspNetCore.Identity.IdentityResult of the operation.
        /// </returns>
        Task<IdentityResult> ConfirmEmailAsync(TUser user, string token);

        /// <summary>
        /// Finds and returns a user, if any, who has the specified userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        ///  The System.Threading.Tasks.Task that represents the asynchronous operation, containing
        ///  the user matching the specified userId if it exists.
        /// </returns>
        Task<TUser> FindByIdAsync(string userId);
    }
}
