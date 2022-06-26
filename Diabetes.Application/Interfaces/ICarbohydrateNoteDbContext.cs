using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Diabetes.Application.Interfaces
{
    public interface ICarbohydrateNoteDbContext
    {
        public DbSet<Domain.CarbohydrateNote> CarbohydrateNotes { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}