using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Diabetes.Application.Interfaces
{
    public interface IUsersFoodDbContext
    {
        public DbSet<Domain.UsersFood> UsersFoods { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}