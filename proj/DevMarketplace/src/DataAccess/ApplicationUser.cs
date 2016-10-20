using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    /// <summary>
    /// Represents the default application user that will be used for Authentication purposes.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
    }
}
