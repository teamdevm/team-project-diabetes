using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Diabetes.Persistence.Contexts
{
    public sealed class AccountContext:IdentityDbContext<Account>
    {
        public AccountContext(DbContextOptions<AccountContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}