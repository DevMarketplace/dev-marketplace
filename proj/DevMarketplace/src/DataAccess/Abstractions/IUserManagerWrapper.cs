using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

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

        /// <summary>
        /// Gets a flag indicating whether the email address for the specified user has been
        /// verified, true if the email address is verified otherwise false.
        /// </summary>
        /// <param name="user">The user whose email confirmation status should be returned.</param>
        /// <returns>
        /// The task object containing the results of the asynchronous operation, a flag
        /// indicating whether the email address for the specified user has been confirmed
        /// or not.
        /// </returns>
        Task<bool> IsEmailConfirmedAsync(TUser user);

        /// <summary>
        /// Generates a password reset token for the specified user, using the configured
        /// password reset token provider.
        /// </summary>
        /// <param name="user">The user to generate a password reset token for.</param>
        /// <returns>
        /// The System.Threading.Tasks.Task that represents the asynchronous operation, containing
        /// a password reset token for the specified user.
        /// </returns>
        Task<string> GeneratePasswordResetTokenAsync(TUser user);

        /// <summary>
        /// Resets the user's password to the specified newPassword after validating the
        /// given password reset token.
        /// </summary>
        /// <param name="user">The user whose password should be reset.</param>
        /// <param name="token">The password reset token to verify.</param>
        /// <param name="newPassword">The new password to set if reset token verification fails.</param>
        /// <returns>
        /// The System.Threading.Tasks.Task that represents the asynchronous operation, containing
        /// the Microsoft.AspNetCore.Identity.IdentityResult of the operation.
        /// </returns>
        Task<IdentityResult> ResetPasswordAsync(TUser user, string token, string newPassword);

        /// <summary>
        /// Returns the user corresponding to the IdentityOptions.ClaimsIdentity.UserIdClaimType
        /// claim in the principal or null.
        /// </summary>
        /// <param name="principal">The principal which contains the user id claim.</param>
        /// <returns>
        /// The user corresponding to the IdentityOptions.ClaimsIdentity.UserIdClaimType
        /// claim in the principal or null
        /// </returns>
        Task<TUser> GetUserAsync(ClaimsPrincipal principal);
    }
}
