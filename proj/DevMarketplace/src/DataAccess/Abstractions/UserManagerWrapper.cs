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
