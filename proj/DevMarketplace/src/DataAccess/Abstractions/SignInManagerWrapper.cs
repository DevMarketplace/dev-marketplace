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
            throw new NotImplementedException();
        }

        public Task SignInAsync(TUser user, bool isPersistent, string authenticationMethod = null)
        {
            throw new NotImplementedException();
        }

        public Task SignOutAsync()
        {
            throw new NotImplementedException();
        }
    }
}
