using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fora.Web.Infrastructure
{
    public static class Extensions
    {
        public static IdentityResult ToIdentity<T>(this Operation<T> operation)
        {
            if (operation.Succeeded)
            {
                return IdentityResult.Success;
            }
            else
            {
                return IdentityResult.Failed(new[] { new IdentityError { Code = "Operation Error", Description = operation.Message } });
            }
        }
    }
}
