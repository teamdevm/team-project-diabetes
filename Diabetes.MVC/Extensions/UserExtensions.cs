using System;
using System.Security.Claims;
using Diabetes.Persistence;
using Microsoft.AspNetCore.Identity;

namespace Diabetes.MVC.Extensions
{
    public static class UserExtensions
    {
        public static Guid GetId(this ClaimsPrincipal user)
        {
            return Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
        }
    }
}