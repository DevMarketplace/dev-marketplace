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

        public Task SignInAsync(TUser user, AuthenticationProperties authenticationProperties, string authenticationMethod = null)
        {
            return SignInManager.SignInAsync(user, authenticationProperties, authenticationMethod);
        }

        public Task SignInAsync(TUser user, bool isPersistent, string authenticationMethod = null)
        {
            return SignInManager.SignInAsync(user, isPersistent, authenticationMethod);
        }

        public Task SignOutAsync()
        {
            return SignInManager.SignOutAsync();
        }

        public Task<SignInResult> PasswordSignInAsync(TUser user, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return SignInManager.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
        }

        public Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return SignInManager.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
        }

        public Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent)
        {
            return SignInManager.ExternalLoginSignInAsync(loginProvider, providerKey, isPersistent);
        }

        public IEnumerable<AuthenticationDescription> GetExternalAuthenticationSchemes()
        {
            return SignInManager.GetExternalAuthenticationSchemes();
        }

        public Task<ExternalLoginInfo> GetExternalLoginInfoAsync(string expectedXsrf = null)
        {
            return SignInManager.GetExternalLoginInfoAsync(expectedXsrf);
        }
    }
}
