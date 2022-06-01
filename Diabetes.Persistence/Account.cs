using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;

namespace Diabetes.Persistence
{
    public class Account:IdentityUser
    {
        public string Name { get; set; }
        public string? DiabetesType { get; set; }
        public DateTime Birthdate { get; set; }
        public string? Gender { get; set; }
        public int? Height { get; set; }
        public int? Weight { get; set; }
    }
}