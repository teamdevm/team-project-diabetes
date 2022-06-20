using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Diabetes.Application.Interfaces
{
    public interface IFoodDbContext
    {
        public DbSet<Domain.Food> Foods { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        
    }
}