using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Diabetes.Application.Interfaces
{
    public interface IFoodPortionDbContext
    {
        public DbSet<Domain.FoodPortion> FoodPortions { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}